using System;
using System.Diagnostics;
using System.Reflection;
using UnityModManagerNet;
using HarmonyLib;
using ModKit;
using ModKit.Utility;

namespace SolastaGatherYourParty
{
    public class Core
    {

    }

    public class GatherYourPartySettings : UnityModManager.ModSettings
    {
        public const int GAME_PARTY_SIZE = 4;
        public const int MIN_PARTY_SIZE = 1;
        public const int MAX_PARTY_SIZE = 6;
        public const int DUNGEON_MIN_LEVEL = 1;
        public const int DUNGEON_MAX_LEVEL = 20;
        public const float ADVENTURE_PANEL_DEFAULT_SCALE = 0.75f;
        public const float REST_PANEL_DEFAULT_SCALE = 0.8f;
        public const float PARTY_CONTROL_PANEL_DEFAULT_SCALE = 0.95f;
        public const float VICTORY_MODAL_DEFAULT_SCALE = 0.85f;

        public int PartySize = GAME_PARTY_SIZE;
        public bool DungeonLevelBypass = false;
        public int DungeonMinLevel = DUNGEON_MIN_LEVEL;
        public int DungeonMaxLevel = DUNGEON_MAX_LEVEL;
        public float AdventurePanelScale = ADVENTURE_PANEL_DEFAULT_SCALE;
        public float RestPanelScale = REST_PANEL_DEFAULT_SCALE;
        public float PartyControlPanelScale = PARTY_CONTROL_PANEL_DEFAULT_SCALE;
        public float VictoryModalScale = VICTORY_MODAL_DEFAULT_SCALE;
        public bool InvincibleParty = false;
        public bool IdleEnemies = false;
        public bool NoFogOfWar = false;
    }

    public class Main
    {
        [Conditional("DEBUG")]
        internal static void Log(string msg) => Logger.Log(msg);
        internal static void Error(Exception ex) => Logger?.Error(ex.ToString());
        internal static void Error(string msg) => Logger?.Error(msg);
        internal static void Warning(string msg) => Logger?.Warning(msg);
        internal static UnityModManager.ModEntry.ModLogger Logger { get; private set; }
        internal static ModManager<Core, GatherYourPartySettings> Mod;
        internal static MenuManager Menu;
        internal static GatherYourPartySettings Settings { get { return Mod.Settings; } }

        public static bool Loaded = false;

        internal static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Logger = modEntry.Logger;

                Mod = new ModManager<Core, GatherYourPartySettings>();
                Menu = new MenuManager();
                modEntry.OnToggle = OnToggle;

                var harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Error(ex);
                throw;
            }

            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool enabled)
        {
            if (enabled)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Mod.Enable(modEntry, assembly);
                Menu.Enable(modEntry, assembly);
            }
            else
            {
                Menu.Disable(modEntry);
                Mod.Disable(modEntry, false);
                ReflectionCache.Clear();
            }
            return true;
        }
    }
}
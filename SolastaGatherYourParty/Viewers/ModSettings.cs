using UnityModManagerNet;
using ModKit;
using static SolastaGatherYourParty.Settings;

namespace SolastaGatherYourParty.Viewers
{
    public class ModSettings : IMenuSelectablePage
    {
        public string Name => "Mod Main.Settings";

        public int Priority => 0;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Main.Mod == null || !Main.Mod.Enabled) return;

            UI.Slider("Party Size", ref Main.Settings.PartySize,
                MIN_PARTY_SIZE, MAX_PARTY_SIZE, GAME_PARTY_SIZE, "", UI.AutoWidth());

            UI.Div();
            UI.Toggle("Bypass", ref Main.Settings.DungeonLevelBypass, 0);
            UI.Slider("Dungeon Min Level", ref Main.Settings.DungeonMinLevel,
                DUNGEON_MIN_LEVEL, DUNGEON_MAX_LEVEL, DUNGEON_MIN_LEVEL, "", UI.AutoWidth());
            if (Main.Settings.DungeonMinLevel > Main.Settings.DungeonMaxLevel) Main.Settings.DungeonMaxLevel = Main.Settings.DungeonMinLevel;
            UI.Slider("Dungeon Max Level", ref Main.Settings.DungeonMaxLevel,
                DUNGEON_MIN_LEVEL, DUNGEON_MAX_LEVEL, DUNGEON_MAX_LEVEL, "", UI.AutoWidth());
            if (Main.Settings.DungeonMaxLevel < Main.Settings.DungeonMinLevel) Main.Settings.DungeonMinLevel = Main.Settings.DungeonMaxLevel;

            UI.Div();
            UI.Slider("Adventure Panel Scale", ref Main.Settings.AdventurePanelScale,
                0.7f, 1.2f, ADVENTURE_PANEL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
            UI.Slider("Rest Panel Scale", ref Main.Settings.RestPanelScale,
                0.7f, 1.2f, REST_PANEL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
            UI.Slider("Party Control Panel Scale", ref Main.Settings.PartyControlPanelScale,
                0.7f, 1.2f, PARTY_CONTROL_PANEL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
            UI.Slider("Victory Modal Scale", ref Main.Settings.VictoryModalScale,
                0.7f, 1.2f, VICTORY_MODAL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
        }
    }
}
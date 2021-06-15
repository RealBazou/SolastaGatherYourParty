using UnityModManagerNet;
using ModKit;
using static SolastaGatherYourParty.Main;
using static SolastaGatherYourParty.GatherYourPartySettings;

namespace SolastaGatherYourParty.Menus.Viewers
{
    public class ModSettings : IMenuSelectablePage
    {
        public string Name => "Mod Settings";

        public int Priority => 0;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Mod == null || !Mod.Enabled) return;

            UI.Slider("Party Size", ref Settings.PartySize,
                MIN_PARTY_SIZE, MAX_PARTY_SIZE, GAME_PARTY_SIZE, "", UI.AutoWidth());

            UI.Div();
            UI.Toggle("Bypass", ref Settings.DungeonLevelBypass, 0);
            UI.Slider("Dungeon Min Level", ref Settings.DungeonMinLevel,
                DUNGEON_MIN_LEVEL, DUNGEON_MAX_LEVEL, DUNGEON_MIN_LEVEL, "", UI.AutoWidth());
            if (Settings.DungeonMinLevel > Settings.DungeonMaxLevel) Settings.DungeonMaxLevel = Settings.DungeonMinLevel;
            UI.Slider("Dungeon Max Level", ref Settings.DungeonMaxLevel,
                DUNGEON_MIN_LEVEL, DUNGEON_MAX_LEVEL, DUNGEON_MAX_LEVEL, "", UI.AutoWidth());
            if (Settings.DungeonMaxLevel < Settings.DungeonMinLevel) Settings.DungeonMinLevel = Settings.DungeonMaxLevel;

            UI.Div();
            UI.Slider("Adventure Panel Scale", ref Settings.AdventurePanelScale,
                0.7f, 1.2f, ADVENTURE_PANEL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
            UI.Slider("Rest Panel Scale", ref Settings.RestPanelScale,
                0.7f, 1.2f, REST_PANEL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
            UI.Slider("Party Control Panel Scale", ref Settings.PartyControlPanelScale,
                0.7f, 1.2f, PARTY_CONTROL_PANEL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
            UI.Slider("Victory Modal Scale", ref Settings.VictoryModalScale,
                0.7f, 1.2f, VICTORY_MODAL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
        }
    }
}
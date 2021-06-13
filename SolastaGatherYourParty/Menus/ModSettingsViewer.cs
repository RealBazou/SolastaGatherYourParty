using UnityModManagerNet;
using ModKit;
using static SolastaGatherYourParty.Main;

namespace SolastaGatherYourParty.Menus
{
    public class ModSettingsViewer : IMenuSelectablePage
    {
        public string Name => "Mod Settings";

        public int Priority => 0;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Mod == null || !Mod.Enabled)
                return;

            UI.Slider("Party Size", ref settings.PartySize,
                MIN_PARTY_SIZE, MAX_PARTY_SIZE, GAME_PARTY_SIZE, "", UI.AutoWidth());

            UI.Div();
            UI.Slider("Dungeon Min Level", ref settings.DungeonMinLevel,
                DUNGEON_MIN_LEVEL, DUNGEON_MAX_LEVEL, DUNGEON_MIN_LEVEL, "", UI.AutoWidth());
            UI.Slider("Dungeon Max Level", ref settings.DungeonMaxLevel,
                DUNGEON_MIN_LEVEL, DUNGEON_MAX_LEVEL, DUNGEON_MAX_LEVEL, "", UI.AutoWidth());

            if (settings.DungeonMinLevel > settings.DungeonMaxLevel)
            {
                settings.DungeonMinLevel = settings.DungeonMaxLevel;
            }
            if (settings.DungeonMaxLevel < settings.DungeonMinLevel)
            {
                settings.DungeonMaxLevel = settings.DungeonMinLevel;
            }

            UI.Div();
            UI.Slider("Adventure Panel Scale", ref settings.AdventurePanelScale,
                0.7f, 1.2f, ADVENTURE_PANEL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
            UI.Slider("Rest Panel Scale", ref settings.RestPanelScale,
                0.7f, 1.2f, REST_PANEL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
            UI.Slider("Party Control Panel Scale", ref settings.PartyControlPanelScale,
                0.7f, 1.2f, PARTY_CONTROL_PANEL_DEFAULT_SCALE, 2, "", UI.AutoWidth());
        }
    }
}

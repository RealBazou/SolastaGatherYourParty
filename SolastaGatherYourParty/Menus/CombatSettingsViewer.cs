using System.Collections.Generic;
using UnityModManagerNet;
using ModKit;
using static SolastaGatherYourParty.Main;

namespace SolastaGatherYourParty.Menus
{
    public class CombatSettingsViewer : IMenuSelectablePage
    {
        internal static readonly PlayerController PlayerAIController = new PlayerController(1000, "PlayerAI", PlayerController.ControllerType.AI, RuleDefinitions.Side.Ally);
        internal static PlayerController PlayerHumanController => ServiceRepository.GetService<IPlayerControllerService>().ActivePlayerController;
        internal static IGameLocationCharacterService GameLocationCharacterService => ServiceRepository.GetService<IGameLocationCharacterService>();
        internal static List<GameLocationCharacter> PartyCharacters => GameLocationCharacterService.PartyCharacters;

        internal static int[] AIChoices = new int[Main.MAX_PARTY_SIZE];

        public string Name => "Combat Settings";

        public int Priority => 1;

        public static void EnableAI()
        {
            if (PartyCharacters == null) return;

            for (var index = 0; index < PartyCharacters.Count; index++)
            {
                if (AIChoices[index] == 0)
                {
                    PartyCharacters[index].ControllerId = 0;
                }
                else
                {
                    PartyCharacters[index].ControllerId = 1000;
                }
            }
            PlayerHumanController.DirtyControlledCharacters();
            PlayerAIController.DirtyControlledCharacters();
        }

        public static void DisableAI()
        {
            if (PartyCharacters == null) return;

            for (var index = 0; index < PartyCharacters.Count; index++)
            {
                PartyCharacters[index].ControllerId = 0;
            }
            PlayerHumanController.DirtyControlledCharacters();
            PlayerAIController.DirtyControlledCharacters();
        }

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Mod == null || !Mod.Enabled) return;

            if (GameLocationCharacterService == null || PartyCharacters == null)
            {
                UI.Label("Party AI: Load or start new a game.", UI.AutoWidth());
            } else
            {
                UI.Label("Party AI: Changes only take effect at a new round.");
                for (var index = 0; index < PartyCharacters.Count; index++)
                {
                    UI.HStack(PartyCharacters[index].RulesetActor.Name, 1,
                        () => UI.SelectionGrid(ref AIChoices[index], new string[] { "Human", "Computer" }, 2, UI.AutoWidth())
                    );
                }
            }
        }
    }
}
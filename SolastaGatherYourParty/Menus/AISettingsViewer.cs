using System.Collections.Generic;
using UnityModManagerNet;
using HarmonyLib;
using ModKit;
using static SolastaGatherYourParty.Main;


namespace SolastaGatherYourParty.Menus
{
    public class AISettingsViewer : IMenuSelectablePage
    {
        public static readonly PlayerController PlayerAIController = new PlayerController(1000, "PlayerAI", PlayerController.ControllerType.AI, RuleDefinitions.Side.Ally);
        public static PlayerController PlayerHumanController => ServiceRepository.GetService<IPlayerControllerService>().ActivePlayerController;
        public static IGameLocationCharacterService GameLocationCharacterService => ServiceRepository.GetService<IGameLocationCharacterService>();
        public static List<GameLocationCharacter> PartyCharacters => GameLocationCharacterService.PartyCharacters;

        public static bool InGameLocation()
        {
            return GameLocationCharacterService != null && PartyCharacters != null;
        }

        public string Name => "AI Settings";

        public int Priority => 1;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Mod == null || !Mod.Enabled)
                return;

            if (GameLocationCharacterService == null || PartyCharacters == null)
            {
                UI.Label("First, start or load a new game.", UI.AutoWidth());
                return;
            }

            for(var index = 0; index < PartyCharacters.Count; index++)
            {
                var partyCharacter = PartyCharacters[index];
                var texts = new string[] { "Human", "AI" };
                UI.HStack(partyCharacter.RulesetActor.Name, 1,
                    () => UI.SelectionGrid(ref settings.AIChoices[index], texts, 2, UI.AutoWidth())
                );
            }

            for (var index = 0; index < PartyCharacters.Count; index++)
            {
                if (settings.AIChoices[index] == 0)
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
    }
}
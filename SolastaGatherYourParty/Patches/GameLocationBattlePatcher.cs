using HarmonyLib;

namespace SolastaGatherYourParty.Patches
{
    internal static class GameLocationBattlePatcher
    {
        internal static PlayerController AiPlayerController;

        [HarmonyPatch(typeof(GameLocationBattle), "Initialize")]
        internal static class GameLocationBattle_Initialize_Patch
        {
            internal static void Prefix()
            {
                AiPlayerController = new PlayerController(1, "PlayerAI", PlayerController.ControllerType.AI, RuleDefinitions.Side.Ally);
                ServiceRepository.GetService<IPlayerControllerService>().RegisterPlayerController(AiPlayerController);
            }
        }

        [HarmonyPatch(typeof(GameLocationBattle), "StartRound")]
        internal static class GameLocationBattle_StartRound_Patch
        {
            internal static void Prefix()
            {
                var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;
                var activePlayerController = ServiceRepository.GetService<IPlayerControllerService>().ActivePlayerController;

                for (var index = 0; index < party.Count; index++)
                {
                    party[index].ControllerId = Main.AIChoices[index];
                }
                AiPlayerController.DirtyControlledCharacters();
                activePlayerController.DirtyControlledCharacters(); 
            }
        }

        [HarmonyPatch(typeof(GameLocationBattle), "Shutdown")]
        internal static class GameLocationBattle_Shutdown_Patch
        {
            internal static void Prefix()
            {
                var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;
                var activePlayerController = ServiceRepository.GetService<IPlayerControllerService>().ActivePlayerController;

                for (var index = 0; index < party.Count; index++)
                {
                    party[index].ControllerId = activePlayerController.ControllerId;
                }
                AiPlayerController.DirtyControlledCharacters();
                activePlayerController.DirtyControlledCharacters();
                ServiceRepository.GetService<IPlayerControllerService>().UnregisterPlayerController(AiPlayerController);
            }
        }
    }
}
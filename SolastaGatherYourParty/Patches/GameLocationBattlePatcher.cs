using HarmonyLib;

namespace SolastaGatherYourParty.Patches
{
    internal static class GameLocationBattlePatcher
    {
        internal const int IN_BATTLE = -1;
        internal const int OUT_BATTLE = -2;
        internal static int CurrentRound = OUT_BATTLE;

        internal static PlayerController AiPlayerController;

        internal static PlayerController ActivePlayerController => ServiceRepository.GetService<IPlayerControllerService>().ActivePlayerController;

        [HarmonyPatch(typeof(GameLocationBattle), "Initialize")]
        internal static class GameLocationBattle_Initialize_Patch
        {
            internal static void Prefix(GameLocationBattle __instance)
            {
                if (CurrentRound == OUT_BATTLE)
                {
                    AiPlayerController = new PlayerController(1, "PlayerAI", PlayerController.ControllerType.AI, RuleDefinitions.Side.Ally);
                    ServiceRepository.GetService<IPlayerControllerService>().RegisterPlayerController(AiPlayerController);
                    CurrentRound = IN_BATTLE;
                }
            }
        }

        [HarmonyPatch(typeof(GameLocationBattle), "StartRound")]
        internal static class GameLocationBattle_StartRound_Patch
        {
            internal static void Prefix(GameLocationBattle __instance)
            {
                if (CurrentRound != __instance.CurrentRound)
                {
                    var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

                    for (var index = 0; index < party.Count; index++)
                    {
                        party[index].ControllerId = Main.AIChoices[index];
                    }
                    ActivePlayerController.DirtyControlledCharacters();
                    AiPlayerController.DirtyControlledCharacters();
                    CurrentRound = __instance.CurrentRound;
                }
            }
        }

        [HarmonyPatch(typeof(GameLocationBattle), "Shutdown")]
        internal static class GameLocationBattle_Shutdown_Patch
        {
            internal static void Prefix(GameLocationBattle __instance)
            {
                if (CurrentRound != OUT_BATTLE)
                {
                    var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

                    for (var index = 0; index < party.Count; index++)
                    {
                        party[index].ControllerId = 0;
                    }
                    ActivePlayerController.DirtyControlledCharacters();
                    AiPlayerController.DirtyControlledCharacters();
                    ServiceRepository.GetService<IPlayerControllerService>().UnregisterPlayerController(AiPlayerController);
                    CurrentRound = OUT_BATTLE;
                }
            }
        }
    }
}
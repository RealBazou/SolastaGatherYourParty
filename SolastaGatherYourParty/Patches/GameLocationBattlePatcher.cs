using HarmonyLib;
using System.Collections.Generic;

namespace SolastaGatherYourParty.Patches
{
    internal static class GameLocationBattlePatcher
    {
        internal static PlayerController PlayerAIController;
        internal static PlayerController PlayerHumanController => ServiceRepository.GetService<IPlayerControllerService>().ActivePlayerController;

        internal static int StartRound = -2; // keep a state as these methods are triggered twice in a battle

        [HarmonyPatch(typeof(GameLocationBattle), "Initialize")]
        internal static class GameLocationBattle_Initialize_Patch
        {
            internal static void Prefix(GameLocationBattle __instance)
            {
                if (StartRound == -2)
                {
                    PlayerAIController = new PlayerController(1, "PlayerAI", PlayerController.ControllerType.AI, RuleDefinitions.Side.Ally);
                    ServiceRepository.GetService<IPlayerControllerService>().RegisterPlayerController(PlayerAIController);
                    StartRound = -1;
                }
            }
        }

        [HarmonyPatch(typeof(GameLocationBattle), "StartRound")]
        internal static class GameLocationBattle_StartRound_Patch
        {
            internal static void Prefix(GameLocationBattle __instance)
            {
                if (StartRound != __instance.CurrentRound)
                {
                    var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

                    if (party != null)
                    {
                        for (var index = 0; index < party.Count; index++)
                        {
                            party[index].ControllerId = Main.AIChoices[index];
                        }
                        PlayerHumanController.DirtyControlledCharacters();
                        PlayerAIController.DirtyControlledCharacters();
                    }

                    StartRound = __instance.CurrentRound;
                }
            }
        }

        [HarmonyPatch(typeof(GameLocationBattle), "Shutdown")]
        internal static class GameLocationBattle_Shutdown_Patch
        {
            internal static void Prefix(GameLocationBattle __instance)
            {
                if (StartRound != -2)
                {
                    var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

                    for (var index = 0; index < party.Count; index++)
                    {
                        party[index].ControllerId = 0;
                    }
                    PlayerHumanController.DirtyControlledCharacters();
                    PlayerAIController.DirtyControlledCharacters();
                    ServiceRepository.GetService<IPlayerControllerService>().UnregisterPlayerController(PlayerAIController);

                    StartRound = -2;
                }
            }
        }
    }
}
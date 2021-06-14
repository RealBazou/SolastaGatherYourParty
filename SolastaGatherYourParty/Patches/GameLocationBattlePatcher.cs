using HarmonyLib;
using static SolastaGatherYourParty.Menus.CombatSettingsViewer;

namespace SolastaGatherYourParty.Patches
{
    internal static class GameLocationBattlePatcher
    {
        internal static int StartRound = -1;
        internal static int EndRound = -1;

        [HarmonyPatch(typeof(GameLocationBattle), "Initialize")]
        internal static class GameLocationBattle_Initialize_Patch
        {
            internal static void Prefix(GameLocationBattle __instance)
            {
                StartRound = -1;
                EndRound = -1;
            }
        }

        [HarmonyPatch(typeof(GameLocationBattle), "StartRound")]
        internal static class GameLocationBattle_StartRound_Patch
        {
            internal static void Prefix(GameLocationBattle __instance)
            {
                if (StartRound != __instance.CurrentRound)
                {
                    StartRound = __instance.CurrentRound;
                    EnableAI();
                }
            }
        }

        [HarmonyPatch(typeof(GameLocationBattle), "EndRound")]
        internal static class GameLocationBattle_EndRound_Patch
        {
            internal static void Prefix(GameLocationBattle __instance)
            {
                if (EndRound != __instance.CurrentRound)
                {
                    EndRound = __instance.CurrentRound;
                    DisableAI();
                }
            }
        }

        [HarmonyPatch(typeof(GameLocationBattle), "Shutdown")]
        internal static class GameLocationBattle_Shutdown_Patch
        {
            internal static void Prefix(GameLocationBattle __instance)
            {
                if (StartRound >= 0)
                {
                    DisableAI();
                    StartRound = -1;
                    EndRound = -1;
                }
            }
        }
    }
}
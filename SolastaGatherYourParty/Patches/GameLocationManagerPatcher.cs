using HarmonyLib;
using static SolastaGatherYourParty.Main;

namespace SolastaGatherYourParty.Patches
{    
    internal static class GameLocationCharacterManagerPatcher
    {
        [HarmonyPatch(typeof(GameLocationCharacterManager), "OnCharacterCreated")]
        internal static class GameLocationCharacterManager_OnCharacterCreated_Patch
        {
            // STILL NOT COVERING ALL SCENARIOS... NEED TO DEBUG

            internal static void Postfix(GameLocationCharacterManager __instance)
            {
                if (__instance.PartyCharacters.Count > Settings.GAME_PARTY_SIZE)
                {
                    for (var i = Settings.GAME_PARTY_SIZE; i < __instance.PartyCharacters.Count; i++)
                    {
                        var pos = __instance.PartyCharacters[i - Settings.GAME_PARTY_SIZE].LocationPosition;
                        __instance.PartyCharacters[i].LocationPosition = new TA.int3(pos.x, pos.y, pos.z);
                    }
                }
            }
        }
    }
}
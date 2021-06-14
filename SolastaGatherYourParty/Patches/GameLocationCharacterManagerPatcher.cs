using HarmonyLib;
using static SolastaGatherYourParty.Main;
using static SolastaGatherYourParty.GatherYourPartySettings;

namespace SolastaGatherYourParty.Patches
{
    internal static class GameLocationCharacterManagerPatcher
    {
        [HarmonyPatch(typeof(GameLocationCharacterManager), "OnCharacterCreated")]
        internal static class GameLocationCharacterManager_OnCharacterCreated_Patch
        {
            internal static void Prefix(GameLocationCharacterManager __instance)
            {
                // hack to bypass custom dungeon entrance gadget with only 4 locations...
                if (__instance.PartyCharacters.Count > GAME_PARTY_SIZE)
                {
                    for (var i = GAME_PARTY_SIZE; i < __instance.PartyCharacters.Count; i++)
                    {
                        var pos = __instance.PartyCharacters[i - GAME_PARTY_SIZE].LocationPosition;
                        __instance.PartyCharacters[i].LocationPosition = new TA.int3(pos.x, pos.y, pos.z);
                    }
                }
            }
        }
    }
}
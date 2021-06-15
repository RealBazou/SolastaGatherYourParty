using HarmonyLib;
using static SolastaGatherYourParty.GatherYourPartySettings;

namespace SolastaGatherYourParty.Patches
{
    internal static class GameLocationCharacterManagerPatcher
    {
        [HarmonyPatch(typeof(GameLocationCharacterManager), "BindServices")]
        internal static class GameLocationCharacterManager_BindServices_Patch
        {
            internal static void Prefix(GameLocationCharacterManager __instance)
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
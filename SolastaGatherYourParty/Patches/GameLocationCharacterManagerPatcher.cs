using HarmonyLib;

namespace SolastaGatherYourParty.Patches
{
    class GameLocationCharacterManagerPatch
    {
        [HarmonyPatch(typeof(GameLocationCharacterManager), "RefreshAllCharacters")]
        internal static class GameLocationCharacterManager_RefreshAllCharacters_Patch
        {
            internal static void Prefix(GameLocationCharacterManager __instance)
            {
                for(var index = Settings.GAME_PARTY_SIZE; index < __instance.PartyCharacters.Count; index++)
                {
                    __instance.PartyCharacters[index].LocationPosition = __instance.PartyCharacters[index - Settings.GAME_PARTY_SIZE].LocationPosition;
                }
            }
        }
    }
}
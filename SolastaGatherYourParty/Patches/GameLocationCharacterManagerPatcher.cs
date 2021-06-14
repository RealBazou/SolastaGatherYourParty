﻿using HarmonyLib;
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
                if (Settings.PartySize > GAME_PARTY_SIZE && __instance.PartyCharacters.Count == Settings.PartySize)
                {
                    for (var i = GAME_PARTY_SIZE; i < Settings.PartySize; i++)
                    {
                        var pos = __instance.PartyCharacters[i - GAME_PARTY_SIZE].LocationPosition;
                        __instance.PartyCharacters[i].LocationPosition = new TA.int3(pos.x, pos.y, pos.z);
                    }
                }
            }
        }
    }
}
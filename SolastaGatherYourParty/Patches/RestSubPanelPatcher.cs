using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using static SolastaGatherYourParty.Main;
using static SolastaGatherYourParty.GatherYourPartySettings;

namespace SolastaGatherYourParty.Patches
{
    internal static class RestSubPanelPatcher
    {
        internal static IGameLocationCharacterService GameLocationCharacterService => ServiceRepository.GetService<IGameLocationCharacterService>();
        internal static List<GameLocationCharacter> PartyCharacters => GameLocationCharacterService.PartyCharacters;

        [HarmonyPatch(typeof(RestSubPanel), "OnBeginShow")]
        internal static class RestSubPanel_OnBeginShow_Patch
        {
            internal static Vector3 originalModulesScale = new Vector3();
            internal static Vector3 originalPlatesScale = new Vector3();

            internal static void Prefix(RectTransform ___characterPlatesTable, RectTransform ___restModulesTable)
            {
                if (originalModulesScale.x == 0)
                {
                    originalModulesScale = ___restModulesTable.localScale;
                }
                if (originalPlatesScale.x == 0)
                {
                    originalPlatesScale = ___characterPlatesTable.localScale;
                }
                if (PartyCharacters.Count > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(Settings.RestPanelScale, PartyCharacters.Count - GAME_PARTY_SIZE);
                    ___restModulesTable.localScale = originalModulesScale * scale;
                    ___characterPlatesTable.localScale = originalPlatesScale * scale;
                }
            }
        }
    }
}
using System;
using UnityEngine;
using HarmonyLib;
using static SolastaGatherYourParty.Settings;

namespace SolastaGatherYourParty.Patches
{
    internal static class RestSubPanelPatcher
    {
        [HarmonyPatch(typeof(RestSubPanel), "OnBeginShow")]
        internal static class RestSubPanel_OnBeginShow_Patch
        {
            internal static void Prefix(RectTransform ___characterPlatesTable, RectTransform ___restModulesTable)
            {
                var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

                if (party?.Count > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(Main.Settings.RestPanelScale, party.Count - GAME_PARTY_SIZE);
                    ___restModulesTable.localScale = new Vector3(scale, scale, scale);
                    ___characterPlatesTable.localScale = new Vector3(scale, scale, scale);
                } else
                {
                    ___restModulesTable.localScale = new Vector3(1, 1, 1);
                    ___characterPlatesTable.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}
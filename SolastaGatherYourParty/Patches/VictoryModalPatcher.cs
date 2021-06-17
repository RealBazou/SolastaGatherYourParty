using System;
using UnityEngine;
using HarmonyLib;
using static SolastaGatherYourParty.Settings;

namespace SolastaGatherYourParty.Patches
{
    class VictoryModalPatcher
    {
        [HarmonyPatch(typeof(VictoryModal), "OnBeginShow")]
        internal static class VictoryModal_OnBeginShow_Patch
        {
            internal static void Prefix(RectTransform ___partyStatCellsContainer, RectTransform ___heroStatsGroup)
            {
                var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

                if (party?.Count > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(Main.Settings.VictoryModalScale, party.Count - GAME_PARTY_SIZE);
                    ___heroStatsGroup.localScale = new Vector3(scale, 1, scale);
                    
                } else
                    ___heroStatsGroup.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
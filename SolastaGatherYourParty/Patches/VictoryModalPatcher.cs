using System;
using UnityEngine;
using HarmonyLib;
using static SolastaGatherYourParty.Main;
using static SolastaGatherYourParty.GatherYourPartySettings;

namespace SolastaGatherYourParty.Patches
{
    internal static class VictoryModalPatcher
    {
        [HarmonyPatch(typeof(VictoryModal), "OnBeginShow")]
        internal static class VictoryModal_OnBeginShow_Patch
        {
            internal static Vector3 originalPartyStatCellsContainerScale;
            internal static Vector3 originalHeroStatsGroupScale;

            internal static void Prefix(RectTransform ___partyStatCellsContainer, RectTransform ___heroStatsGroup)
            {
                var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

                if (originalPartyStatCellsContainerScale == null)
                {
                    originalPartyStatCellsContainerScale = ___partyStatCellsContainer.localScale;
                }
                if (originalHeroStatsGroupScale == null)
                {
                    originalHeroStatsGroupScale = ___partyStatCellsContainer.localScale;
                }
                if (party?.Count > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(Settings.VictoryModalScale, party.Count - GAME_PARTY_SIZE);
                    ___partyStatCellsContainer.localScale = originalPartyStatCellsContainerScale * scale;
                    ___heroStatsGroup.localScale = originalHeroStatsGroupScale * scale;
                } else
                {
                    ___partyStatCellsContainer.localScale = originalPartyStatCellsContainerScale;
                    ___heroStatsGroup.localScale = originalHeroStatsGroupScale;
                }
            }
        }
    }
}
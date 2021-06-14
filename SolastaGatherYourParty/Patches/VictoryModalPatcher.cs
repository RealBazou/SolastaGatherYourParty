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
            internal static Vector3 partyStatCellsContainerScale;
            internal static Vector3 heroStatsGroupScale;

            internal static void Prefix(RectTransform ___partyStatCellsContainer, RectTransform ___heroStatsGroup)
            {
                var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

                if (party?.Count > GAME_PARTY_SIZE)
                {
                    if (partyStatCellsContainerScale == null)
                    {
                        partyStatCellsContainerScale = ___partyStatCellsContainer.localScale;
                    }
                    if (heroStatsGroupScale == null)
                    {
                        heroStatsGroupScale = ___partyStatCellsContainer.localScale;
                    }

                    var scale = (float)Math.Pow(Settings.VictoryModalScale, party.Count - GAME_PARTY_SIZE);
                    ___partyStatCellsContainer.localScale = partyStatCellsContainerScale * scale;
                    ___heroStatsGroup.localScale = heroStatsGroupScale * scale;
                }
            }
        }
    }
}
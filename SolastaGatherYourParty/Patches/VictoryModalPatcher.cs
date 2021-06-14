using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using static SolastaGatherYourParty.Main;
using static SolastaGatherYourParty.GatherYourPartySettings;

namespace SolastaGatherYourParty.Patches
{
    internal static class VictoryModalPatcher
    {
        internal static IGameLocationCharacterService GameLocationCharacterService => ServiceRepository.GetService<IGameLocationCharacterService>();
        internal static List<GameLocationCharacter> PartyCharacters => GameLocationCharacterService.PartyCharacters;

        [HarmonyPatch(typeof(VictoryModal), "OnBeginShow")]
        internal static class VictoryModal_OnBeginShow_Patch
        {
            internal static Vector3 partyStatCellsContainerScale = new Vector3();
            internal static Vector3 heroStatsGroupScale = new Vector3();

            internal static void Prefix(VictoryModal __instance, RectTransform ___partyStatCellsContainer, RectTransform ___heroStatsGroup)
            {
                if (partyStatCellsContainerScale.x == 0)
                {
                    partyStatCellsContainerScale = ___partyStatCellsContainer.localScale;
                }
                if (heroStatsGroupScale.x == 0)
                {
                    heroStatsGroupScale = ___heroStatsGroup.localScale;
                }
                if (PartyCharacters.Count > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(Settings.VictoryModalScale, PartyCharacters.Count - GAME_PARTY_SIZE);
                    ___partyStatCellsContainer.localScale = partyStatCellsContainerScale * scale;
                    ___heroStatsGroup.localScale = heroStatsGroupScale * scale;
                }
            }
        }
    }
}
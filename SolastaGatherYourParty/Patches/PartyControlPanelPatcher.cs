using UnityEngine;
using HarmonyLib;
using System;
using static SolastaGatherYourParty.Main;
using static SolastaGatherYourParty.GatherYourPartySettings;

namespace SolastaGatherYourParty.Patches
{
    internal static class PartyControlPanelPatcher
    {
        [HarmonyPatch(typeof(PartyControlPanel), "OnBeginShow")]
        internal static class PartyControlPanel_OnBeginShow_Patch
        {
            internal static Vector3 originalPlatesTableScale = new Vector3();

            internal static void Prefix(RectTransform ___partyPlatesTable)
            {
                var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

                if (originalPlatesTableScale.x == 0)
                {
                    originalPlatesTableScale = ___partyPlatesTable.localScale;
                }
                if (party?.Count > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(Settings.PartyControlPanelScale, party.Count - GAME_PARTY_SIZE);
                    ___partyPlatesTable.localScale = originalPlatesTableScale * scale;
                }
                else
                    ___partyPlatesTable.localScale = originalPlatesTableScale;
            }
        }
    }
}
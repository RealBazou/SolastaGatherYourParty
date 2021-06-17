using UnityEngine;
using HarmonyLib;
using System;
using static SolastaGatherYourParty.Settings;

namespace SolastaGatherYourParty.Patches
{
    class PartyControlPanelPatcher
    {
        [HarmonyPatch(typeof(PartyControlPanel), "OnBeginShow")]
        internal static class PartyControlPanel_OnBeginShow_Patch
        {
            internal static void Prefix(RectTransform ___partyPlatesTable)
            {
                var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

                if (party?.Count > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(Main.Settings.PartyControlPanelScale, party.Count - GAME_PARTY_SIZE);
                    ___partyPlatesTable.localScale = new Vector3(scale, scale, scale);
                }
                else
                    ___partyPlatesTable.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
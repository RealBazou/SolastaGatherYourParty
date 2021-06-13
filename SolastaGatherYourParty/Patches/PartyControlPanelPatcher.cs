using UnityEngine;
using HarmonyLib;
using System;
using static SolastaGatherYourParty.Main;

namespace SolastaGatherYourParty.Patches
{
    class PartyControlPanelPatcher
    {
        [HarmonyPatch(typeof(PartyControlPanel), "OnBeginShow")]
        internal static class NewAdventurePanel_OnBeginShow_Patch
        {
            internal static Vector3 originalPlatesTableScale = new Vector3();

            internal static void Prefix(RectTransform ___partyPlatesTable)
            {
                if (originalPlatesTableScale.x == 0)
                {
                    originalPlatesTableScale = ___partyPlatesTable.localScale;
                }
                if (settings.PartySize > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(settings.PartyControlPanelScale, settings.PartySize - GAME_PARTY_SIZE);
                    ___partyPlatesTable.localScale = originalPlatesTableScale * scale;
                }
            }
        }
    }
}

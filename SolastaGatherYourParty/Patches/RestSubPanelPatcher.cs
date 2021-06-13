﻿using System;
using UnityEngine;
using HarmonyLib;
using static SolastaGatherYourParty.Main;

namespace SolastaGatherYourParty.Patches
{
    internal static class RestSubPanelPatcher
    {
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
                if (settings.PartySize > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(settings.RestPanelScale, settings.PartySize - GAME_PARTY_SIZE);
                    ___restModulesTable.localScale = originalModulesScale * scale;
                    ___characterPlatesTable.localScale = originalPlatesScale * scale;
                }
            }
        }
    }
}
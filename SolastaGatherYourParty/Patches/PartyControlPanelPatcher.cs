using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System;
using static SolastaGatherYourParty.Main;
using static SolastaGatherYourParty.GatherYourPartySettings;

namespace SolastaGatherYourParty.Patches
{
    internal static class PartyControlPanelPatcher
    {
        internal static IGameLocationCharacterService GameLocationCharacterService => ServiceRepository.GetService<IGameLocationCharacterService>();
        internal static List<GameLocationCharacter> PartyCharacters => GameLocationCharacterService.PartyCharacters;

        [HarmonyPatch(typeof(PartyControlPanel), "OnBeginShow")]
        internal static class PartyControlPanel_OnBeginShow_Patch
        {
            internal static Vector3 originalPlatesTableScale = new Vector3();

            internal static void Prefix(RectTransform ___partyPlatesTable)
            {
                if (originalPlatesTableScale.x == 0)
                {
                    originalPlatesTableScale = ___partyPlatesTable.localScale;
                }
                if (PartyCharacters.Count > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(Settings.PartyControlPanelScale, PartyCharacters.Count - GAME_PARTY_SIZE);
                    ___partyPlatesTable.localScale = originalPlatesTableScale * scale;
                }
            }
        }
    }
}
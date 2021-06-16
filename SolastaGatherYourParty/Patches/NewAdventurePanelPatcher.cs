using System;
using UnityEngine;
using HarmonyLib;
using SolastaModApi;
using SolastaModApi.Extensions;
using static SolastaGatherYourParty.Settings;

namespace SolastaGatherYourParty.Patches
{
    internal static class NewAdventurePanelPatcher
    {
        [HarmonyPatch(typeof(NewAdventurePanel), "SelectUserLocation")]
        internal static class NewAdventurePanel_SelectUserLocation_Patch
        {
            internal static void Prefix(UserLocation userLocation)
            {
                if (userLocation != null && Main.Settings.DungeonLevelBypass)
                {
                    userLocation.StartLevelMin = Main.Settings.DungeonMinLevel;
                    userLocation.StartLevelMax = Main.Settings.DungeonMaxLevel;
                }
            }
        }

        [HarmonyPatch(typeof(NewAdventurePanel), "OnBeginShow")]
        internal static class NewAdventurePanel_OnBeginShow_Patch
        {
            internal static void Prefix(RectTransform ___characterSessionPlatesTable)
            {
                DatabaseHelper.CampaignDefinitions.UserCampaign.SetPartySize<CampaignDefinition>(Main.Settings.PartySize);

                for (var i = GAME_PARTY_SIZE; i < Main.Settings.PartySize; i++)
                {
                    var plate = UnityEngine.Object.Instantiate(___characterSessionPlatesTable.GetChild(0));
                    plate.SetParent(___characterSessionPlatesTable.GetChild(0).parent, false);
                }

                if (Main.Settings.PartySize > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(Main.Settings.AdventurePanelScale, Main.Settings.PartySize - GAME_PARTY_SIZE);
                    ___characterSessionPlatesTable.localScale = new Vector3(scale, scale, scale);
                }
                else
                    ___characterSessionPlatesTable.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
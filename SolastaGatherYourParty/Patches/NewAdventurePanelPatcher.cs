using System;
using UnityEngine;
using HarmonyLib;
using SolastaModApi;
using SolastaModApi.Extensions;
using static SolastaGatherYourParty.Main;
using static SolastaGatherYourParty.GatherYourPartySettings;

namespace SolastaGatherYourParty.Patches
{
    internal static class NewAdventurePanelPatcher
    {
        [HarmonyPatch(typeof(NewAdventurePanel), "SelectUserLocation")]
        internal static class NewAdventurePanel_SelectUserLocation_Patch
        {
            internal static void Prefix(UserLocation userLocation)
            {
                if (userLocation != null && Settings.DungeonLevelBypass)
                {
                    userLocation.StartLevelMin = Settings.DungeonMinLevel;
                    userLocation.StartLevelMax = Settings.DungeonMaxLevel;
                }
            }
        }

        [HarmonyPatch(typeof(NewAdventurePanel), "OnBeginShow")]
        internal static class NewAdventurePanel_OnBeginShow_Patch
        {
            internal static Vector3 originalSessionPlatesScale = new Vector3();

            internal static void Prefix(RectTransform ___characterSessionPlatesTable)
            {
                DatabaseHelper.CampaignDefinitions.UserCampaign.SetPartySize<CampaignDefinition>(Settings.PartySize);

                if (originalSessionPlatesScale.x == 0)
                {
                    originalSessionPlatesScale = ___characterSessionPlatesTable.localScale;
                }
                if (Settings.PartySize > GAME_PARTY_SIZE)
                {
                    var scale = (float)Math.Pow(Settings.AdventurePanelScale, Settings.PartySize - GAME_PARTY_SIZE);
                    ___characterSessionPlatesTable.localScale = originalSessionPlatesScale * scale;

                    for (var i = GAME_PARTY_SIZE; i < Settings.PartySize; i++)
                    {
                        var plate = UnityEngine.Object.Instantiate(___characterSessionPlatesTable.GetChild(0));
                        plate.SetParent(___characterSessionPlatesTable.GetChild(0).parent, false);
                    }
                }
                else
                    ___characterSessionPlatesTable.localScale = originalSessionPlatesScale;
            }
        }
    }
}
using HarmonyLib;
using SolastaModApi;
using SolastaModApi.Extensions;

namespace SolastaGatherYourParty.Patches
{
    class GameManagerPatcher
    {
        [HarmonyPatch(typeof(GameManager), "BindPostDatabase")]
        internal static class GameManager_BindPostDatabase_Patch
        {
            internal static void Postfix()
            {
                DatabaseHelper.CampaignDefinitions.UserCampaign.SetPartySize<CampaignDefinition>(Main.Settings.PartySize);
            }
        }
    }
}
using HarmonyLib;

namespace SolastaGatherYourParty.Patches
{
    class AiLocationManagerPatcher
    {
        [HarmonyPatch(typeof(AiLocationManager), "CharacterCreated")]
        internal static class AiLocationManager_CharacterCreated_Patch
        {
            internal static void Prefix(AiLocationManager __instance, GameLocationCharacter character)
            {
                var controllers = ServiceRepository.GetService<IPlayerControllerService>();

                if (character.ControllerId == 8)
                {
                   
                }
            }
        
            internal static void Postfix(AiLocationManager __instance, GameLocationCharacter character)
            {
                var controllers = ServiceRepository.GetService<IPlayerControllerService>();

                if (character.ControllerId == 8)
                {
                    
                }
            }
        }
    }
}
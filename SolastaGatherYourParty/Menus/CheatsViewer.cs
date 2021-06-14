using UnityEngine;
using UnityModManagerNet;
using ModKit;
using static SolastaGatherYourParty.Main;

namespace SolastaGatherYourParty.Menus
{
    public class CheatsViewer : IMenuSelectablePage
    {
        public string Name => "Cheats";

        public int Priority => 2;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Mod == null || !Mod.Enabled) return;

            UI.Toggle("Invincible Party", ref Settings.InvincibleParty, 0, UI.AutoWidth());
            UI.Toggle("Idle Enemies", ref Settings.IdleEnemies, 0, UI.AutoWidth());
            UI.Toggle("No Fog of War", ref Settings.NoFogOfWar, 0, UI.AutoWidth());

            Cheats.SetPartyInvicible(Settings.InvincibleParty);
            Cheats.SetMonstersIdle(Settings.IdleEnemies);
            Cheats.SetFogOfWar(Settings.NoFogOfWar);
        }
    }

    public static class Cheats
    {
        public static void SetPartyInvicible(bool invincible)
        {
            IGameLocationCharacterService service = ServiceRepository.GetService<IGameLocationCharacterService>();
            if (service == null)
                return;
            foreach (GameLocationCharacter partyCharacter in service.PartyCharacters)
            {
                if (invincible && !partyCharacter.RulesetCharacter.HasConditionOfCategoryAndType("15Debug", "ConditionDebugInvicible"))
                {
                    RulesetCondition condition = RulesetCondition.CreateCondition(partyCharacter.RulesetCharacter.Guid, DatabaseRepository.GetDatabase<ConditionDefinition>().GetElement("ConditionDebugInvicible"));
                    partyCharacter.RulesetCharacter.AddConditionOfCategory("15Debug", condition);
                }
                else if (!invincible && partyCharacter.RulesetCharacter.HasConditionOfCategoryAndType("15Debug", "ConditionDebugInvicible"))
                    partyCharacter.RulesetCharacter.RemoveAllConditionsOfCategoryAndType("15Debug", "ConditionDebugInvicible");
            }
        }

        public static void SetFogOfWar(bool disabled)
        {
            IGraphicsLocationPostProcessService service = ServiceRepository.GetService<IGraphicsLocationPostProcessService>();
            if (service == null)
                return;
            service.FowEnabled = !disabled;
        }

        public static void SetMonstersIdle(bool idleEnemies)
        {
            if ((Object)Gui.GameLocation != (Object)null)
                Gui.GameLocation.IdleEnemies = idleEnemies;
        }
    }
}

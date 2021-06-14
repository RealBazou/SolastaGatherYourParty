using System;

namespace SolastaGatherYourParty
{
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
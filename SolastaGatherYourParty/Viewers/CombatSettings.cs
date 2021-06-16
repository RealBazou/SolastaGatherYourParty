using UnityModManagerNet;
using ModKit;

namespace SolastaGatherYourParty.Viewers
{
    public class CombatSettings : IMenuSelectablePage
    {
        public string Name => "Combat Settings";

        public int Priority => 1;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Main.Mod == null || !Main.Mod.Enabled) return;

            var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;

            if (party != null)
            {
                UI.Label("Party AI: Changes only take effect at a new round.");
                for (var index = 0; index < party.Count; index++)
                {
                    UI.HStack(party[index].RulesetActor.Name, 1,
                        () => UI.SelectionGrid(ref Main.AIChoices[index], new string[] { "Human", "Computer" }, 2, UI.AutoWidth())
                    );
                }
            }
        }
    }
}
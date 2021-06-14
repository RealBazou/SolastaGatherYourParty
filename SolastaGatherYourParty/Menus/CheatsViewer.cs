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
            if (Mod == null || !Mod.Enabled)
                return;

            UI.Toggle("Invincible Party", ref settings.InvincibleParty, 0, UI.AutoWidth());
            UI.Toggle("Idle Enemies", ref settings.IdleEnemies, 0, UI.AutoWidth());
            UI.Toggle("No Fog of War", ref settings.NoFogOfWar, 0, UI.AutoWidth());

            Cheats.SetPartyInvicible(settings.InvincibleParty);
            Cheats.SetMonstersIdle(settings.IdleEnemies);
            Cheats.SetFogOfWar(settings.NoFogOfWar);
        }
    }
}

using UnityModManagerNet;
using ModKit;
using static SolastaGatherYourParty.Main;
using static SolastaGatherYourParty.Menus.Controllers.Cheats;

namespace SolastaGatherYourParty.Menus.Viewers
{
    public class Cheats : IMenuSelectablePage
    {
        public string Name => "Cheats";

        public int Priority => 2;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Mod == null || !Mod.Enabled) return;

            UI.Toggle("Invincible Party", ref Settings.InvincibleParty, 0, UI.AutoWidth());
            UI.Toggle("Idle Enemies", ref Settings.IdleEnemies, 0, UI.AutoWidth());
            UI.Toggle("No Fog of War", ref Settings.NoFogOfWar, 0, UI.AutoWidth());

            SetPartyInvicible(Settings.InvincibleParty);
            SetMonstersIdle(Settings.IdleEnemies);
            SetFogOfWar(Settings.NoFogOfWar);
        }
    }
}
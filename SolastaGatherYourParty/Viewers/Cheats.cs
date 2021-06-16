using UnityModManagerNet;
using ModKit;
using static SolastaGatherYourParty.Controllers.Cheats;

namespace SolastaGatherYourParty.Viewers
{
    public class Cheats : IMenuSelectablePage
    {
        public string Name => "Cheats";

        public int Priority => 2;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Main.Mod == null || !Main.Mod.Enabled) return;

            UI.Toggle("Invincible Party", ref Main.Settings.InvincibleParty, 0, UI.AutoWidth());
            UI.Toggle("Idle Enemies", ref Main.Settings.IdleEnemies, 0, UI.AutoWidth());
            UI.Toggle("No Fog of War", ref Main.Settings.NoFogOfWar, 0, UI.AutoWidth());

            SetPartyInvicible(Main.Settings.InvincibleParty);
            SetMonstersIdle(Main.Settings.IdleEnemies);
            SetFogOfWar(Main.Settings.NoFogOfWar);
        }
    }
}
using UnityModManagerNet;

namespace SolastaGatherYourParty
{
    public class Settings : UnityModManager.ModSettings
    {

        public int PartySize = 4;
        public int DungeonMinLevel = 1;
        public int DungeonMaxLevel = 20;
        public float AdventurePanelScale = 0.75f;
        public float RestPanelScale = 0.8f;
        public float PartyControlPanelScale = 0.95f;
        public bool InvincibleParty = false;
        public bool IdleEnemies = false;
        public bool FogOfWar = true;
        public int[] AIChoices = new int[Main.MAX_PARTY_SIZE];
    }
}
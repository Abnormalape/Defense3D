namespace BHSSolo.DungeonDefense.NPCs
{
    class NPCEventHandler
    {
        public delegate void NPCAttack();
        public event NPCAttack OnNPCAttack;

        public delegate void NPCAttacked();
        public event NPCAttacked OnNPCAttacked;

        public delegate void NPCDead();
        public event NPCDead OnNPCDead;
    }
}

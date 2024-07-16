namespace BHSSolo.DungeonDefense.NPCs
{
    class NPCEventHandler
    {
        //nPC means this NPC who has NPCEventHandler
        public delegate void NPCAttack(NPC nPC);
        public event NPCAttack OnNPCAttack;

        public delegate void NPCAttacked(NPC nPC);
        public event NPCAttacked OnNPCAttacked;

        public delegate void NPCDead(NPC nPC);
        public event NPCDead OnNPCDead;
    }
}

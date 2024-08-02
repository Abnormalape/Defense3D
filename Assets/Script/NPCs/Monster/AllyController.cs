namespace BHSSolo.DungeonDefense.NPCs
{
    class AllyController : NPC
    {
        private void Awake()
        {
            this.StateMachineBehaviour = new Function.StateMachineBehaviour();
        }

        public delegate void Attack();
        public delegate void Damaged();
        public delegate void Dead();
        public delegate void Placed();
        public delegate void DisPlaced();


        public event Attack OnAttack;
        public event Damaged OnDamaged;
        public event Dead OnDead;
        public event Placed OnPlaced;
        public event DisPlaced OnDisPlaced;
    }
}

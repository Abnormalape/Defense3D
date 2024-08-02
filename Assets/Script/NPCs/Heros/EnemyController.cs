namespace BHSSolo.DungeonDefense.NPCs
{
    class EnemyController : NPC
    {
        private void Awake()
        {
            this.StateMachineBehaviour = new Function.StateMachineBehaviour();
        }

        public delegate void Attack();
        public delegate void Damaged();
        public delegate void Dead();
        public delegate void Enter();
        public delegate void Left();


        public event Attack OnAttack;
        public event Damaged OnDamaged;
        public event Dead OnDead;
        public event Enter OnEnter;
        public event Left OnLeft;
    }
}

using BHSSolo.DungeonDefense.DungeonRoom;
using Unity.VisualScripting;

namespace BHSSolo.DungeonDefense.NPCs
{
    public class EnemyController : NPC
    {
        private void Awake()
        {
            this.StateMachineBehaviour = new();
            OnEnter += SetCurrentRoom;
            OnExit += SetCurrentRoomEmpty;
        }


        private void SetCurrentRoom(EnemyController enteredNPC, DungeonRoomController placingRoom)
        {
            CurrentRoom = placingRoom;
        }

        private void SetCurrentRoomEmpty(EnemyController exitedEnemy)
        {
            CurrentRoom = null;
        }


        public delegate void Attack(EnemyController thisEnemy);
        public delegate void Damaged(EnemyController thisEnemy);
        public delegate void Dead(EnemyController thisEnemy);
        public delegate void Enter(EnemyController enteredNPC, DungeonRoomController enteringRoom);
        public delegate void Exit(EnemyController thisEnemy);


        public event Attack OnAttack;
        public event Damaged OnDamaged;
        public event Dead OnDead;
        public event Enter OnEnter;
        public event Exit OnExit;
    }
}

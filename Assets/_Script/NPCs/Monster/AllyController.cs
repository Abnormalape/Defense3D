using BHSSolo.DungeonDefense.DungeonRoom;

namespace BHSSolo.DungeonDefense.NPCs
{
    public class AllyController : NPC
    {
        private void Awake()
        {
            this.StateMachineBehaviour = new();
            OnPlaced += SetCurrentRoom;
            OnDisPlaced += SetCurrentRoomEmpty;
        }

        private void SetCurrentRoom(AllyController enteredNPC, DungeonRoomController placingRoom)
        {
            CurrentRoom = placingRoom;
        }

        private void SetCurrentRoomEmpty()
        {
            CurrentRoom = null;
        }

        public delegate void Attack();
        public delegate void Damaged();
        public delegate void Dead();
        public delegate void Placed(AllyController placedAlly, DungeonRoomController placedRoom);
        public delegate void DisPlaced();


        public event Attack OnAttack;
        public event Damaged OnDamaged;
        public event Dead OnDead;
        public event Placed OnPlaced;
        public event DisPlaced OnDisPlaced;
    }
}

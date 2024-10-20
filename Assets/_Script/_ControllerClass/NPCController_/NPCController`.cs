using BHSSolo.DungeonDefense.Enums;
using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class NPCController_ : MonoBehaviour
    {
        public RoomController CurrentRoom;


        /// <summary>
        /// Should Initialized on Ally,EnemyController.
        /// </summary>
        public abstract NpcManager_ NpcManager_ { get; set; }



        public List<NPCController_> OpponentList { get; private set; } = new();
        public void AddOpponent(List<NPCController_> addedOpponents)
        {
            OpponentList.AddRange(addedOpponents);
            OnAddOpponentList(addedOpponents);
        }
        public void RemoveOpponent(List<NPCController_> removedOpponents)
        {
            OpponentList.AddRange(removedOpponents);
            OnRemoveOpponentList(removedOpponents);
        }
        public delegate void OpponentHandler(List<NPCController_> modifiedOpponents);
        public event OpponentHandler OnAddOpponentList;
        public event OpponentHandler OnRemoveOpponentList;

        public abstract NPCType NpcType { get; set; }

        public delegate void NpcEvent(IBuffHolder actor, IBuffHolder opponent);
        public event NpcEvent OnAttack;
        public event NpcEvent OnHit;
        public event NpcEvent OnAttacked;
        public event NpcEvent OnDamaged;
    }
}

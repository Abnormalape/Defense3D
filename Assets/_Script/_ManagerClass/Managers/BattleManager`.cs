using BHSSolo.DungeonDefense.Controller;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class BattleManager_ : MonoBehaviour, IManagerClass
    {
        AllyManager_ AllyManager_ { get; set; }
        EnemyManager_ EnemyManager_ { get; set; }
        public GameManager_ OwnerManager { get; set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            AllyManager_ = OwnerManager.AllyManager_;
            EnemyManager_ = OwnerManager.EnemyManager_;
        }

        public void SendBuff(NPCController_ buffSender, NPCController_ BuffReceiver)
        {

        }

        public void SendDamage(NPCController_ damageSender, NPCController_ damageReceiver)
        {

        }

        public void SpeedCalculator(float attackerSpeed, float targetSpeed)
        {

        }

        public void GuardCaculator(float attackerDamage, float targetDefense)
        {

        }

    }
}

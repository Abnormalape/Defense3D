using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.Enums;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class BattleManager_ : MonoBehaviour, IManagerClass
    {
        AllyManager_ AllyManager_ { get; set; }
        EnemyManager_ EnemyManager_ { get; set; }
        public GameManager_ GameManager { get; set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
            AllyManager_ = GameManager.AllyManager_;
            EnemyManager_ = GameManager.EnemyManager_;
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

        public void SendAttack(NPCController_ actor, NPCController_ opponent)
        {
            actor.Attack(actor, opponent);
            opponent.Attacked(opponent, actor);


        }
    }
}

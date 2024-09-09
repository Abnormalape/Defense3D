using BHSSolo.DungeonDefense.Controller;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class BattleManager_ //Todo:
    {
        AllyManager_ AllyManager_ { get; set; }
        EnemyManager_ EnemyManager_ { get; set; }



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

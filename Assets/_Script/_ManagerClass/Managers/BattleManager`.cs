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

        public void SendAttack(NPCType opponentType, int opponentID, temp_AllyController attacker) //Todo: temp_AllyController => battle NPC?
        {
            temp_AllyController hit = null; //Todo:

            //if (opponentType == NPCType.Enemy) { hit = EnemyManager_.ID_ControllerDictionary[opponentID]; }
            //else if (opponentType == NPCType.Ally) { hit = AllyManager_.ID_ControllerDictionary[opponentID]; }
            //else { Debug.Log("Wrong Type"); return; }

            AllyStatusBlock attackerStatus = new(attacker.allyCurrentStatus); //Todo: allyCurrentStatus => NPCCurrentStatus
            AllyStatusBlock hitStatus = new(hit.allyCurrentStatus);

            AllyStatusBlock attackerFinalStatus;
            AllyStatusBlock hitFinalStatus;

            float damage;

            //CalculateFinalStatus(attackerStatus, attacker.allyBuffs, out attackerFinalStatus);
            //CalculateFinalStatus(hitStatus, hit.allyBuffs, out hitFinalStatus);

            foreach (var e in attacker.allyBuffs)
            {
                //e.RunBuff(attackerStatus, attacker.allyBuffs, hitStatus, hit.allyBuffs);
            }

            foreach (var e in hit.allyBuffs)
            {
                //e.RunBuff(hitStatus, hit.allyBuffs, attackerStatus, attacker.allyBuffs);
            }

            //CalculateDamage(attackerFinalStatus, hitFinalStatus, out damage);

            foreach (var e in attacker.allyBuffs)
            {
                //e.RunBuff(out damage);
            }

            foreach (var e in hit.allyBuffs)
            {
                //e.RunBuff(out damage);
            }

            //피격자의 상태를 변경?
        }
    }
}

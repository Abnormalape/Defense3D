
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class BuffController_
    {
        public int BuffType;
        public int BuffID = 0;

        private int buffDuration;
        public int BuffDuration
        {
            get => buffDuration;
            set
            {
                if (buffDuration - value <= 0)
                {
                    OnBuffFinished?.Invoke();
                    FinishBuff();
                }
                buffDuration -= value;
            }
        }

        //public IBuffHolder BuffHolder { get; private set; }

        private List<NpcStatModifier> statModifiers;


        public void InitializeBuff()//IBuffHolder buffHolder)
        {
            //BuffHolder = buffHolder;
            //statModifiers = BuffHolder.BuffDictionary[BuffID];

            //switch (BuffType)
            //{
            //    case 0: OnBuffFinished += RunBuff; break;
            //    case 1: BuffHolder.OnAttack += RunBuff; break;
            //    case 2: BuffHolder.OnAttacked += RunBuff; break;
            //    case 3: BuffHolder.OnDamaged += RunBuff; break;
            //    case 4: RunBuff(); break;
            //}
        }


        private void RunBuff()
        {
            Debug.Log("Buff Runed");
            //BuffHolder.StatModifiers.AddRange(statModifiers);
        }

        private void FinishBuff()
        {

        }


        public delegate void BuffEvent();

        public event BuffEvent OnBuffStarted;
        public event BuffEvent OnBuffFinished;
    }
}

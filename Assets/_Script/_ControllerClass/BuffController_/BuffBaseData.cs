using BHSSolo.DungeonDefense.ManagerClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    [System.Serializable]
    public class BuffBaseData //Data
    {
        public int BuffID;
        public string BuffName;

        public List<EffectType> EffectTypes;
        public List<int> EffectsIDs;

        public List<EffectValueTypes> EffectValueTypes;
        public List<int> EffectValueIDs;

        public TriggerTarget TriggerTarget;
        public SubTriggerTarget TriggerSubTarget;
        public TriggerAction TriggerAction;
        public SubTriggerAction SubTriggerAction;

        public EffectTarget EffectTarget;

        public DurationType DurationType;
    }

    public enum EffectType
    {
        None = 0, //Error
        BuffGiver,
        StatModifier,
        DamagerGiver,
        TargetController,
    }

    public enum EffectValueTypes
    {
        None = 0,
        Fixed,
        Variable,
    }

    public enum TriggerTarget
    {
        None = 0,
        BuffHolder,
        BuffHolderTarget,
        NearAlly,
        NearEnemy,
        AllAlly,
        AllEnemy,
        Time,
    }

    public enum SubTriggerTarget
    {
        None = 0,
        Buff,
        Stat,
        Passed,
    }

    public enum TriggerAction
    {
        None = 0,
        OnAttack, //공격을 시도
        OnHit, //공격이 적중
        OnAttacked, //공격을 받음
        OnDamaged, //데미지를 입음
        OnDead, //죽었을 때
        Start, //시작 할때
    }

    public enum SubTriggerAction
    {
        None = 0,
        Over, //스탯이 몇 이하일때
        Below, //체력이 몇 이상일때
        Changed, //공격력이 변경될때
        Execute, //어떤 버프가 실행될때
    }

    public enum EffectTarget
    {
        None = 0,
        BuffHolder,
        BuffHolderTarget,
        NearAlly,
        NearEnemy,
        AllAlly,
        AllEnemy,
        Opponent, //전투시, 상대방(복수 가능)
    }

    public enum DurationType
    {
        OnTime, //지속시간이 시간에 따라 줄어든다
        OnStack, //지속시간이 버프 발동할때마다 줄어든다
        Infinite, //지속시간이 줄어들지 않는다
    }


    //============================


    public interface IBuffHolder
    {
        public Dictionary<int, BuffController> HoldingBuffs { get; set; } //버프를 가진 애니까 보유버프 목록이 당연히 있어야지?
    }

    public class SampleBuffHolder : IBuffHolder //aka npc, room
    {
        public Dictionary<int, BuffController> HoldingBuffs { get; set; }
        BuffManager tempBuffManager;

        public List<NpcStatModifier> StatModifiers { get; set; } = new();

        public void InitializeSampleBuffHolder()
        {
            for (int i = 0; i < 10; i++)
            {
                AddBuff(i + 1);
            }
        }

        private void AddBuff(int buffID)
        {
            BuffController tempBuffController = tempBuffManager.InstantiateBuff(buffID, out BuffBaseData tempBuffBaseData);
            tempBuffController.InitializeBuff(tempBuffBaseData, this);
            HoldingBuffs.Add(tempBuffController.BuffID, tempBuffController);
        }
    }

    /// <summary>
    /// Must Execute [InitializeBuff] After Instantiate.
    /// </summary>
    public abstract class BuffController : IController
    {
        /// <summary>
        /// The Inheriting Object Must Declare This.
        /// </summary>
        public abstract int BuffID { get; set; }

        public IManagerClass OwnerManager { get; set; }
        public BuffManager BuffManager { get; set; }
        public IBuffHolder BuffHolder { get; set; }

        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            BuffManager = OwnerManager as BuffManager;
        }

        public void InitializeBuff(BuffBaseData buffBaseData, IBuffHolder buffHolder)
        {
            SetBuffUsingBaseData(buffBaseData);
        }

        public abstract void SubscribeTriggerAction();
        public abstract void ExecuteBuff(); //Todo: Buff's Action. Like Make Buff, Damage Someone etc...





        protected IBuffHolder BuffTriggerTarget;
        protected event Action BuffTriggerAction;
        private void SetBuffUsingBaseData(BuffBaseData buffBaseData) //Todo: Set Buff's Effect Target, Trigger Target etc...
        {
            if (buffBaseData.TriggerTarget == TriggerTarget.BuffHolder)
            {
                BuffTriggerTarget = BuffHolder;
            }
            else
            {
                //Not Yet
            }

            if (buffBaseData.TriggerAction == TriggerAction.OnHit)
            {
                SubscribeToEvent(BuffTriggerTarget, TriggerAction.OnHit.ToString());
            }
            else
            {
                //Not Yet
            }
        }

        public event Action action
        {
            add
            {

            }
            remove
            {

            }
        }


        private void SubscribeToEvent(IBuffHolder target, string eventName)
        {
            var targetType = target.GetType();

            var eventInfo = targetType.GetEvent(eventName);
            Action callback = delegate ()
            {
                Console.WriteLine($"DoSomething");
            };

            eventInfo.GetAddMethod().Invoke(target, new object[] { callback });
            if (eventInfo == null)
            {
                Debug.LogWarning($"Event '{eventName}' not found on target {target.GetType().Name}.");
                return;
            }

            var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, nameof(ExecuteBuff));

            eventInfo.AddEventHandler(target, handler);
        }

        //일단 관측 대상을 골라야 하고. TriggerTarget
        //다음으로는 대상의 어떤 동작에 반응 할건지 알아야 함. TriggerAction

        //만약 관측 대상이 같은 방의 아군이고, 동작이 사망이라면,
        //같은 방의 아군의 사망 이벤트 += ExecuteBuff 를 하는 식.

        //그게 끝나면 해당 동작에 ExecuteBuff를 연결해야 함.
        //ExecuteBuff의 내용은 크게 4개로 나뉘는데, 아래의 4가지임
        //(Make new StatModifier, Make new Buff, Make new DamageHandler, Make new TargetController)
        //statModifier의 경우 조정할 스탯과 수치를 가짐. (EffectType, EffectValue) from json
        //buffGiver의 경우 부여할 버프와 수치를 가짐. (EffectType, EffectValue) from json
        //DamageHandler의 경우 데미지와 데미지의 종류를 가짐. (EffectType, EffectValue) from json
        //TargetController 일반적인 버프와 다르게 대상의 동작을 제어함. (예를 들어 "미아"버프를 가진 대상은 최단 경로를 알아도 갈림길에서 랜덤 선택을 함.)

        //EffectTarget은 효과를 적용할 대상임. 그럴일은 잘 없지만, 버프가 실행이 되어도 상대가 없으면 버프는 실행되지 않음.
        //만약 내가 적을 죽였을때, (ExecuteBuff)같은 방의 아군에게 "용기"버프를 준다면 같은방에 아군이 없는, 즉 EffectTarget.count가 0이라면 누구에게도 버프를 주지 않음.

        //즉 관측 대상과 버프 대상은 동적으로 변경이 가능해야 함.
        //만약 내가 공격할때 상대에게 "출혈"버프를 준다면, 평상시에는 EffectTarget은 null이지만, 공격시 동적으로 EffectTarget에 opponent를 추가하고 공격 종료시 제거해야 함.
    }

    public class SampleBuff_StatModifier : BuffController
    {
        public override int BuffID { get; set; } = -1; //Buff Id Can't be Minus.

        public override void SubscribeTriggerAction()
        {
            //triggerTarget.triggerEvent += ExecuteBuff();
        }

        public override void ExecuteBuff()
        {
            NpcStatModifier tempStatModifire = new(NpcStat.PhysicalPower, 100000f, StatModifierType.Adder);
            (BuffHolder as SampleBuffHolder).StatModifiers.Add(tempStatModifire); //BuffHolder is Effect Target.
        }
    }

    public class BuffManager : MonoBehaviour, IManagerClass//Factory interface를 사용할까?
    {
        public GameManager_ GameManager { get; set; }
        private DataManager_ DataManager { get; set; }
        private List<BuffBaseData> BuffBaseDatas { get; set; } //Buff 초기화에 사용할 BaseData. Index와 ID가 일치한다(중요).
        public Dictionary<int, Type> BuffTypes { get; set; } = new();

        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
            DataManager = GameManager.DataManager_;
            //BuffBaseDatas = DataManager.BuffBaseDatas;

            var tempBuffs = Assembly.GetExecutingAssembly().GetTypes()
                                    .Where(t => typeof(BuffController).IsAssignableFrom(t)
                                             && !t.IsInterface
                                             && !t.IsAbstract).ToList();

            foreach (var buff in tempBuffs)
            {
                var instance = Activator.CreateInstance(buff);
                BuffTypes.Add((instance as BuffController).BuffID, buff); //ID를 key로 찾아낸 타입(객체)를 value로.
            }
        }

        /// <summary>
        /// Return buff with buff Data.
        /// Must Initialize Result.
        /// </summary>
        /// <param name="buffID">Buff Id To Instantiate</param>
        /// <param name="buffData">TempBuffBaseData To Initialize</param>
        /// <returns></returns>
        public BuffController InstantiateBuff(int buffID, out BuffBaseData buffData)
        {
            if (buffID < 0 || buffID >= BuffBaseDatas.Count)
            {
                Debug.LogWarning($"Invalid buffID: {buffID}");
                buffData = null;
                return null;
            }

            BuffTypes.TryGetValue(buffID, out var buffType); //For Instance.
            BuffBaseData tempBaseData = BuffBaseDatas[buffID]; //For Initialize.

            if (buffType == null || tempBaseData == null) { buffData = null; return null; }

            var instance = Activator.CreateInstance(buffType) as BuffController;
            instance.InitializeController(this);

            buffData = tempBaseData;
            return instance;
        }
    }
}
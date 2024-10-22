using BHSSolo.DungeonDefense.Enums;
using BHSSolo.DungeonDefense.Management;
using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class AllyController_ : NPCController_, IController, IStatHolder, IStateMachineOwner<AllyController_, AllyStates>, IBuffHolder
    {
        public NpcBaseStat NpcBaseStat { get; set; } //Todo: NpcBaseStat 자체가 빈 클래스가 내용 채워야 함
        public int maxLevel { get; set; }
        public int level { get; set; }
        public CustomStateMachine<AllyController_, AllyStates> StateMachine { get; set; }
        public override NPCType NpcType { get; set; } = NPCType.Ally;
        public abstract int Ally_ID { get; set; }

        public event IStatHolder.ResourceStatEvent OnCurrentResourceStatModified;
        public event IStatHolder.AbilityStatEvent OnFinalAbilityStatModified;
        public NpcBaseStat CurrentResourceStat { get; set; }
        public NpcBaseStat CurrentFinalStat { get; set; }

        public IManagerClass OwnerManager { get; set; }
        private AllyManager_ AllyManager_;
        public Dictionary<int, BuffController> HoldingBuffs { get; set; } = new();
        public override NpcManager_ NpcManager_ { get; set; }
        public BuffManager BuffManager { get; set; }
        public List<NpcStatModifier> StatModifiers { get; set; } = new();


        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            AllyManager_ = OwnerManager.GameManager.AllyManager_;
            NpcManager_ = OwnerManager.GameManager.NpcManager_;

            NpcBaseStat = new NpcBaseStat(AllyManager_.BaseDataDictionary[Ally_ID]);


            InitializeStateMachine(this);

            BuffManager = OwnerManager.GameManager.BuffManager_;
            int[] buffIDs = AllyManager_.BaseDataDictionary[Ally_ID].TraitIDs;
            foreach (int e in buffIDs)
            {
                var tempBuff = BuffManager.InstantiateBuff(e, out BuffBaseData tempBuffData);
                HoldingBuffs.Add(e, tempBuff);
                tempBuff.InitializeBuff(tempBuffData, this);
            }
        }

        protected virtual void Update()
        {
            StateMachine?.CurrentState?.StateUpdate();
        }

        public void InitializeStateMachine(AllyController_ stateBlackBoard)
        {
            StateMachine = new(stateBlackBoard);
        }

        public void ChangeState(AllyStates state)
        {
        }

        public void InitializeBuffHolder()
        {
        }
        public void AddBuff(int buffID)
        {
        }
        public void RemoveBuff(int buffID)
        {
        }
    }

    public enum AllyType
    {
        None,
        Goblin,
        Skeleton
    }
}
//    public abstract NPCCurrentStatus AllyStatus_ { get; set; }

//    public abstract NpcBaseStatus AllyBaseStatus_ { get; set; }

//    public abstract AllyType AllyEnum_ { get; set; }

//    public abstract AllyManager_ AllyManager_ { get; set; }

//    public abstract int level { get; set; }

//    public abstract int Ally_ID { get; set; }
//    //public int Ally_ID { get; set; }

//    public abstract BattleManager_ BattleManager_ { get; set; } //BattleManager

//    //===



//    public IState_ CurrentState { get; set; }
//    public Dictionary<Enum, IState_> Type_StateDictionary { get; set; } = new(10);

//    #region FindPath
//    //public List<DungeonGridData> SearchedPath { get; private set; } = new();
//    //private DungeonGridData startGrid;
//    //private DungeonGridData endGrid;
//    //private List<DungeonGridData> travledCrossRoads = new(20);
//    //private List<DungeonGridData> travledForks = new(20);


//    //public List<DungeonGridData> ExcludeGrids { get; private set; } = new(20);
//    //public void AddExclusion(DungeonGridData exclusion)
//    //{
//    //    if (!ExcludeGrids.Contains(exclusion))
//    //        ExcludeGrids.Add(exclusion);
//    //}
//    #endregion FindPath

//    public virtual void InitializeAllyController()
//    {
//        InitializeStateDictionary();
//        ChangeControllerState(AllyStates.Idle); //Todo:
//    }

//    protected virtual void Update()
//    {
//        CurrentState?.StateUpdate();
//    }

//    public void ChangeControllerState(Enum stateName)
//    {
//        if (Type_StateDictionary[stateName] == CurrentState)
//            return;

//        CurrentState?.StateExit();
//        CurrentState = Type_StateDictionary[stateName];
//        OnChangeControllerState();
//        CurrentState.StateEnter();
//    }

//    public void InitializeStateDictionary()
//    {
//        var tempAllyStates = Assembly.GetExecutingAssembly().GetTypes()
//                            .Where(t => typeof(IState_).IsAssignableFrom(t)
//                                     && typeof(IAllyState).IsAssignableFrom(t)
//                                     && !t.IsInterface
//                                     && !t.IsAbstract).ToList();

//        foreach (var allyState in tempAllyStates)
//        {
//            var tempAllyState = Activator.CreateInstance(allyState);

//            IAllyState tempIAllyState = tempAllyState as IAllyState;
//            tempIAllyState.InitializeAllyState(this);
//            AddState(tempIAllyState.AllyState, tempAllyState as IState_);
//        }
//    }

//    public void AddState(Enum stateName, IState_ state)
//    {
//        Type_StateDictionary.Add(stateName, state);
//    }

//    public void OnChangeControllerState()
//    {
//    }
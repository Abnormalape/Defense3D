using BHSSolo.DungeonDefense.Contruct;
using BHSSolo.DungeonDefense.Enums;
using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class EnemyController_ : MonoBehaviour, IStatHolder, IController, IStateMachineOwner<EnemyController_, EnemyStates>, INpc
    {
        public int maxLevel { get; set; }
        public int level { get; set; }
        public CustomStateMachine<EnemyController_, EnemyStates> StateMachine { get; set; }
        public NPCType NpcType { get; set; } = NPCType.Enemy;
        public abstract int Enemy_ID { get; set; }

        public int NPCID;
        public string Race;
        public int CurrentLevel;







        public NpcBaseStat NpcBaseStat { get; set; }
        public NpcBaseStat NpcFinalStat { get; set; }
        public List<NpcStatModifier> EnemyStatModifiers { get; set; }

        public void SetFinalStat()
        {
            NpcBaseStat tempBaseStat = new();

            if (EnemyStatModifiers != null)
            {
                foreach (var modifier in EnemyStatModifiers)
                {
                    tempBaseStat[modifier.NpcStat] += modifier.StatModifyValue;
                }
            }

            tempBaseStat += NpcBaseStat;

            NpcFinalStat = tempBaseStat;
        }







        public IManagerClass OwnerManager { get; set; }
        private EnemyManager_ enemyManager_;

        public virtual void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            enemyManager_ = OwnerManager.OwnerManager.EnemyManager_;


            NpcBaseStat = new NpcBaseStat(enemyManager_.BaseDataDictionary[Enemy_ID]);
            NpcFinalStat = new();


            InitializeStateMachine(this);
        }

        protected virtual void Update()
        {
            StateMachine.CurrentState.StateUpdate();
        }

        public void InitializeStateMachine(EnemyController_ stateBlackBoard)
        {
            Debug.Log("Initialize StateMachine");
            StateMachine = new(stateBlackBoard);
        }

        public void ChangeState(EnemyStates state)
        {
        }

        #region Find Path
        public List<DungeonGridData> SearchedPath { get; private set; } = new();
        private DungeonGridData startGrid;
        private DungeonGridData endGrid;
        private List<DungeonGridData> travledCrossRoads = new(20);
        private List<DungeonGridData> travledForks = new(20);


        public List<DungeonGridData> ExcludeGrids { get; private set; } = new(20);


        public void AddExclusion(DungeonGridData exclusion)
        {
            if (!ExcludeGrids.Contains(exclusion))
                ExcludeGrids.Add(exclusion);
        }

        public void SetSearchedPath(List<DungeonGridData> searchedPath)
        {
            SearchedPath.Clear();
            SearchedPath = searchedPath;
        }


        #endregion Find Path
    }

    public enum EnemyType
    {
        None,
        SwordMan,
        Archer
    }
}

//public abstract NPCCurrentStatus EnemyStatus_ { get; set; }

//public abstract NpcBaseStatus EnemyBaseStatus_ { get; set; }

//public abstract EnemyType EnemyEnum_ { get; set; }

//public abstract EnemyManager_ EnemyManager_ { get; set; }

//public abstract int level { get; set; }

//public abstract int Enemy_ID { get; set; }
////public int Enemy_ID { get; set; }

//public abstract BattleManager_ BattleManager_ { get; set; } //BattleManager

////===



//public IState_ CurrentState { get; set; }
//public Dictionary<Enum, IState_> Type_StateDictionary { get; set; } = new(10);

//public void ChangeControllerState(Enum stateName)
//{
//    if (Type_StateDictionary[stateName] == CurrentState)
//        return;

//    CurrentState?.StateExit();
//    CurrentState = Type_StateDictionary[stateName];
//    OnChangeControllerState();
//    CurrentState.StateEnter();
//}

//public void InitializeStateDictionary()
//{
//    var tempEnemyStates = Assembly.GetExecutingAssembly().GetTypes()
//                        .Where(t => typeof(IState_).IsAssignableFrom(t)
//                                 && typeof(IEnemyState).IsAssignableFrom(t)
//                                 && !t.IsInterface
//                                 && !t.IsAbstract).ToList();

//    foreach (var enemyState in tempEnemyStates)
//    {
//        var tempEnemyState = Activator.CreateInstance(enemyState);

//        IEnemyState tempIEnemyState = tempEnemyState as IEnemyState;
//        tempIEnemyState.InitializeEnemyState(this);
//        AddState(tempIEnemyState.EnemyState, tempEnemyState as IState_);
//    }
//}

//public void AddState(Enum stateName, IState_ state)
//{
//    Type_StateDictionary.Add(stateName, state);
//}

//public void OnChangeControllerState()
//{
//}

//#region FindPath
//public void SetSearchedPath(List<DungeonGridData> searchedPath)
//{
//    SearchedPath.Clear();
//    SearchedPath = searchedPath;
//}
//#endregion FindPath
using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class CursorManager : MonoBehaviour, IManagerClass, IGameStateReactor, IManagerStateMachine
    {
        public GameManager_ OwnerManager { get; set; }

        private GameStateManager_ GameStateManager { get; set; }
        private DungeonConstructManager DungeonConstructManager { get; set; }

        public GameObject FollowCursor { get; private set; }
        public IState_ CurrentState { get; set; } //Todo: CursorState
        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; } = new(); //Todo: CursorState
        

        public bool isAttachedOnGridCursor = false;
        public string attachedNameOnGridCursor;

        private const string GRID_TARGET_PATH = "Prefabs/RoomSilhouette/GridTarget";
        private GameObject GridTargetPrefab;
        public GameObject GridTarget { get; private set; }

        public string HoldingRoomName { get; private set; }
        public Vector2 HoldingRoomSize { get; private set; }
        public void SetHoldingRoomData(string holdingRoomName, int holdingRoomSizeX, int holdingRoomSizeZ)
        {
            HoldingRoomName = holdingRoomName;
            HoldingRoomSize = new Vector2(holdingRoomSizeX,holdingRoomSizeZ);
        }



        private void Update()
        {
            CurrentState.StateUpdate(); //Todo: CursorState

            //if (Input.GetMouseButtonUp(0)) // This should go to each cursor state.
            //{
            //    Debug.Log("Cursor Clicked");

            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hit;

            //    if (Physics.Raycast(ray, out hit, 1000f, LayerMask.NameToLayer("ClickableObject")))
            //    {
            //        Debug.Log(hit.transform.gameObject.name + " has Clicked!!");
            //    }
            //}
        }
        

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            this.GameStateManager = OwnerManager.GameStateManager_;
            this.DungeonConstructManager = OwnerManager.DungeonConstructManager_;
            GameStateManager.OnGameStateChanged += GameStateReaction; //Reaction.
            GridTargetPrefab = Resources.Load(GRID_TARGET_PATH) as GameObject;
            FollowCursor = GameObject.FindGameObjectWithTag("MainCursor"); //Null

            OnInitializeManager_StateMachine();
        }



        public void GameStateReaction(GameState gameState) //Todo:
        {
            switch (gameState)
            {
                case GameState.Dungeon_ObserveState:

                    ChangeManagerState(CursorState.OnManage_Idle);
                    Debug.Log("Cursor State : OnManager_Idle");

                    return;
                case GameState.Dungeon_ConstructionState:

                    ChangeManagerState(CursorState.OnManage_Idle);
                    Debug.Log("Cursor State : OnManager_Idle");

                    return;
                default:
                    Debug.Log("Cursor State Default");
                    return;
            }
        }

        public void OnInitializeManager_StateMachine()
        {
            FindCursorStates();
        }

        private void FindCursorStates()
        {
            var cursorStates = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(t => typeof(IState_).IsAssignableFrom(t)
                                         && typeof(ICursorState).IsAssignableFrom(t)
                                         && !t.IsInterface
                                         && !t.IsAbstract).ToList();

            foreach (var cursorState in cursorStates)
            {
                var tempCursorState = Activator.CreateInstance(cursorState);

                ICursorState tempIGameState = tempCursorState as ICursorState;
                tempIGameState.InitialzieCursorState(this);
                AddState(tempIGameState.CursorState, tempIGameState as IState_);
            }

            ChangeManagerState(CursorState.OnPlayer);
        }

        public void AddState(Enum stateName, IState_ state_)
        {
            Type_StateDictionary.Add(stateName, state_);
        }

        public void RemoveState(Enum stateName)
        {
            Type_StateDictionary.Remove(stateName);
        }

        public void ChangeManagerState(Enum stateName)
        {
            if (CurrentState == stateName) return;

            CurrentState?.StateExit();
            CurrentState = Type_StateDictionary[stateName];
            OnChangeManagerState();
            CurrentState.StateEnter();
        }

        public void OnChangeManagerState()
        {
            Debug.Log("Cursor State Changed.");
        }

        #region For GridCursorState
        public GameObject SummonGridTarget()
        {
            GridTarget = Instantiate(GridTargetPrefab); //Instantiate.

            CursorGridTargetController cursorGridTargetController //Get ref.
                = GridTarget.GetComponent<CursorGridTargetController>();

            string roomToSummonPath = $"Prefabs/RoomSilhouette/{HoldingRoomName}";
            Debug.Log(roomToSummonPath);
            GameObject roomToSummon = Resources.Load(roomToSummonPath) as GameObject;

            cursorGridTargetController.InitializeGridTarget(roomToSummon, HoldingRoomSize); //Initialize Grid Target.

            return GridTarget;
        }

        public void DestroyGridTarget()
        {
            Destroy(GridTarget);
            SetHoldingRoomData("", 0, 0); //Reset HoldingRoomData.
        }
        #endregion For GridCursorState
    }

    public enum CursorState
    {
        OnPlayer, //Hide Cursor
        OnManage_Idle, //Show Cursor, Free Move
        OnManage_TargetNPC, //Show Cursor, Free Move, OnClickTarget : Ally
        OnManage_TargetRoom, //Show Cursor, Free Move, OnClickTarget : Room
        OnManage_Grid, //Show Cursor, Grid Move
        OnManage_Grid_FirstBuildDrag //Drag Movement when Room was First Build.
    }
}

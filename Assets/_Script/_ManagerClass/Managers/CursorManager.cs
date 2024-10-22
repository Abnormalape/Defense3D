using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class CursorManager : MonoBehaviour, IManagerClass, IGameStateReactor, IStateMachineOwner<CursorManager, CursorState>//, IManagerStateMachine
    {
        public GameManager_ GameManager { get; set; }

        private GameStateManager_ GameStateManager { get; set; }
        private DungeonConstructManager DungeonConstructManager { get; set; }

        public GameObject FollowCursor { get; private set; }


        private const string GRID_TARGET_PATH = "Prefabs/RoomSilhouette/GridTarget";
        private GameObject GridTargetPrefab;
        public GameObject GridTarget { get; private set; }


        private void Update()
        {
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
            StateMachine.CurrentState?.StateUpdate();
        }


        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
            this.GameStateManager = GameManager.GameStateManager_;
            this.DungeonConstructManager = GameManager.DungeonConstructManager_;
            GameStateManager.OnGameStateChanged += GameStateReaction;
            GridTargetPrefab = Resources.Load(GRID_TARGET_PATH) as GameObject;
            FollowCursor = GameObject.FindGameObjectWithTag("MainCursor");
            InitializeStateMachine(this);
        }



        public void GameStateReaction(GameState gameState) //Todo:
        {
            switch (gameState)
            {
                case GameState.Dungeon_ObserveState:
                    ChangeState(CursorState.OnManage_Idle);
                    Debug.Log("Cursor State : OnManager_Idle");
                    return;
                case GameState.Dungeon_ConstructionState:
                    ChangeState(CursorState.OnManage_Idle);
                    Debug.Log("Cursor State : OnManager_Idle");
                    return;
                default:
                    Debug.Log("Cursor State Default");
                    return;
            }
        }

        #region For GridCursorState
        public GameObject SummonGridTarget()
        {
            GridTarget = Instantiate(GridTargetPrefab);

            CursorGridTargetController cursorGridTargetController
                = GridTarget.GetComponent<CursorGridTargetController>();

            Vector2 roomSize = new Vector2(
                DungeonConstructManager.ConstructionProgress.HoldingRoomWidth,
                DungeonConstructManager.ConstructionProgress.HoldingRoomDepth); //Todo:

            cursorGridTargetController.InitializeGridTarget(null, roomSize);

            return GridTarget;
        }

        public void DestroyGridTarget()
        {
            Destroy(GridTarget);
        }
        #endregion For GridCursorState

        //=====================================

        public CustomStateMachine<CursorManager, CursorState> StateMachine { get; set; }
        public void InitializeStateMachine(CursorManager stateBlackBoard)
        {
            StateMachine = new(stateBlackBoard);
        }

        public void ChangeState(CursorState state)
        {
            StateMachine.ChangeState(state);
        }
    }

    public enum CursorState
    {
        OnPlayer, 
        OnManage_Idle, 
        OnManage_TargetNPC, 
        OnManage_TargetRoom, 
        OnManage_Grid, 
        OnManage_Grid_RoomBuildAfterJudge,
        OnManage_Grid_PassageBuildAfterJudge,
    }
}

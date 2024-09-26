using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class CursorManager : MonoBehaviour, IManagerClass, IGameStateReactor, IManagerStateMachine
    {
        public GameManager_ OwnerManager { get; set; }

        private GameStateManager_ GameStateManager { get; set; }
        private DungeonConstructManager DungeonConstructManager { get; set; }

        public GameObject GridCursor { get; private set; }
        public GameObject FollowCursor { get; private set; }
        public IState_ CurrentState { get; set; } //Todo: CursorState
        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; } //Todo: CursorState
        public StateMachineBehaviour_ StateMachineBehaviour_ { get; set; } //Todo: CursorState

        public bool isAttachedOnGridCursor = false;
        public string attachedNameOnGridCursor;


        private void Update()
        {
            CurrentState.StateUpdate();

            if (Input.GetMouseButtonUp(0)) // This should go to each cursor state.
            {
                Debug.Log("Cursor Clicked");

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer("ClickableObject")))
                {
                    Debug.Log(hit.transform.gameObject.name + " has Clicked!!");
                }
            }

            if (isAttachedOnGridCursor) // This should go to each cursor state (state Construction Grid).
            {
                if (Input.GetMouseButtonDown(0))
                    keypress = true;

                if (Input.GetMouseButtonUp(0) && keypress)
                {
                    DungeonConstructManager.JudgeConstruction(GridCursor.transform.position, attachedNameOnGridCursor);
                    keypress = false;
                }
            }
        }
        private bool keypress = false; //Todo: Remove

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            this.GameStateManager = OwnerManager.GameStateManager_;
            this.DungeonConstructManager = OwnerManager.DungeonConstructManager_;
            GameStateManager.OnGameStateChanged += GameStateReaction;
            GridCursor = GameObject.FindGameObjectWithTag("GridCursor");
            FollowCursor = GameObject.FindGameObjectWithTag("MainCursor"); //Null
        }

        public void GameStateReaction(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Dungeon_ObserveState:
                    //Todo: Current Cursor State = Clickable
                    Debug.Log("Cursor State Clickable");
                    return;
                case GameState.Dungeon_ConstructionState:
                    Debug.Log("Cursor State Construct");
                    return;
                default:
                    Debug.Log("Cursor State Default");
                    return;
            }
        }

        public void AttachGameObjectToCursor(CursorType cursorToAttach, GameObject gameObjectToAttach, string attachedName)
        {
            GameObject tempCursor;

            switch (cursorToAttach)
            {
                case CursorType.FollowCursor:
                    //ChangeManagerState(CursorType); //Todo: Cursor State
                    tempCursor = FollowCursor;
                    break;
                case CursorType.GridCursor:
                    //ChangeManagerState(CursorType); //Todo: Cursor State
                    tempCursor = GridCursor;
                    isAttachedOnGridCursor = true;
                    attachedNameOnGridCursor = attachedName;
                    break;
                default:
                    tempCursor = null;
                    return;
            }

            gameObjectToAttach.transform.parent = tempCursor.transform;
            gameObjectToAttach.transform.localPosition = Vector3.zero;
        }

        public void OnInitializeManager_StateMachine()
        {
            //Todo: CursorState
            //Todo: Find CursorStates
        }

        public void AddState(Enum stateName, IState_ state_)
        {
            //Todo: CursorState
        }

        public void RemoveState(Enum stateName)
        {
            //Todo: CursorState
        }

        public void ChangeManagerState(Enum stateName)
        {
            //Todo: CursorState, Judge changing state is current state.
            CurrentState.StateExit();
            CurrentState = Type_StateDictionary[stateName]; //Todo: Find IState with Enum.
            CurrentState.StateEnter();
        }

        public void OnChangeManagerState()
        {
            //Todo: CursorState
        }
    }

    public enum CursorType
    {
        FollowCursor,
        GridCursor,
    }
}

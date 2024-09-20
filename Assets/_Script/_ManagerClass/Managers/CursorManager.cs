using UnityEngine;
using UnityEngine.EventSystems;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class CursorManager : MonoBehaviour, IManagerClass, IGameStateReactor
    {
        public GameManager_ OwnerManager { get; set; }

        private GameStateManager_ GameStateManager { get; set; }
        private DungeonConstructManager DungeonConstructManager { get; set; }

        public GameObject GridCursor { get; private set; }
        public GameObject FollowCursor { get; private set; }


        public bool isAttachedOnGridCursor = false;
        public string attachedNameOnGridCursor;


        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Cursor Clicked");

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer("ClickableObject")))
                {
                    Debug.Log(hit.transform.gameObject.name + " has Clicked!!");
                }
            }

            if (isAttachedOnGridCursor)
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
            FollowCursor = GameObject.FindGameObjectWithTag("MainCursor");
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
                    tempCursor = FollowCursor;
                    break;
                case CursorType.GridCursor:
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
    }

    public enum CursorType
    {
        FollowCursor,
        GridCursor,
    }
}

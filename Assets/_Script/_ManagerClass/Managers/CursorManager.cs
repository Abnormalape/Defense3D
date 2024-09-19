using UnityEngine;
using UnityEngine.EventSystems;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class CursorManager : MonoBehaviour, IManagerClass, IGameStateReactor
    {
        public GameManager_ OwnerManager { get; set; }

        private GameStateManager_ GameStateManager { get; set; }

        public GameObject mainCursor { get; private set; }

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
        }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            GameStateManager = OwnerManager.GameStateManager_;
            GameStateManager.OnGameStateChanged += GameStateReaction;
            mainCursor = GameObject.FindGameObjectWithTag("MainCursor");
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
    }
}

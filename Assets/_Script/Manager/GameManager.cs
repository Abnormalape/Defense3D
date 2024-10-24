using BHSSolo.DungeonDefense.Game;
using BHSSolo.DungeonDefense.InteractableObject;
using BHSSolo.DungeonDefense.Singleton;
using BHSSolo.DungeonDefense.UI;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Management
{
    public class GameManager : SingletonMono<GameManager>
    {
        public EnemyManager EnemyManager { get => _enemyManager; private set => _enemyManager = value; }

        public AllyManager AllyManager { get => _allyManager; private set => _allyManager = value; }

        public RoomManager RoomManager { get => _roomManager; private set => _roomManager = value; }

        public GameStateController GameStateController { get => _gameStateController; private set => _gameStateController = value; }

        public InteractableObjectManager InteractableObjectManager
        {
            get => interactableObjectManager;
            set => interactableObjectManager = value;
        }


        public UIManager UIManager
        {
            get
            {
                if (_uIManager == null)
                {
                    _uIManager = new UIManager();
                }
                return _uIManager;
            }
        }

        

        public bool IsSaveGameDataExsist { get; private set; }

        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private AllyManager _allyManager;
        [SerializeField] private RoomManager _roomManager;
        [SerializeField] private GameStateController _gameStateController;
        [SerializeField] private UIManager _uIManager;
        [SerializeField] private InteractableObjectManager interactableObjectManager;

        private void Awake()
        {
            if (EnemyManager == null) { EnemyManager = GetComponent<EnemyManager>(); }
            if (AllyManager == null) { AllyManager = GetComponent<AllyManager>(); }
            if (RoomManager == null) { RoomManager = GetComponent<RoomManager>(); }
        }
    }
}
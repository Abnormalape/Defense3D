using BHSSolo.DungeonDefense.Data;
using UnityEditor;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    [RequireComponent(typeof(UIManager_))]
    [RequireComponent(typeof(RoomManager_))]
    [RequireComponent(typeof(AllyManager_))]
    [RequireComponent(typeof(DataManager_))]
    [RequireComponent(typeof(AudioManager))]
    [RequireComponent(typeof(SceneManager_))]
    [RequireComponent(typeof(EnemyManager_))]
    [RequireComponent(typeof(InputManager_))]
    [RequireComponent(typeof(PlayerManager_))]
    [RequireComponent(typeof(GameStateManager_))]
    [RequireComponent(typeof(InteractableManager_))]
    [RequireComponent(typeof(EventManager))]
    public class GameManager_ : MonoBehaviour
    {
        public UIManager_ UIManager_ { get; private set; }
        public RoomManager_ RoomManager_ { get; private set; }
        public AllyManager_ AllyManager_ { get; private set; }
        public DataManager_ DataManager_ { get; private set; }
        public AudioManager AudioManager_ { get; private set; }
        public SceneManager_ SceneManager_ { get; private set; }
        public EnemyManager_ EnemyManager_ { get; private set; }
        public InputManager_ InputManager_ { get; private set; }
        public PlayerManager_ PlayerManager_ { get; private set; }
        public GameStateManager_ GameStateManager_ { get; private set; }
        public InteractableManager_ InteractableManager_ { get; private set; }
        public EventManager EventManager_ { get; private set; }

        private void Awake()
        {
            GameData.InitializeGameData();

            UIManager_ = GetComponent<UIManager_>();
            RoomManager_ = GetComponent<RoomManager_>();
            AllyManager_ = GetComponent<AllyManager_>();
            DataManager_ = GetComponent<DataManager_>();
            AudioManager_ = GetComponent<AudioManager>();
            SceneManager_ = GetComponent<SceneManager_>();
            EnemyManager_ = GetComponent<EnemyManager_>();
            InputManager_ = GetComponent<InputManager_>();
            PlayerManager_ = GetComponent<PlayerManager_>();
            GameStateManager_ = GetComponent<GameStateManager_>();
            InteractableManager_ = GetComponent<InteractableManager_>();
            EventManager_ = GetComponent<EventManager>();

            this.DataManager_.InitializeManager(this); //DataManager First.
            this.UIManager_.InitializeManager(this);
            this.RoomManager_.InitializeManager(this);
            this.AllyManager_.InitializeManager(this);
            this.AudioManager_.InitializeManager(this);
            this.SceneManager_.InitializeManager(this);
            this.EnemyManager_.InitializeManager(this);
            this.InputManager_.InitializeManager(this);
            this.PlayerManager_.InitializeManager(this);
            this.GameStateManager_.InitializeManager(this);
            this.InteractableManager_.InitializeManager(this);
            this.EventManager_.InitializeManager(this);

        }

        private void Start()
        {
            Debug.Log("GameManager Alter Setting Complete");
        }
    }
}
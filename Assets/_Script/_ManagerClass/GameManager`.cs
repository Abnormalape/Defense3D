using BHSSolo.DungeonDefense.Data;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    [RequireComponent(typeof(NpcManager_))]
    [RequireComponent(typeof(TimeManager))]
    [RequireComponent(typeof(BuffManager))]
    public class GameManager_ : MonoBehaviour
    {
        public UIManager_ UIManager_ { get; private set; }
        public RoomManager_ RoomManager_ { get; private set; }
        public AllyManager_ AllyManager_ { get; private set; }
        public DataManager_ DataManager_ { get; private set; }
        public AudioManager AudioManager_ { get; private set; }
        public SceneManager_ SceneManager_ { get; private set; }
        public EnemyManager_ EnemyManager_ { get; private set; }
        public CustomInputManager CustomInputManager_ { get; private set; }
        public PlayerManager_ PlayerManager_ { get; private set; }
        public GameStateManager_ GameStateManager_ { get; private set; }
        public InteractableManager_ InteractableManager_ { get; private set; }
        public EventManager EventManager_ { get; private set; }
        public CameraManager CameraManager_ { get; private set; }
        public DungeonConstructManager DungeonConstructManager_ { get; private set; }
        public CursorManager CursorManager_ { get; private set; }
        public BattleManager_ BattleManager_ { get; private set; }
        public NpcManager_ NpcManager_ { get; private set; }
        public TimeManager TimeManager_ { get; private set; }
        public BuffManager BuffManager_ { get; private set; }

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
            CustomInputManager_ = GetComponent<CustomInputManager>();
            PlayerManager_ = GetComponent<PlayerManager_>();
            GameStateManager_ = GetComponent<GameStateManager_>();
            InteractableManager_ = GetComponent<InteractableManager_>();
            EventManager_ = GetComponent<EventManager>();
            CameraManager_ = GetComponent<CameraManager>();
            DungeonConstructManager_ = GetComponent<DungeonConstructManager>();
            BattleManager_ = GetComponent<BattleManager_>();
            CursorManager_ = GetComponent<CursorManager>();
            NpcManager_ = GetComponent<NpcManager_>();
            TimeManager_ = GetComponent<TimeManager>();
            BuffManager_ = GetComponent<BuffManager>();

            this.DataManager_.InitializeManager(this); //DataManager First.
            this.TimeManager_.InitializeManager(this);
            this.UIManager_.InitializeManager(this);
            this.RoomManager_.InitializeManager(this);
            this.BattleManager_.InitializeManager(this);
            this.DungeonConstructManager_.InitializeManager(this); //DungeonConstructManager Must Come Before AllyManager and EnemyManager.
            this.BuffManager_.InitializeManager(this); //Buff Must Come Before Ally and Enemy
            this.AllyManager_.InitializeManager(this);
            this.EnemyManager_.InitializeManager(this);
            this.NpcManager_.InitializeManager(this);
            this.AudioManager_.InitializeManager(this);
            this.SceneManager_.InitializeManager(this);
            this.CustomInputManager_.InitializeManager(this);
            this.PlayerManager_.InitializeManager(this);
            this.InteractableManager_.InitializeManager(this);
            this.EventManager_.InitializeManager(this);
            this.CameraManager_.InitializeManager(this);
            this.CursorManager_.InitializeManager(this);
            this.GameStateManager_.InitializeManager(this); //GameStateManager Last.
        }

        private void Start()
        {
            Debug.Log("GameManager Alter Setting Complete");
        }

        private void Update()
        {
        }
    }
}
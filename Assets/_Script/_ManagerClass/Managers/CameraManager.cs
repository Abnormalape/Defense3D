using UnityEngine;
using Cinemachine;
using System.Linq;
using static UnityEngine.UI.Image;
using System;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class CameraManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ GameManager { get; set; }

        [SerializeField] private CinemachineFreeLook PlayerFollow;
        [SerializeField] private CinemachineVirtualCamera DungeonView;
        private Dictionary<CameraState, CinemachineVirtualCameraBase> StateCameraDictoinary = new(10);
        private CameraState currentCameraState;
        private CinemachineVirtualCameraBase currentVirtualCamera;
        private GameStateManager_ gameStateManager_;

        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
            gameStateManager_ = GameManager.GameStateManager_;

            InitializeStateCameraDictionary();

            gameStateManager_.OnGameStateChanged += ChangeCameraState;
        }

        private void InitializeStateCameraDictionary()
        {
            StateCameraDictoinary.Add(CameraState.FollowPlayer, PlayerFollow);
            StateCameraDictoinary.Add(CameraState.DungeonView, DungeonView);

            foreach (var virtualCamera in StateCameraDictoinary)
            {
                virtualCamera.Value.Priority = 1; //Set all Virtual Camera's priority to 1.
            }
        }

        public void ChangeCameraState(GameState gameState)
        {
            Debug.Log("Camera State Changed");

            CameraState tempCameraState;

            if (gameState == GameState.Dungeon_ObserveState || gameState == GameState.Dungeon_ConstructionState)
                tempCameraState = CameraState.DungeonView;
            else
                tempCameraState = CameraState.FollowPlayer;

            if (currentCameraState == tempCameraState)
                return;

            if (currentVirtualCamera != null)
            {
                currentVirtualCamera.Priority = 1;
            }
            StateCameraDictoinary[tempCameraState].Priority = 2;

            currentCameraState = tempCameraState;
            currentVirtualCamera = StateCameraDictoinary[tempCameraState];
        }
    }

    public enum CameraState
    {
        None,
        FollowPlayer,
        DungeonView,
    }
}

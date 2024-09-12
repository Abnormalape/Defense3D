﻿using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.ManagerClass;
using System;
using UnityEngine;

namespace BHSSolo.DungeonDefense.InteractableObject
{
    public class DungeonCoreInteractable : InteractableController, IController
    {
        [SerializeField] private GameObject ObservePoint2D;
        private Camera _camera; //Todo: Remove
        private UIManager_ UIManager_;
        private GameStateManager_ GameStateManager_;
        private readonly GameState targetGameState = GameState.DungeonManagementState;


        public IManagerClass OwnerManager { get; set; }
        public override bool CanInteract { get; set; }
        public override float InteractRange { get; set; }
        public override InteractableManager_ InteractableManager { get; set; }


        public void ControllerInitializer(IManagerClass owenerManager)
        {
            OwnerManager = owenerManager;
            UIManager_ = OwnerManager.OwnerManager.UIManager_;
            this.GameStateManager_ = OwnerManager.OwnerManager.GameStateManager_;

            OnInteracted += this.GameStateManager_.ChangeManagerState;

            _camera = Camera.main; //Todo: Remove
        }

        public override void OnInteract()
        {
            _camera.transform.parent = ObservePoint2D.transform;
            _camera.transform.localPosition = Vector3.zero;
            _camera.transform.localRotation = Quaternion.identity;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UIManager_.Open(UI_enum.Manager);
            //=========================================//
            OnInteracted(targetGameState);
        }

        public override void OnInteractable()
        {
            Debug.Log("Now Dungeon Core Can Interact");
        }

        public override void OnNonInteractable()
        {
            Debug.Log("Now Dungeon Core Can Not Interact");
        }


        public delegate void Interacted(Enum gameState);
        public event Interacted OnInteracted;
    }
}

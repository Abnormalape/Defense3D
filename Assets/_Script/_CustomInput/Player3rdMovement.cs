//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Resources/InputActions/Player.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public partial class @Player: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Player()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""OnPlayer"",
            ""id"": ""4e6251aa-c214-4464-ba28-c70b60325582"",
            ""actions"": [
                {
                    ""name"": ""PlayerMoveAction"",
                    ""type"": ""Value"",
                    ""id"": ""dc0cd86f-f95d-49b1-b81a-f9dbc2ca5ac7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PlayerHorizontalAction"",
                    ""type"": ""Value"",
                    ""id"": ""c48f4aa8-8716-4b62-a223-092e400dbeca"",
                    ""expectedControlType"": ""Delta"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PlayerVerticalAction"",
                    ""type"": ""Value"",
                    ""id"": ""8310d5bf-02a7-49ce-85f1-e9914ee81238"",
                    ""expectedControlType"": ""Delta"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""125c9c0c-2fdb-4433-9be8-d81059905edb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMoveAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""971fd8ba-f27c-4b68-82ad-17124467be52"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""PlayerMoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a35d2002-feac-4fdc-8a69-419f7347afa9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""PlayerMoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b6a1250c-9242-4715-a3fd-acf9b062dda5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""PlayerMoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""73bbf2da-511c-4576-906b-36a239de5015"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""PlayerMoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Mouse"",
                    ""id"": ""254f0822-bd76-407f-b05d-6ed6671f81a4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerHorizontalAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""33c037ff-66f7-41b8-86ff-42d15a755ba7"",
                    ""path"": ""<Mouse>/delta/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerHorizontalAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6027d3b3-1cac-49be-81ba-6124db4e34e4"",
                    ""path"": ""<Mouse>/delta/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerHorizontalAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Mouse"",
                    ""id"": ""70104215-9c4d-4abb-9f20-723bf3607e34"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerVerticalAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8314dd72-9db6-44b4-8267-fb8677d95e76"",
                    ""path"": ""<Mouse>/delta/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerVerticalAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e120efd8-6f1c-467f-9057-0839c11b53c0"",
                    ""path"": ""<Mouse>/delta/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerVerticalAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""OnManage"",
            ""id"": ""8eea9926-b908-42d3-b2a2-7620689736dd"",
            ""actions"": [
                {
                    ""name"": ""ManageMoveAction"",
                    ""type"": ""Button"",
                    ""id"": ""bdba4360-2f76-4380-803b-b8f80ca88bb8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ManageZoomAction"",
                    ""type"": ""Button"",
                    ""id"": ""7abfd850-2e47-4843-b5a3-6ae0878d2bf0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a87a5d0a-07bd-4c27-a144-a3757cfd87f5"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ManageMoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65650a05-7fed-4d99-b936-a6b52239aa44"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ManageZoomAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // OnPlayer
            m_OnPlayer = asset.FindActionMap("OnPlayer", throwIfNotFound: true);
            m_OnPlayer_PlayerMoveAction = m_OnPlayer.FindAction("PlayerMoveAction", throwIfNotFound: true);
            m_OnPlayer_PlayerHorizontalAction = m_OnPlayer.FindAction("PlayerHorizontalAction", throwIfNotFound: true);
            m_OnPlayer_PlayerVerticalAction = m_OnPlayer.FindAction("PlayerVerticalAction", throwIfNotFound: true);
            // OnManage
            m_OnManage = asset.FindActionMap("OnManage", throwIfNotFound: true);
            m_OnManage_ManageMoveAction = m_OnManage.FindAction("ManageMoveAction", throwIfNotFound: true);
            m_OnManage_ManageZoomAction = m_OnManage.FindAction("ManageZoomAction", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // OnPlayer
        private readonly InputActionMap m_OnPlayer;
        private List<IOnPlayerActions> m_OnPlayerActionsCallbackInterfaces = new List<IOnPlayerActions>();
        private readonly InputAction m_OnPlayer_PlayerMoveAction;
        private readonly InputAction m_OnPlayer_PlayerHorizontalAction;
        private readonly InputAction m_OnPlayer_PlayerVerticalAction;
        public struct OnPlayerActions
        {
            private @Player m_Wrapper;
            public OnPlayerActions(@Player wrapper) { m_Wrapper = wrapper; }
            public InputAction @PlayerMoveAction => m_Wrapper.m_OnPlayer_PlayerMoveAction;
            public InputAction @PlayerHorizontalAction => m_Wrapper.m_OnPlayer_PlayerHorizontalAction;
            public InputAction @PlayerVerticalAction => m_Wrapper.m_OnPlayer_PlayerVerticalAction;
            public InputActionMap Get() { return m_Wrapper.m_OnPlayer; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(OnPlayerActions set) { return set.Get(); }
            public void AddCallbacks(IOnPlayerActions instance)
            {
                if (instance == null || m_Wrapper.m_OnPlayerActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_OnPlayerActionsCallbackInterfaces.Add(instance);
                @PlayerMoveAction.started += instance.OnPlayerMoveAction;
                @PlayerMoveAction.performed += instance.OnPlayerMoveAction;
                @PlayerMoveAction.canceled += instance.OnPlayerMoveAction;
                @PlayerHorizontalAction.started += instance.OnPlayerHorizontalAction;
                @PlayerHorizontalAction.performed += instance.OnPlayerHorizontalAction;
                @PlayerHorizontalAction.canceled += instance.OnPlayerHorizontalAction;
                @PlayerVerticalAction.started += instance.OnPlayerVerticalAction;
                @PlayerVerticalAction.performed += instance.OnPlayerVerticalAction;
                @PlayerVerticalAction.canceled += instance.OnPlayerVerticalAction;
            }

            private void UnregisterCallbacks(IOnPlayerActions instance)
            {
                @PlayerMoveAction.started -= instance.OnPlayerMoveAction;
                @PlayerMoveAction.performed -= instance.OnPlayerMoveAction;
                @PlayerMoveAction.canceled -= instance.OnPlayerMoveAction;
                @PlayerHorizontalAction.started -= instance.OnPlayerHorizontalAction;
                @PlayerHorizontalAction.performed -= instance.OnPlayerHorizontalAction;
                @PlayerHorizontalAction.canceled -= instance.OnPlayerHorizontalAction;
                @PlayerVerticalAction.started -= instance.OnPlayerVerticalAction;
                @PlayerVerticalAction.performed -= instance.OnPlayerVerticalAction;
                @PlayerVerticalAction.canceled -= instance.OnPlayerVerticalAction;
            }

            public void RemoveCallbacks(IOnPlayerActions instance)
            {
                if (m_Wrapper.m_OnPlayerActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IOnPlayerActions instance)
            {
                foreach (var item in m_Wrapper.m_OnPlayerActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_OnPlayerActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public OnPlayerActions @OnPlayer => new OnPlayerActions(this);

        // OnManage
        private readonly InputActionMap m_OnManage;
        private List<IOnManageActions> m_OnManageActionsCallbackInterfaces = new List<IOnManageActions>();
        private readonly InputAction m_OnManage_ManageMoveAction;
        private readonly InputAction m_OnManage_ManageZoomAction;
        public struct OnManageActions
        {
            private @Player m_Wrapper;
            public OnManageActions(@Player wrapper) { m_Wrapper = wrapper; }
            public InputAction @ManageMoveAction => m_Wrapper.m_OnManage_ManageMoveAction;
            public InputAction @ManageZoomAction => m_Wrapper.m_OnManage_ManageZoomAction;
            public InputActionMap Get() { return m_Wrapper.m_OnManage; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(OnManageActions set) { return set.Get(); }
            public void AddCallbacks(IOnManageActions instance)
            {
                if (instance == null || m_Wrapper.m_OnManageActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_OnManageActionsCallbackInterfaces.Add(instance);
                @ManageMoveAction.started += instance.OnManageMoveAction;
                @ManageMoveAction.performed += instance.OnManageMoveAction;
                @ManageMoveAction.canceled += instance.OnManageMoveAction;
                @ManageZoomAction.started += instance.OnManageZoomAction;
                @ManageZoomAction.performed += instance.OnManageZoomAction;
                @ManageZoomAction.canceled += instance.OnManageZoomAction;
            }

            private void UnregisterCallbacks(IOnManageActions instance)
            {
                @ManageMoveAction.started -= instance.OnManageMoveAction;
                @ManageMoveAction.performed -= instance.OnManageMoveAction;
                @ManageMoveAction.canceled -= instance.OnManageMoveAction;
                @ManageZoomAction.started -= instance.OnManageZoomAction;
                @ManageZoomAction.performed -= instance.OnManageZoomAction;
                @ManageZoomAction.canceled -= instance.OnManageZoomAction;
            }

            public void RemoveCallbacks(IOnManageActions instance)
            {
                if (m_Wrapper.m_OnManageActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IOnManageActions instance)
            {
                foreach (var item in m_Wrapper.m_OnManageActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_OnManageActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public OnManageActions @OnManage => new OnManageActions(this);
        private int m_PCSchemeIndex = -1;
        public InputControlScheme PCScheme
        {
            get
            {
                if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
                return asset.controlSchemes[m_PCSchemeIndex];
            }
        }
        public interface IOnPlayerActions
        {
            void OnPlayerMoveAction(InputAction.CallbackContext context);
            void OnPlayerHorizontalAction(InputAction.CallbackContext context);
            void OnPlayerVerticalAction(InputAction.CallbackContext context);
        }
        public interface IOnManageActions
        {
            void OnManageMoveAction(InputAction.CallbackContext context);
            void OnManageZoomAction(InputAction.CallbackContext context);
        }
    }
}

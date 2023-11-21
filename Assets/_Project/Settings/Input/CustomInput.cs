//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/_Project/Settings/Input/CustomInput.inputactions
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

namespace gameoff
{
    public partial class @CustomInput : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @CustomInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""CustomInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""ae06a5e2-f98f-4638-9276-acc0483f3d15"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""1059f4c3-e333-4ae2-b592-e651e31c0ef2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""a624df6b-eef1-4c85-adeb-397656e5bc8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WeaponRotationMouse"",
                    ""type"": ""Value"",
                    ""id"": ""91cb181d-9bbb-4732-9668-d45d6973e6bf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""WeaponRotationGamepad"",
                    ""type"": ""Value"",
                    ""id"": ""bab9b7f8-22a0-472a-bca8-9ecad06a7673"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PrimaryAttack"",
                    ""type"": ""Button"",
                    ""id"": ""b8a10d0b-2035-4422-ba47-dafac3912492"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpecialAttack"",
                    ""type"": ""Button"",
                    ""id"": ""9de426ce-7e4b-4b34-96b6-4e79d86810b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""e49c77f2-a3d0-4d7e-aa27-3073e8e2c138"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""051b413c-5c71-4afe-ae36-a9f3f5d70c1a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7e8368de-197e-41d5-8424-bcf2efd63ab1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""35ca816c-0221-4e16-9dd2-01610f536d18"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6d436312-e8f9-44a1-b4b7-bd3060bac898"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bc369b4d-e33c-4ed4-9ee7-17cf828dd0d9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""45e1ad6b-859f-46ed-8d67-72cc7a178a18"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9acd264c-f75f-4bae-bf46-d910208b5d78"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""219fb8c8-1327-4462-8123-32a07de9c5dd"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""87e5f693-b919-4691-a112-6dfd72e5b3c2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2ee9fec6-1d1e-4031-8586-459b85c1ab4e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller Left Analogue"",
                    ""id"": ""a943ab5d-4651-4aa3-96fa-427cc2026a28"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8499829c-0d80-4753-a21b-628fab0606bf"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""15b67914-640b-433b-93a0-2e1d9a0e84b4"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""68510936-e43b-40b7-843b-f3a5e8582122"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4f7e5498-8b97-478c-bcd2-781f8d2349ff"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7e403b07-34d3-4b6f-9b27-e40546e1eb0b"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65af5c98-2b47-4b26-a4f8-b5bd42f365b3"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f35ceff1-21ee-4e30-80ad-0cbd5ccd42e8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30b8119f-ac1d-49ca-987b-d94ccdce3d78"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponRotationMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Controller Right Analogue"",
                    ""id"": ""c5c8ca00-31f7-46a2-8810-b47d9f2503b6"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponRotationGamepad"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""73c550be-558c-450c-a3bd-887454ba60af"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponRotationGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""34a6e2ef-0e37-48e7-820b-a88c0783c688"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponRotationGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ec385686-e652-487f-ab3a-b209c6e42da7"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponRotationGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d6564d95-933e-48d9-91ef-e21d118c2cfe"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponRotationGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""074a05d2-d64b-4db0-86c5-790c478bdcce"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65725fbc-30ba-4494-bb9d-9b7577022d0e"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea168287-1fdd-46a5-888a-3ce1a2df5fd9"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f75cbcb6-0176-47b7-9e06-df24876352d4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a07aca4-a53a-425d-8a9d-a4e41860e256"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78f4e95f-a038-4210-85aa-3aefe98a1e06"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""General"",
            ""id"": ""7013d3f1-703b-4c8b-a8ef-86804f4a2dfd"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""be84ec03-5a03-460f-8947-933e4976e315"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5f4a2857-22c4-4465-a7ac-fe722159331a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""467a82ae-b1da-4ade-88ae-ff17c576ec21"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
            m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
            m_Player_WeaponRotationMouse = m_Player.FindAction("WeaponRotationMouse", throwIfNotFound: true);
            m_Player_WeaponRotationGamepad = m_Player.FindAction("WeaponRotationGamepad", throwIfNotFound: true);
            m_Player_PrimaryAttack = m_Player.FindAction("PrimaryAttack", throwIfNotFound: true);
            m_Player_SpecialAttack = m_Player.FindAction("SpecialAttack", throwIfNotFound: true);
            m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
            // General
            m_General = asset.FindActionMap("General", throwIfNotFound: true);
            m_General_Pause = m_General.FindAction("Pause", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Movement;
        private readonly InputAction m_Player_Dash;
        private readonly InputAction m_Player_WeaponRotationMouse;
        private readonly InputAction m_Player_WeaponRotationGamepad;
        private readonly InputAction m_Player_PrimaryAttack;
        private readonly InputAction m_Player_SpecialAttack;
        private readonly InputAction m_Player_Reload;
        public struct PlayerActions
        {
            private @CustomInput m_Wrapper;
            public PlayerActions(@CustomInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Player_Movement;
            public InputAction @Dash => m_Wrapper.m_Player_Dash;
            public InputAction @WeaponRotationMouse => m_Wrapper.m_Player_WeaponRotationMouse;
            public InputAction @WeaponRotationGamepad => m_Wrapper.m_Player_WeaponRotationGamepad;
            public InputAction @PrimaryAttack => m_Wrapper.m_Player_PrimaryAttack;
            public InputAction @SpecialAttack => m_Wrapper.m_Player_SpecialAttack;
            public InputAction @Reload => m_Wrapper.m_Player_Reload;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                    @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                    @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                    @WeaponRotationMouse.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponRotationMouse;
                    @WeaponRotationMouse.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponRotationMouse;
                    @WeaponRotationMouse.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponRotationMouse;
                    @WeaponRotationGamepad.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponRotationGamepad;
                    @WeaponRotationGamepad.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponRotationGamepad;
                    @WeaponRotationGamepad.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponRotationGamepad;
                    @PrimaryAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAttack;
                    @PrimaryAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAttack;
                    @PrimaryAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAttack;
                    @SpecialAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialAttack;
                    @SpecialAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialAttack;
                    @SpecialAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialAttack;
                    @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                    @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                    @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Dash.started += instance.OnDash;
                    @Dash.performed += instance.OnDash;
                    @Dash.canceled += instance.OnDash;
                    @WeaponRotationMouse.started += instance.OnWeaponRotationMouse;
                    @WeaponRotationMouse.performed += instance.OnWeaponRotationMouse;
                    @WeaponRotationMouse.canceled += instance.OnWeaponRotationMouse;
                    @WeaponRotationGamepad.started += instance.OnWeaponRotationGamepad;
                    @WeaponRotationGamepad.performed += instance.OnWeaponRotationGamepad;
                    @WeaponRotationGamepad.canceled += instance.OnWeaponRotationGamepad;
                    @PrimaryAttack.started += instance.OnPrimaryAttack;
                    @PrimaryAttack.performed += instance.OnPrimaryAttack;
                    @PrimaryAttack.canceled += instance.OnPrimaryAttack;
                    @SpecialAttack.started += instance.OnSpecialAttack;
                    @SpecialAttack.performed += instance.OnSpecialAttack;
                    @SpecialAttack.canceled += instance.OnSpecialAttack;
                    @Reload.started += instance.OnReload;
                    @Reload.performed += instance.OnReload;
                    @Reload.canceled += instance.OnReload;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // General
        private readonly InputActionMap m_General;
        private IGeneralActions m_GeneralActionsCallbackInterface;
        private readonly InputAction m_General_Pause;
        public struct GeneralActions
        {
            private @CustomInput m_Wrapper;
            public GeneralActions(@CustomInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Pause => m_Wrapper.m_General_Pause;
            public InputActionMap Get() { return m_Wrapper.m_General; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GeneralActions set) { return set.Get(); }
            public void SetCallbacks(IGeneralActions instance)
            {
                if (m_Wrapper.m_GeneralActionsCallbackInterface != null)
                {
                    @Pause.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPause;
                    @Pause.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPause;
                    @Pause.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPause;
                }
                m_Wrapper.m_GeneralActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Pause.started += instance.OnPause;
                    @Pause.performed += instance.OnPause;
                    @Pause.canceled += instance.OnPause;
                }
            }
        }
        public GeneralActions @General => new GeneralActions(this);
        public interface IPlayerActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnDash(InputAction.CallbackContext context);
            void OnWeaponRotationMouse(InputAction.CallbackContext context);
            void OnWeaponRotationGamepad(InputAction.CallbackContext context);
            void OnPrimaryAttack(InputAction.CallbackContext context);
            void OnSpecialAttack(InputAction.CallbackContext context);
            void OnReload(InputAction.CallbackContext context);
        }
        public interface IGeneralActions
        {
            void OnPause(InputAction.CallbackContext context);
        }
    }
}
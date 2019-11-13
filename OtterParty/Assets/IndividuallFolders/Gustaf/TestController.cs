// GENERATED AUTOMATICALLY FROM 'Assets/IndividuallFolders/Gustaf/TestController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class TestController : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public TestController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TestController"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""bb8cfe7a-a7eb-4f46-a628-cd463a89ff46"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""919c46cf-20fe-4691-a2f3-c04063ef6e82"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8d150e76-8206-47c6-bc44-3c50ab271bd3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""db3d9c05-7578-4f00-82e0-e3edec468394"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftSpam"",
                    ""type"": ""Button"",
                    ""id"": ""817991db-6130-41ff-b68b-ed424a80cc24"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightSpam"",
                    ""type"": ""Button"",
                    ""id"": ""97b00100-16a7-4080-ad2a-225092ea5e8b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchScene"",
                    ""type"": ""Button"",
                    ""id"": ""f4761744-a230-45dd-bdf4-43b4baa5e437"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shove"",
                    ""type"": ""Button"",
                    ""id"": ""f7e09ab9-5a67-4606-bf26-33e7dd4acb4a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2fa85812-051b-41a1-baf6-7eab7622bca5"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c83ed332-0a9c-4a87-bc00-1c7f1050ebf5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""323f8351-f832-4740-a234-205cea35ab78"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e65b9548-d5ff-4c47-84ab-c80adbfd218c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""20f5d499-6ea2-4003-9107-48aab862e551"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f7a93d43-ed84-45b4-8d3a-5c0802d4e395"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9e78a7b7-a1d2-4ff9-a7e6-afd88dbd83b0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba604ffb-e20f-4ff9-b31f-3f58d13890f0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8884788c-f1b9-42fa-9c8b-77f42608b65b"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b602bef-947a-47e4-bc20-7d8fe248055c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab879fdf-b18f-4c73-b5cb-5f5be09fb14c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5545226d-b171-4ebc-bdce-db76c834133e"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftSpam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19938ba1-a9c9-4623-a80a-41ef5dce4308"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftSpam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbea1ecd-c0fc-4649-9f1e-9ee6f25bffc6"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightSpam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e71588d0-cccf-4623-b1b3-fac018785772"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightSpam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71c06d89-fa7d-4c2d-9599-4138a99ee7ad"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""213cdc3c-1f40-4327-87e1-558953c354f1"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39858804-b2d1-4a9d-b8d9-9b7300e72665"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31478f70-f702-4b1b-8a24-ae9ad37b3ac5"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""OnlyController"",
            ""id"": ""a4a90b43-af69-4f6d-a3f4-452b1a5ab20b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""da40d3c6-e808-4364-847f-b014bc732751"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2b2cb4e3-d040-49a3-8f6d-bdff84772b47"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""b381f830-7eb2-493a-97a8-22821535c8dc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftSpam"",
                    ""type"": ""Button"",
                    ""id"": ""9c44e007-357d-4efb-8e76-993b9237723e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightSpam"",
                    ""type"": ""Button"",
                    ""id"": ""5d82a277-e815-4ffb-aec9-e4d530616715"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c1633a32-2e76-4b01-b293-6fa7f373f5ef"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f5b8d25-f43f-4f93-ad79-0d838e891b50"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""741e82f5-7020-4f68-9a52-2fc9292ef05c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44d45bce-bf6f-4e1b-ab97-2e58d74b4f65"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftSpam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9344d6e3-84c8-407d-bd7a-2ae3752b4d86"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightSpam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ReadyUP"",
            ""id"": ""9918e93c-761c-4394-9a33-f638cf17a860"",
            ""actions"": [
                {
                    ""name"": ""ReadyUp"",
                    ""type"": ""Button"",
                    ""id"": ""8dc71a04-8f2a-4d9f-83a2-36e0b3a1a532"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0e3a27d0-b511-4d5c-bd0c-9797f0b3a333"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReadyUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8260f46b-89f4-4fbf-a2d1-8d7e5d31adaf"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReadyUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Fire = m_Gameplay.FindAction("Fire", throwIfNotFound: true);
        m_Gameplay_LeftSpam = m_Gameplay.FindAction("LeftSpam", throwIfNotFound: true);
        m_Gameplay_RightSpam = m_Gameplay.FindAction("RightSpam", throwIfNotFound: true);
        m_Gameplay_SwitchScene = m_Gameplay.FindAction("SwitchScene", throwIfNotFound: true);
        m_Gameplay_Shove = m_Gameplay.FindAction("Shove", throwIfNotFound: true);
        // OnlyController
        m_OnlyController = asset.FindActionMap("OnlyController", throwIfNotFound: true);
        m_OnlyController_Move = m_OnlyController.FindAction("Move", throwIfNotFound: true);
        m_OnlyController_Jump = m_OnlyController.FindAction("Jump", throwIfNotFound: true);
        m_OnlyController_Fire = m_OnlyController.FindAction("Fire", throwIfNotFound: true);
        m_OnlyController_LeftSpam = m_OnlyController.FindAction("LeftSpam", throwIfNotFound: true);
        m_OnlyController_RightSpam = m_OnlyController.FindAction("RightSpam", throwIfNotFound: true);
        // ReadyUP
        m_ReadyUP = asset.FindActionMap("ReadyUP", throwIfNotFound: true);
        m_ReadyUP_ReadyUp = m_ReadyUP.FindAction("ReadyUp", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Fire;
    private readonly InputAction m_Gameplay_LeftSpam;
    private readonly InputAction m_Gameplay_RightSpam;
    private readonly InputAction m_Gameplay_SwitchScene;
    private readonly InputAction m_Gameplay_Shove;
    public struct GameplayActions
    {
        private TestController m_Wrapper;
        public GameplayActions(TestController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Fire => m_Wrapper.m_Gameplay_Fire;
        public InputAction @LeftSpam => m_Wrapper.m_Gameplay_LeftSpam;
        public InputAction @RightSpam => m_Wrapper.m_Gameplay_RightSpam;
        public InputAction @SwitchScene => m_Wrapper.m_Gameplay_SwitchScene;
        public InputAction @Shove => m_Wrapper.m_Gameplay_Shove;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Fire.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire;
                Fire.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire;
                Fire.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire;
                LeftSpam.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftSpam;
                LeftSpam.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftSpam;
                LeftSpam.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftSpam;
                RightSpam.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightSpam;
                RightSpam.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightSpam;
                RightSpam.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightSpam;
                SwitchScene.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchScene;
                SwitchScene.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchScene;
                SwitchScene.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchScene;
                Shove.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShove;
                Shove.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShove;
                Shove.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShove;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.canceled += instance.OnMove;
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                Fire.started += instance.OnFire;
                Fire.performed += instance.OnFire;
                Fire.canceled += instance.OnFire;
                LeftSpam.started += instance.OnLeftSpam;
                LeftSpam.performed += instance.OnLeftSpam;
                LeftSpam.canceled += instance.OnLeftSpam;
                RightSpam.started += instance.OnRightSpam;
                RightSpam.performed += instance.OnRightSpam;
                RightSpam.canceled += instance.OnRightSpam;
                SwitchScene.started += instance.OnSwitchScene;
                SwitchScene.performed += instance.OnSwitchScene;
                SwitchScene.canceled += instance.OnSwitchScene;
                Shove.started += instance.OnShove;
                Shove.performed += instance.OnShove;
                Shove.canceled += instance.OnShove;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // OnlyController
    private readonly InputActionMap m_OnlyController;
    private IOnlyControllerActions m_OnlyControllerActionsCallbackInterface;
    private readonly InputAction m_OnlyController_Move;
    private readonly InputAction m_OnlyController_Jump;
    private readonly InputAction m_OnlyController_Fire;
    private readonly InputAction m_OnlyController_LeftSpam;
    private readonly InputAction m_OnlyController_RightSpam;
    public struct OnlyControllerActions
    {
        private TestController m_Wrapper;
        public OnlyControllerActions(TestController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_OnlyController_Move;
        public InputAction @Jump => m_Wrapper.m_OnlyController_Jump;
        public InputAction @Fire => m_Wrapper.m_OnlyController_Fire;
        public InputAction @LeftSpam => m_Wrapper.m_OnlyController_LeftSpam;
        public InputAction @RightSpam => m_Wrapper.m_OnlyController_RightSpam;
        public InputActionMap Get() { return m_Wrapper.m_OnlyController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OnlyControllerActions set) { return set.Get(); }
        public void SetCallbacks(IOnlyControllerActions instance)
        {
            if (m_Wrapper.m_OnlyControllerActionsCallbackInterface != null)
            {
                Move.started -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnMove;
                Move.canceled -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnMove;
                Jump.started -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnJump;
                Fire.started -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnFire;
                Fire.performed -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnFire;
                Fire.canceled -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnFire;
                LeftSpam.started -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnLeftSpam;
                LeftSpam.performed -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnLeftSpam;
                LeftSpam.canceled -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnLeftSpam;
                RightSpam.started -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnRightSpam;
                RightSpam.performed -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnRightSpam;
                RightSpam.canceled -= m_Wrapper.m_OnlyControllerActionsCallbackInterface.OnRightSpam;
            }
            m_Wrapper.m_OnlyControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.canceled += instance.OnMove;
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                Fire.started += instance.OnFire;
                Fire.performed += instance.OnFire;
                Fire.canceled += instance.OnFire;
                LeftSpam.started += instance.OnLeftSpam;
                LeftSpam.performed += instance.OnLeftSpam;
                LeftSpam.canceled += instance.OnLeftSpam;
                RightSpam.started += instance.OnRightSpam;
                RightSpam.performed += instance.OnRightSpam;
                RightSpam.canceled += instance.OnRightSpam;
            }
        }
    }
    public OnlyControllerActions @OnlyController => new OnlyControllerActions(this);

    // ReadyUP
    private readonly InputActionMap m_ReadyUP;
    private IReadyUPActions m_ReadyUPActionsCallbackInterface;
    private readonly InputAction m_ReadyUP_ReadyUp;
    public struct ReadyUPActions
    {
        private TestController m_Wrapper;
        public ReadyUPActions(TestController wrapper) { m_Wrapper = wrapper; }
        public InputAction @ReadyUp => m_Wrapper.m_ReadyUP_ReadyUp;
        public InputActionMap Get() { return m_Wrapper.m_ReadyUP; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ReadyUPActions set) { return set.Get(); }
        public void SetCallbacks(IReadyUPActions instance)
        {
            if (m_Wrapper.m_ReadyUPActionsCallbackInterface != null)
            {
                ReadyUp.started -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnReadyUp;
                ReadyUp.performed -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnReadyUp;
                ReadyUp.canceled -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnReadyUp;
            }
            m_Wrapper.m_ReadyUPActionsCallbackInterface = instance;
            if (instance != null)
            {
                ReadyUp.started += instance.OnReadyUp;
                ReadyUp.performed += instance.OnReadyUp;
                ReadyUp.canceled += instance.OnReadyUp;
            }
        }
    }
    public ReadyUPActions @ReadyUP => new ReadyUPActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnLeftSpam(InputAction.CallbackContext context);
        void OnRightSpam(InputAction.CallbackContext context);
        void OnSwitchScene(InputAction.CallbackContext context);
        void OnShove(InputAction.CallbackContext context);
    }
    public interface IOnlyControllerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnLeftSpam(InputAction.CallbackContext context);
        void OnRightSpam(InputAction.CallbackContext context);
    }
    public interface IReadyUPActions
    {
        void OnReadyUp(InputAction.CallbackContext context);
    }
}

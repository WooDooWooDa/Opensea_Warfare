//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Resources/GameInputs.inputactions
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

public partial class @GameInputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInputs"",
    ""maps"": [
        {
            ""name"": ""BattleMap"",
            ""id"": ""b6ae9aa3-ce34-424b-a68c-2539a6e68797"",
            ""actions"": [
                {
                    ""name"": ""FireCommand"",
                    ""type"": ""Button"",
                    ""id"": ""cf7967f4-5b37-402d-80ae-6ea7600afd62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap(duration=0.2,pressPoint=0.3)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ab7846f9-648a-4905-bbff-499c127097a3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a06d0885-413d-4bd7-b391-15c51120d980"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""37176e5b-039b-4ac9-8e08-d8bc60b6f4d1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClickDrag"",
                    ""type"": ""PassThrough"",
                    ""id"": ""70a2dce9-d8b0-4d68-b313-31efd93e11c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.05,pressPoint=0.1)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""4a010419-96f0-4901-9e95-a0835dc127ec"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SpaceBar"",
                    ""type"": ""Button"",
                    ""id"": ""ff0f9bcb-77ad-4822-bb09-62d46376c26f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightTap"",
                    ""type"": ""Button"",
                    ""id"": ""89d1388d-b657-4cb1-b2a7-9cca8c1e05b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectShip"",
                    ""type"": ""Value"",
                    ""id"": ""d79ef1cc-64ec-4682-b23d-3a9390edab23"",
                    ""expectedControlType"": ""Key"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ScrollWheelClick"",
                    ""type"": ""Button"",
                    ""id"": ""50670dcd-401a-47d8-968a-2b1dcb0d97d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8e742ab7-6730-4698-9d0b-493a19fb23fc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireCommand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a6fdcbc-f8f7-4123-87e8-dfbed551ab42"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aaadcd2a-6338-4d0c-a472-79695c8fa685"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da3d9c4b-c60a-4d9c-98ad-bd41118fb026"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7aa9f24-4a08-4406-8fc9-359ef24cac58"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea5e94c4-5f68-4ab8-916d-4ed1b7653f20"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43bd8f40-c4da-4210-835b-bb64755b9dbc"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClickDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9511c305-3be3-447f-b25b-bdfde0baf837"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""86a46e56-609a-43ae-b0b4-f28c386d2a41"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d22f1c95-f6e0-4d5a-b6d4-1e45cdfdf9af"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b3b1fbac-be23-4525-bbc6-48adb01ec952"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""48b25534-bd70-4baa-a3a3-f224c24d938d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ecec6e45-a23a-4aaf-9d0c-e92710317aef"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ab7ca6e3-3482-4b68-b9db-fa3362783246"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""45251a4d-e586-47f8-9497-8a5612507f86"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""06770c8e-feec-4bd8-98ad-48e24cd6eaaf"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""42a4b952-80a0-45b5-89ae-09a5cd44cce6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a2152b4b-b056-4646-821b-b193a38b8b17"",
                    ""path"": ""<XRController>/{Primary2DAxis}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58cb8d99-74e3-47de-9bba-efef842cd71d"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e84b028-c8fa-4130-a261-e28acb84f711"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceBar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd896fd7-bec6-426f-a1b1-35c0c5726aad"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca2bceba-3c81-4744-84ee-e61091115f88"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""SelectShip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f797f604-6cd7-4050-9c62-ddf475c4b149"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""SelectShip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe738fea-1e63-4bcf-aa7a-d960f6657c06"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""SelectShip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbd9c8de-d4c2-45a6-85b6-bf6ecc89f8d6"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""SelectShip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f4cf880-ec19-4d1c-af86-0d9406d635e6"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""SelectShip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fa15d38-87d9-4091-9626-89df579d40bc"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""SelectShip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f17d54d-56b8-45c8-a289-182cafec77f6"",
                    ""path"": ""<Keyboard>/numpad7"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""SelectShip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd6546eb-3fd6-4db0-b5e1-e68c9b29236f"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""SelectShip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b9a2109-3d00-48da-9d45-188012f46b6c"",
                    ""path"": ""<Keyboard>/numpad9"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""SelectShip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""131d2dd8-8ddf-43e2-b85d-37629e0689df"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheelClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GodMap"",
            ""id"": ""1af0741a-071d-485d-995a-4e91c0854197"",
            ""actions"": [
                {
                    ""name"": ""PassCurrentMainObjective"",
                    ""type"": ""Button"",
                    ""id"": ""f857cabc-8eb3-4ae6-b4bc-b1e6dcc98cd1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""40c8d483-ea93-4d25-ada4-14d46296af9b"",
                    ""path"": ""<Keyboard>/rightCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PassCurrentMainObjective"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // BattleMap
        m_BattleMap = asset.FindActionMap("BattleMap", throwIfNotFound: true);
        m_BattleMap_FireCommand = m_BattleMap.FindAction("FireCommand", throwIfNotFound: true);
        m_BattleMap_Point = m_BattleMap.FindAction("Point", throwIfNotFound: true);
        m_BattleMap_ScrollWheel = m_BattleMap.FindAction("ScrollWheel", throwIfNotFound: true);
        m_BattleMap_RightClick = m_BattleMap.FindAction("RightClick", throwIfNotFound: true);
        m_BattleMap_RightClickDrag = m_BattleMap.FindAction("RightClickDrag", throwIfNotFound: true);
        m_BattleMap_Move = m_BattleMap.FindAction("Move", throwIfNotFound: true);
        m_BattleMap_SpaceBar = m_BattleMap.FindAction("SpaceBar", throwIfNotFound: true);
        m_BattleMap_RightTap = m_BattleMap.FindAction("RightTap", throwIfNotFound: true);
        m_BattleMap_SelectShip = m_BattleMap.FindAction("SelectShip", throwIfNotFound: true);
        m_BattleMap_ScrollWheelClick = m_BattleMap.FindAction("ScrollWheelClick", throwIfNotFound: true);
        // GodMap
        m_GodMap = asset.FindActionMap("GodMap", throwIfNotFound: true);
        m_GodMap_PassCurrentMainObjective = m_GodMap.FindAction("PassCurrentMainObjective", throwIfNotFound: true);
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

    // BattleMap
    private readonly InputActionMap m_BattleMap;
    private IBattleMapActions m_BattleMapActionsCallbackInterface;
    private readonly InputAction m_BattleMap_FireCommand;
    private readonly InputAction m_BattleMap_Point;
    private readonly InputAction m_BattleMap_ScrollWheel;
    private readonly InputAction m_BattleMap_RightClick;
    private readonly InputAction m_BattleMap_RightClickDrag;
    private readonly InputAction m_BattleMap_Move;
    private readonly InputAction m_BattleMap_SpaceBar;
    private readonly InputAction m_BattleMap_RightTap;
    private readonly InputAction m_BattleMap_SelectShip;
    private readonly InputAction m_BattleMap_ScrollWheelClick;
    public struct BattleMapActions
    {
        private @GameInputs m_Wrapper;
        public BattleMapActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @FireCommand => m_Wrapper.m_BattleMap_FireCommand;
        public InputAction @Point => m_Wrapper.m_BattleMap_Point;
        public InputAction @ScrollWheel => m_Wrapper.m_BattleMap_ScrollWheel;
        public InputAction @RightClick => m_Wrapper.m_BattleMap_RightClick;
        public InputAction @RightClickDrag => m_Wrapper.m_BattleMap_RightClickDrag;
        public InputAction @Move => m_Wrapper.m_BattleMap_Move;
        public InputAction @SpaceBar => m_Wrapper.m_BattleMap_SpaceBar;
        public InputAction @RightTap => m_Wrapper.m_BattleMap_RightTap;
        public InputAction @SelectShip => m_Wrapper.m_BattleMap_SelectShip;
        public InputAction @ScrollWheelClick => m_Wrapper.m_BattleMap_ScrollWheelClick;
        public InputActionMap Get() { return m_Wrapper.m_BattleMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattleMapActions set) { return set.Get(); }
        public void SetCallbacks(IBattleMapActions instance)
        {
            if (m_Wrapper.m_BattleMapActionsCallbackInterface != null)
            {
                @FireCommand.started -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnFireCommand;
                @FireCommand.performed -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnFireCommand;
                @FireCommand.canceled -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnFireCommand;
                @Point.started -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnPoint;
                @ScrollWheel.started -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnScrollWheel;
                @RightClick.started -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnRightClick;
                @RightClickDrag.started -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnRightClickDrag;
                @RightClickDrag.performed -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnRightClickDrag;
                @RightClickDrag.canceled -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnRightClickDrag;
                @Move.started -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnMove;
                @SpaceBar.started -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnSpaceBar;
                @SpaceBar.performed -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnSpaceBar;
                @SpaceBar.canceled -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnSpaceBar;
                @RightTap.started -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnRightTap;
                @RightTap.performed -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnRightTap;
                @RightTap.canceled -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnRightTap;
                @SelectShip.started -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnSelectShip;
                @SelectShip.performed -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnSelectShip;
                @SelectShip.canceled -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnSelectShip;
                @ScrollWheelClick.started -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnScrollWheelClick;
                @ScrollWheelClick.performed -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnScrollWheelClick;
                @ScrollWheelClick.canceled -= m_Wrapper.m_BattleMapActionsCallbackInterface.OnScrollWheelClick;
            }
            m_Wrapper.m_BattleMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FireCommand.started += instance.OnFireCommand;
                @FireCommand.performed += instance.OnFireCommand;
                @FireCommand.canceled += instance.OnFireCommand;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @RightClickDrag.started += instance.OnRightClickDrag;
                @RightClickDrag.performed += instance.OnRightClickDrag;
                @RightClickDrag.canceled += instance.OnRightClickDrag;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @SpaceBar.started += instance.OnSpaceBar;
                @SpaceBar.performed += instance.OnSpaceBar;
                @SpaceBar.canceled += instance.OnSpaceBar;
                @RightTap.started += instance.OnRightTap;
                @RightTap.performed += instance.OnRightTap;
                @RightTap.canceled += instance.OnRightTap;
                @SelectShip.started += instance.OnSelectShip;
                @SelectShip.performed += instance.OnSelectShip;
                @SelectShip.canceled += instance.OnSelectShip;
                @ScrollWheelClick.started += instance.OnScrollWheelClick;
                @ScrollWheelClick.performed += instance.OnScrollWheelClick;
                @ScrollWheelClick.canceled += instance.OnScrollWheelClick;
            }
        }
    }
    public BattleMapActions @BattleMap => new BattleMapActions(this);

    // GodMap
    private readonly InputActionMap m_GodMap;
    private IGodMapActions m_GodMapActionsCallbackInterface;
    private readonly InputAction m_GodMap_PassCurrentMainObjective;
    public struct GodMapActions
    {
        private @GameInputs m_Wrapper;
        public GodMapActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @PassCurrentMainObjective => m_Wrapper.m_GodMap_PassCurrentMainObjective;
        public InputActionMap Get() { return m_Wrapper.m_GodMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GodMapActions set) { return set.Get(); }
        public void SetCallbacks(IGodMapActions instance)
        {
            if (m_Wrapper.m_GodMapActionsCallbackInterface != null)
            {
                @PassCurrentMainObjective.started -= m_Wrapper.m_GodMapActionsCallbackInterface.OnPassCurrentMainObjective;
                @PassCurrentMainObjective.performed -= m_Wrapper.m_GodMapActionsCallbackInterface.OnPassCurrentMainObjective;
                @PassCurrentMainObjective.canceled -= m_Wrapper.m_GodMapActionsCallbackInterface.OnPassCurrentMainObjective;
            }
            m_Wrapper.m_GodMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PassCurrentMainObjective.started += instance.OnPassCurrentMainObjective;
                @PassCurrentMainObjective.performed += instance.OnPassCurrentMainObjective;
                @PassCurrentMainObjective.canceled += instance.OnPassCurrentMainObjective;
            }
        }
    }
    public GodMapActions @GodMap => new GodMapActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IBattleMapActions
    {
        void OnFireCommand(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnRightClickDrag(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSpaceBar(InputAction.CallbackContext context);
        void OnRightTap(InputAction.CallbackContext context);
        void OnSelectShip(InputAction.CallbackContext context);
        void OnScrollWheelClick(InputAction.CallbackContext context);
    }
    public interface IGodMapActions
    {
        void OnPassCurrentMainObjective(InputAction.CallbackContext context);
    }
}

// GENERATED AUTOMATICALLY FROM 'Assets/IndividuallFolders/Gustaf/PlayerControllerMapper.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerControllerMapper : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public PlayerControllerMapper()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControllerMapper"",
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
                    ""name"": ""CancelReadyUp"",
                    ""type"": ""Button"",
                    ""id"": ""1dd6374a-7f32-43e2-9d41-86f804fd5029"",
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
                },
                {
                    ""name"": ""ReadyUp"",
                    ""type"": ""Button"",
                    ""id"": ""1d05a755-839f-4732-8e7f-41c6967bdea3"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""daaa0806-ca81-4876-824f-445d404b4bed"",
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
                    ""id"": ""15beed30-a76b-444c-a88e-caabdaac3eff"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReadyUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97fc5fe9-d2b2-4e47-92f8-00203c1cfe8f"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelReadyUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8826967-8979-4c63-a239-db9e0856fb79"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelReadyUp"",
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
                },
                {
                    ""name"": ""ShiftLeft"",
                    ""type"": ""Button"",
                    ""id"": ""dec33edc-ac95-4671-8b94-ce5d3a722efd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShiftRight"",
                    ""type"": ""Button"",
                    ""id"": ""881dd64e-4b0d-4c4a-8e50-05c742f70d1f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CancelReadyUp"",
                    ""type"": ""Button"",
                    ""id"": ""e4679d20-083d-450d-b0b7-ea1751dd89af"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""76d7595c-c474-4d2f-9e33-84323da81785"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23b6c3c9-0ff2-4bb8-8655-01e0ec4cb29f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""159d8143-672e-4308-a613-48619b113069"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f1802e9-757c-4566-ac98-3372695e3799"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69d8d3df-0f17-4a6c-bf07-a588fe7e0bd2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba541692-e54f-416c-b993-8e4abe3797fe"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0881bc92-05ac-4582-b3e3-0f9defda089b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelReadyUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46ca92a5-64bb-4244-85bc-7e96e782caff"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelReadyUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Credits"",
            ""id"": ""84162e61-b732-496d-87c5-373c86eb9b46"",
            ""actions"": [
                {
                    ""name"": ""Leave"",
                    ""type"": ""Button"",
                    ""id"": ""feda2942-4ebc-4e35-82b3-e15e463f471d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShiftRight"",
                    ""type"": ""Button"",
                    ""id"": ""7ac7ad93-bfc1-4915-b9e4-70c49eaddf9d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShiftLeft"",
                    ""type"": ""Button"",
                    ""id"": ""785dc203-9aff-43d9-90cc-37a76846bce5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""04cda2e5-64b1-45b4-a867-a6ad56241886"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e0d9deb-47e9-4021-838b-36621a83b22e"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26a41b98-ca62-4f91-8b11-4a204409c930"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc99f540-248a-422a-b6a9-a294c3ef2166"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59597690-9b7a-4110-be20-ae9ce83c3aa4"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""094d6a34-b3f3-4ab2-8e3f-7860f2315de8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d257a0a-09ce-4b35-bdc8-4f15d3592a5c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftRight"",
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
        m_Gameplay_CancelReadyUp = m_Gameplay.FindAction("CancelReadyUp", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Fire = m_Gameplay.FindAction("Fire", throwIfNotFound: true);
        m_Gameplay_LeftSpam = m_Gameplay.FindAction("LeftSpam", throwIfNotFound: true);
        m_Gameplay_RightSpam = m_Gameplay.FindAction("RightSpam", throwIfNotFound: true);
        m_Gameplay_SwitchScene = m_Gameplay.FindAction("SwitchScene", throwIfNotFound: true);
        m_Gameplay_Shove = m_Gameplay.FindAction("Shove", throwIfNotFound: true);
        m_Gameplay_ReadyUp = m_Gameplay.FindAction("ReadyUp", throwIfNotFound: true);
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
        m_ReadyUP_ShiftLeft = m_ReadyUP.FindAction("ShiftLeft", throwIfNotFound: true);
        m_ReadyUP_ShiftRight = m_ReadyUP.FindAction("ShiftRight", throwIfNotFound: true);
        m_ReadyUP_CancelReadyUp = m_ReadyUP.FindAction("CancelReadyUp", throwIfNotFound: true);
        // Credits
        m_Credits = asset.FindActionMap("Credits", throwIfNotFound: true);
        m_Credits_Leave = m_Credits.FindAction("Leave", throwIfNotFound: true);
        m_Credits_ShiftRight = m_Credits.FindAction("ShiftRight", throwIfNotFound: true);
        m_Credits_ShiftLeft = m_Credits.FindAction("ShiftLeft", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_CancelReadyUp;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Fire;
    private readonly InputAction m_Gameplay_LeftSpam;
    private readonly InputAction m_Gameplay_RightSpam;
    private readonly InputAction m_Gameplay_SwitchScene;
    private readonly InputAction m_Gameplay_Shove;
    private readonly InputAction m_Gameplay_ReadyUp;
    public struct GameplayActions
    {
        private PlayerControllerMapper m_Wrapper;
        public GameplayActions(PlayerControllerMapper wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @CancelReadyUp => m_Wrapper.m_Gameplay_CancelReadyUp;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Fire => m_Wrapper.m_Gameplay_Fire;
        public InputAction @LeftSpam => m_Wrapper.m_Gameplay_LeftSpam;
        public InputAction @RightSpam => m_Wrapper.m_Gameplay_RightSpam;
        public InputAction @SwitchScene => m_Wrapper.m_Gameplay_SwitchScene;
        public InputAction @Shove => m_Wrapper.m_Gameplay_Shove;
        public InputAction @ReadyUp => m_Wrapper.m_Gameplay_ReadyUp;
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
                CancelReadyUp.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancelReadyUp;
                CancelReadyUp.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancelReadyUp;
                CancelReadyUp.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancelReadyUp;
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
                ReadyUp.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReadyUp;
                ReadyUp.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReadyUp;
                ReadyUp.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReadyUp;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.canceled += instance.OnMove;
                CancelReadyUp.started += instance.OnCancelReadyUp;
                CancelReadyUp.performed += instance.OnCancelReadyUp;
                CancelReadyUp.canceled += instance.OnCancelReadyUp;
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
                ReadyUp.started += instance.OnReadyUp;
                ReadyUp.performed += instance.OnReadyUp;
                ReadyUp.canceled += instance.OnReadyUp;
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
        private PlayerControllerMapper m_Wrapper;
        public OnlyControllerActions(PlayerControllerMapper wrapper) { m_Wrapper = wrapper; }
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
    private readonly InputAction m_ReadyUP_ShiftLeft;
    private readonly InputAction m_ReadyUP_ShiftRight;
    private readonly InputAction m_ReadyUP_CancelReadyUp;
    public struct ReadyUPActions
    {
        private PlayerControllerMapper m_Wrapper;
        public ReadyUPActions(PlayerControllerMapper wrapper) { m_Wrapper = wrapper; }
        public InputAction @ReadyUp => m_Wrapper.m_ReadyUP_ReadyUp;
        public InputAction @ShiftLeft => m_Wrapper.m_ReadyUP_ShiftLeft;
        public InputAction @ShiftRight => m_Wrapper.m_ReadyUP_ShiftRight;
        public InputAction @CancelReadyUp => m_Wrapper.m_ReadyUP_CancelReadyUp;
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
                ShiftLeft.started -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnShiftLeft;
                ShiftLeft.performed -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnShiftLeft;
                ShiftLeft.canceled -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnShiftLeft;
                ShiftRight.started -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnShiftRight;
                ShiftRight.performed -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnShiftRight;
                ShiftRight.canceled -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnShiftRight;
                CancelReadyUp.started -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnCancelReadyUp;
                CancelReadyUp.performed -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnCancelReadyUp;
                CancelReadyUp.canceled -= m_Wrapper.m_ReadyUPActionsCallbackInterface.OnCancelReadyUp;
            }
            m_Wrapper.m_ReadyUPActionsCallbackInterface = instance;
            if (instance != null)
            {
                ReadyUp.started += instance.OnReadyUp;
                ReadyUp.performed += instance.OnReadyUp;
                ReadyUp.canceled += instance.OnReadyUp;
                ShiftLeft.started += instance.OnShiftLeft;
                ShiftLeft.performed += instance.OnShiftLeft;
                ShiftLeft.canceled += instance.OnShiftLeft;
                ShiftRight.started += instance.OnShiftRight;
                ShiftRight.performed += instance.OnShiftRight;
                ShiftRight.canceled += instance.OnShiftRight;
                CancelReadyUp.started += instance.OnCancelReadyUp;
                CancelReadyUp.performed += instance.OnCancelReadyUp;
                CancelReadyUp.canceled += instance.OnCancelReadyUp;
            }
        }
    }
    public ReadyUPActions @ReadyUP => new ReadyUPActions(this);

    // Credits
    private readonly InputActionMap m_Credits;
    private ICreditsActions m_CreditsActionsCallbackInterface;
    private readonly InputAction m_Credits_Leave;
    private readonly InputAction m_Credits_ShiftRight;
    private readonly InputAction m_Credits_ShiftLeft;
    public struct CreditsActions
    {
        private PlayerControllerMapper m_Wrapper;
        public CreditsActions(PlayerControllerMapper wrapper) { m_Wrapper = wrapper; }
        public InputAction @Leave => m_Wrapper.m_Credits_Leave;
        public InputAction @ShiftRight => m_Wrapper.m_Credits_ShiftRight;
        public InputAction @ShiftLeft => m_Wrapper.m_Credits_ShiftLeft;
        public InputActionMap Get() { return m_Wrapper.m_Credits; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CreditsActions set) { return set.Get(); }
        public void SetCallbacks(ICreditsActions instance)
        {
            if (m_Wrapper.m_CreditsActionsCallbackInterface != null)
            {
                Leave.started -= m_Wrapper.m_CreditsActionsCallbackInterface.OnLeave;
                Leave.performed -= m_Wrapper.m_CreditsActionsCallbackInterface.OnLeave;
                Leave.canceled -= m_Wrapper.m_CreditsActionsCallbackInterface.OnLeave;
                ShiftRight.started -= m_Wrapper.m_CreditsActionsCallbackInterface.OnShiftRight;
                ShiftRight.performed -= m_Wrapper.m_CreditsActionsCallbackInterface.OnShiftRight;
                ShiftRight.canceled -= m_Wrapper.m_CreditsActionsCallbackInterface.OnShiftRight;
                ShiftLeft.started -= m_Wrapper.m_CreditsActionsCallbackInterface.OnShiftLeft;
                ShiftLeft.performed -= m_Wrapper.m_CreditsActionsCallbackInterface.OnShiftLeft;
                ShiftLeft.canceled -= m_Wrapper.m_CreditsActionsCallbackInterface.OnShiftLeft;
            }
            m_Wrapper.m_CreditsActionsCallbackInterface = instance;
            if (instance != null)
            {
                Leave.started += instance.OnLeave;
                Leave.performed += instance.OnLeave;
                Leave.canceled += instance.OnLeave;
                ShiftRight.started += instance.OnShiftRight;
                ShiftRight.performed += instance.OnShiftRight;
                ShiftRight.canceled += instance.OnShiftRight;
                ShiftLeft.started += instance.OnShiftLeft;
                ShiftLeft.performed += instance.OnShiftLeft;
                ShiftLeft.canceled += instance.OnShiftLeft;
            }
        }
    }
    public CreditsActions @Credits => new CreditsActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnCancelReadyUp(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnLeftSpam(InputAction.CallbackContext context);
        void OnRightSpam(InputAction.CallbackContext context);
        void OnSwitchScene(InputAction.CallbackContext context);
        void OnShove(InputAction.CallbackContext context);
        void OnReadyUp(InputAction.CallbackContext context);
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
        void OnShiftLeft(InputAction.CallbackContext context);
        void OnShiftRight(InputAction.CallbackContext context);
        void OnCancelReadyUp(InputAction.CallbackContext context);
    }
    public interface ICreditsActions
    {
        void OnLeave(InputAction.CallbackContext context);
        void OnShiftRight(InputAction.CallbackContext context);
        void OnShiftLeft(InputAction.CallbackContext context);
    }
}

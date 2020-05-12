using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// This script will assign new keycodes on on either a button click or controller button 0 being pressed. There are a no known bugs.
/// Pressing return will invoke OnButtonClick(). Pressing return a second time will continue the loop until either a new button is pressed,
/// Or a mouse button clicked. Clicking the button with a mouse will invoke the OnButtonClick() but follow it's own subsystem. Pressing 
/// button 0 on a gamepad will invoke the OnButtonClick() but follows its own subsystem. Pressing button 1 will cancel the button remapping.
/// The gamepad subsystem is entirely segregated from the keyboard and mouse subsystems. The keyboard and mouse subsystem have an overlap.
/// </summary>

public class ButtonRemapping : MonoBehaviour, IPointerClickHandler
{
    public Button Button;
    private KeyCode Keycode;
    private KeyCode OldKeycode;
    private int Index;
    private GameManager Gamemanager;

    private bool IsButtonPressed = false;

    Event KeyEvent;

    private void Awake()
    {
        Button = this.gameObject.GetComponent<Button>();
        Index = this.transform.GetSiblingIndex();
    }

    //private void OnEnable()
    //{
    //    Button.onClick.AddListener(delegate { OnButtonClick(); });
    //}

    public void Start()
    {
        Gamemanager = FindObjectOfType<GameManager>();
        Keycode = Gamemanager.Keys[Index];

        if(Keycode != KeyCode.None)
        {
            Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
        }
        else
        {
            if(Gamemanager.Movement[Index] == "Horizontal")
            {
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Thumbstick Horizontal";
            }
            else if (Gamemanager.Movement[Index] == "Vertical")
            {
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Thumbstick Vertical";
            }
            else
            {
                Button.transform.GetChild(0).GetComponent<Text>().text = Gamemanager.Movement[Index];
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Checkpoints.CanClose == false)
        {
            Checkpoints.CanClose = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Checkpoints.CanClose = false;

        OldKeycode = Gamemanager.Keys[Index];
        Button.transform.GetChild(0).GetComponent<Text>().text = "Please enter a new key";

        Gamemanager.Speak(this.transform.GetComponentInChildren<Text>().text);

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            StartCoroutine(GetNewJoystickButton());
        }

        else if (IsButtonPressed == false)
        {
            StartCoroutine(GetNewKey());
        }
    }

    private void OnGUI()
    {
        KeyEvent = Event.current;

        if (KeyEvent.isKey && IsButtonPressed == true)
        {
            if (KeyEvent.keyCode != KeyCode.Return && KeyEvent.keyCode != KeyCode.KeypadEnter)
            {
                if (KeyEvent.keyCode != KeyCode.Escape)
                {
                    Keycode = KeyEvent.keyCode;

                    Gamemanager.Keys[Index] = Keycode;

                    if (Gamemanager.Keys[Index] == KeyCode.None)
                    {
                        Gamemanager.Keys[Index] = OldKeycode;
                    }

                    string keytext = Keycode.ToString();
                    int stringlength = keytext.Length;

                    if (stringlength > 5 && stringlength < 7)
                    {
                        if (keytext == "SysReq")
                        {
                            Button.transform.GetChild(0).GetComponent<Text>().text = "System Requirements";
                        }
                        else
                        {
                            keytext = keytext.Substring(0, 5) + " " + keytext.Substring(5, stringlength - 5);

                            Button.transform.GetChild(0).GetComponent<Text>().text = keytext;
                        }
                    }
                    else
                    {
                        Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                    }

                    IsButtonPressed = false;
                }
                else
                {
                    Keycode = Gamemanager.Keys[Index];
                    Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                    IsButtonPressed = false;
                }
            }
        }

        if (KeyEvent.isMouse && IsButtonPressed == true)
        {
            if (KeyEvent.button == 0)
            {
                Keycode = KeyCode.Mouse0;
            }
            else if (KeyEvent.button == 1)
            {
                Keycode = KeyCode.Mouse1;
            }
            else if (KeyEvent.button == 2)
            {
                Keycode = KeyCode.Mouse2;
            }

            Gamemanager.Keys[Index] = Keycode;

            string keytext = Keycode.ToString();
            int stringlength = keytext.Length;
            keytext = keytext.Substring(0, 5) + " " + keytext.Substring(5, stringlength - 5);

            Button.transform.GetChild(0).GetComponent<Text>().text = keytext;

            IsButtonPressed = false;
        }
    }

    public IEnumerator GetNewKey()
    {
        IsButtonPressed = true;
        yield return WaitForKey();

        StopCoroutine(GetNewKey());
    }

    IEnumerator WaitForKey()
    {
        while (!KeyEvent.isKey)
        {
            yield return null;
        }
    }

    private IEnumerator GetNewJoystickButton()
    {
        IsButtonPressed = true;
        yield return WaitForJoystick();

        StopCoroutine(GetNewJoystickButton());
    }

    IEnumerator WaitForJoystick()
    {
        while (IsButtonPressed == true)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                IsButtonPressed = false;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton2;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Action 1";
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton3;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Action 2";
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton4;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Bumper";
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton5;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Bumper";
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton6))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton6;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Status";
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton7;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Pause";
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton8))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton8;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Analogue Button";
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton9))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton9;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Analogue Button";
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton10;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton11))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton11;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton12))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton12;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton13))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton13;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton14))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton14;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton15))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton15;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton16))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton16;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton17))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton17;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton18))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton18;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton19))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton19;
                Gamemanager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
            }

            if (Input.GetAxis("Horizontal") != 0.0f)
            {
                IsButtonPressed = false;

                Gamemanager.Movement[Index] = "Horizontal";

                Gamemanager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Thumbstick Horizontal";
            }

            if (Input.GetAxis("Vertical") != 0.0f)
            {
                IsButtonPressed = false;

                Gamemanager.Movement[Index] = "Vertical";

                Gamemanager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Thumbstick Vertical";
            }

            if (Input.GetAxis("Right Thumbstick Horizontal") != 0.0f)
            {
                IsButtonPressed = false;

                Gamemanager.Movement[Index] = "Right Thumbstick Horizontal";

                Gamemanager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Thumbstick Horizontal";
            }

            if (Input.GetAxis("Right Thumbstick Vertical") != 0.0f)
            {
                IsButtonPressed = false;

                Gamemanager.Movement[Index] = "Right Thumbstick Vertical";

                Gamemanager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Thumbstick Vertical";
            }

            else if (Input.GetAxis("Left Trigger") != 0.0f)
            {
                IsButtonPressed = false;

                Gamemanager.Movement[Index] = "Left Trigger";

                Gamemanager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Trigger";
            }

            else if (Input.GetAxis("Right Trigger") != 0.0f)
            {
                IsButtonPressed = false;

                Gamemanager.Movement[Index] = "Right Trigger";

                Gamemanager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Trigger";
            }

            else if (Input.GetAxis("DPad Vertical") != 0.0f)
            {
                IsButtonPressed = false;

                Gamemanager.Movement[Index] = "DPad Vertical";

                Gamemanager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "DPad Vertical";
            }

            else if (Input.GetAxis("DPad Horizontal") != 0.0f)
            {
                IsButtonPressed = false;

                Gamemanager.Movement[Index] = "Dpad Horizontal";

                Gamemanager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "DPad Horizontal";
            }

            yield return null;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ButtonRemapping : MonoBehaviour
{
    /*This script will assign new keycodes on on either a button click or controller button 0 being pressed. There are a no known bugs.                                    */
    /*Pressing return will invoke OnButtonClick(). Pressing return a second time will continue the loop until either a new button is pressed. Or a mouse button clicked.   */
    /*Clicking the button with a mouse will invoke the OnButtonClick() but follow it's own subsystem.                                                                      */
    /*Pressing button 0 on a gamepad will invoke the OnButtonClick() but follows its own subsystem. Pressing button 1 will cancel the button remapping.                    */
    /*The gamepad subsystem is entirely segregated from the keyboard and mouse subsystems. The keyboard and mouse subsystem have an overlap.                               */

    public Button Button;
    private KeyCode Keycode;
    private KeyCode OldKeycode;
    private int Index;
    private GameManager GameManager;

    private bool IsButtonPressed = false;

    Event KeyEvent;

    private void Awake()
    {
        Button = this.gameObject.GetComponent<Button>();
        Index = this.transform.GetSiblingIndex();
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(delegate { OnButtonClick(); });
    }

    private void Start()
    {
        GameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Keycode = GameManager.Keys[Index];

        if(Keycode != KeyCode.None)
        {
            Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
        }
        else
        {
            if(GameManager.Movement[Index] == "Horizontal")
            {
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Thumbstick Horizontal";
            }
            else if (GameManager.Movement[Index] == "Vertical")
            {
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Thumbstick Vertical";
            }
            else
            {
                Button.transform.GetChild(0).GetComponent<Text>().text = GameManager.Movement[Index];
            }
        }
    }

    public void OnButtonClick()
    {
        OldKeycode = GameManager.Keys[Index];
        Button.transform.GetChild(0).GetComponent<Text>().text = "Please enter a new key";

        GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);

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

                    GameManager.Keys[Index] = Keycode;

                    if (GameManager.Keys[Index] == KeyCode.None)
                    {
                        GameManager.Keys[Index] = OldKeycode;
                    }

                    string keytext = Keycode.ToString();
                    int stringlength = keytext.Length;

                    if (stringlength > 5 && stringlength < 7)
                    {
                        if (keytext == "SysReq")
                        {
                            Button.transform.GetChild(0).GetComponent<Text>().text = "System Requirements";
                            GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
                        }
                        else
                        {
                            keytext = keytext.Substring(0, 5) + " " + keytext.Substring(5, stringlength - 5);

                            Button.transform.GetChild(0).GetComponent<Text>().text = keytext;
                            GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
                        }
                    }
                    else
                    {
                        Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                        GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
                    }

                    IsButtonPressed = false;
                }
                else
                {
                    GameManager.Speak("Cancel");
                    Keycode = GameManager.Keys[Index];
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

            GameManager.Keys[Index] = Keycode;

            string keytext = Keycode.ToString();
            int stringlength = keytext.Length;
            keytext = keytext.Substring(0, 5) + " " + keytext.Substring(5, stringlength - 5);

            Button.transform.GetChild(0).GetComponent<Text>().text = keytext;
            GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);

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
                GameManager.Speak("Cancel");
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton2;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Action 1";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton3;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Action 2";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton4;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Bumper";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton5;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Bumper";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton6))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton6;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Status";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton7;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Pause";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton8))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton8;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Analogue Button";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton9))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton9;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Analogue Button";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton10;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton11))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton11;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton12))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton12;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton13))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton13;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton14))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton14;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton15))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton15;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton16))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton16;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton17))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton17;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton18))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton18;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton19))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton19;
                GameManager.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            if (Input.GetAxis("Horizontal") != 0.0f)
            {
                IsButtonPressed = false;

                GameManager.Movement[Index] = "Horizontal";

                GameManager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Thumbstick Horizontal";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            if (Input.GetAxis("Vertical") != 0.0f)
            {
                IsButtonPressed = false;

                GameManager.Movement[Index] = "Vertical";

                GameManager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Thumbstick Vertical";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            if (Input.GetAxis("Right Thumbstick Horizontal") != 0.0f)
            {
                IsButtonPressed = false;

                GameManager.Movement[Index] = "Right Thumbstick Horizontal";

                GameManager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Thumbstick Horizontal";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            if (Input.GetAxis("Right Thumbstick Vertical") != 0.0f)
            {
                IsButtonPressed = false;

                GameManager.Movement[Index] = "Right Thumbstick Vertical";

                GameManager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Thumbstick Vertical";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetAxis("Left Trigger") != 0.0f)
            {
                IsButtonPressed = false;

                GameManager.Movement[Index] = "Left Trigger";

                GameManager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Trigger";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetAxis("Right Trigger") != 0.0f)
            {
                IsButtonPressed = false;

                GameManager.Movement[Index] = "Right Trigger";

                GameManager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Trigger";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetAxis("DPad Vertical") != 0.0f)
            {
                IsButtonPressed = false;

                GameManager.Movement[Index] = "DPad Vertical";

                GameManager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "DPad Vertical";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetAxis("DPad Horizontal") != 0.0f)
            {
                IsButtonPressed = false;

                GameManager.Movement[Index] = "Dpad Horizontal";

                GameManager.Keys[Index] = KeyCode.None;
                Button.transform.GetChild(0).GetComponent<Text>().text = "DPad Horizontal";
                GameManager.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            yield return null;
        }
    }
}

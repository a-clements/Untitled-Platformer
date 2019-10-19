using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonPositioner : MonoBehaviour
{
    private Button[] Buttons;
    private float ButtonHeight;
    private float ButtonWidth;
    private float Position;
    private float Space;
    [SerializeField] private float ButtonPositionMultiplier;
    /*If Buttons.Length > 3 ButtonPositionMultiplier = 0                      */
    /*If Buttons.Length = 3 ButtonPositionMultiplier = .3                     */
    /*If Buttons.Length < 3 ButtonPositionMultiplier = add .55 for each button*/

    public void Positioner()
    {
        Array.Resize(ref Buttons, this.transform.childCount);

        ButtonHeight = this.GetComponent<RectTransform>().rect.height / (Buttons.Length + 1);

        ButtonWidth = this.GetComponent<RectTransform>().rect.width * 0.5f;

        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i] = this.transform.GetChild(i).GetComponent<Button>();
            Buttons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(ButtonWidth, ButtonHeight);
        }

        if (Buttons.Length > 2)
        {
            Position = (this.GetComponent<RectTransform>().rect.height - (ButtonHeight * Buttons.Length));
        }

        else
        {
            Position = (this.GetComponent<RectTransform>().rect.height - (ButtonHeight * Buttons.Length)) / Buttons.Length;
        }

        Space = Position / Buttons.Length;

        if (Buttons.Length > 1)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, ((Space * (Buttons.Length * ButtonPositionMultiplier) + Position)));
                Position -= (Space + ButtonHeight);
            }
        }

        else
        {
            Buttons[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
    }
}
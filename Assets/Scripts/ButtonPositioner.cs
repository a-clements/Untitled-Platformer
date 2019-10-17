using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonPositioner : MonoBehaviour
{
    [SerializeField] private Button[] Buttons;
    [SerializeField] private float ButtonHeight;
    [SerializeField] private float ButtonWidth;
    [SerializeField] private float Position;
    [SerializeField] private float Space;

    public void Positioner()
    {
        Array.Resize(ref Buttons, this.transform.childCount);

        ButtonHeight = this.GetComponent<RectTransform>().rect.height / (Buttons.Length + 1);

        ButtonWidth = this.GetComponent<RectTransform>().rect.width * 0.5f;

        if (Buttons.Length > 2)
        {
            Position = (this.GetComponent<RectTransform>().rect.height - (ButtonHeight * Buttons.Length));
        }

        else
        {
            Position = (this.GetComponent<RectTransform>().rect.height - (ButtonHeight * Buttons.Length)) / Buttons.Length;
        }

        Space = Position / Buttons.Length;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Buttons[i] = this.transform.GetChild(i).GetComponent<Button>();
            Buttons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(ButtonWidth, ButtonHeight);

            if(Buttons.Length > 1)
            {
                Buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (Space + Position));
            }

            else
            {
                Buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            }

            Position -= (Space + ButtonHeight);
        }
    }
}
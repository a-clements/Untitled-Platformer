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

    public void Positioner()
    {
        Array.Resize(ref Buttons, this.transform.childCount);

        ButtonHeight = this.GetComponent<RectTransform>().rect.height / (Buttons.Length + 1);

        ButtonWidth = this.GetComponent<RectTransform>().rect.width * 0.5f;

        Position = ButtonHeight * Buttons.Length;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Buttons[i] = this.transform.GetChild(i).GetComponent<Button>();
            Buttons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(ButtonWidth, ButtonHeight);
            Buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, ButtonHeight - Position);
            Position -= ButtonHeight;
        }
    }
}
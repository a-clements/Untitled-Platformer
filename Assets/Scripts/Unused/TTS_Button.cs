using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// This script detects a mouse over event on a button.
/// </summary>

public class TTS_Button : MonoBehaviour, IPointerEnterHandler
{
    private GameManager GameManager;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.enabled == true)
        {
            GameManager.Speak(transform.GetChild(0).GetComponent<Text>().text);
        }
    }
}
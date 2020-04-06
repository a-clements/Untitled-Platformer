﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LinkClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().color = Color.yellow;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Application.OpenURL(this.GetComponent<Text>().text);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Text>().color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Text>().color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

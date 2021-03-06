﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This sets the checkpoint on the player move script to be this transform. This script is also used in the training level to activate and
/// deactivate panels. If the PanelOne variable is blank and CanShow is false, then no training panels will be activated.
/// </summary>

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private GameObject PanelOne;
    public GameObject PanelTwo;
    public GameObject PanelThree;

    [SerializeField] private bool CanShow = true;
    [SerializeField] private EventSystem GetEventSystem;
    [SerializeField] private GameManager Manager;
    public static bool CanClose = true;

    //Event KeyEvent;

    private void Start()
    {
        Time.timeScale = 1;
        Manager = FindObjectOfType<GameManager>();
        GetEventSystem = FindObjectOfType<EventSystem>();

        if(GameManager.IsMicrophone == true && this.transform.name == "Entry")
        {
            PanelTwo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = " At the top left of the UI, above your hearts, is the charge metre of your special ability. " +
            "Killing enemies increases the charge on your special ability. You can only use your special ability when it is fully charged. You will need a microphone to use your special ability.";

            PanelThree.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "On the bottom right of the UI there are two numbers. " +
            "The furthest to the right is the current volume of your microphone input. " +
            "To the left of that is a threshold. To activate your special ability the input volume must be greater than the threshold.";
        }

        else if (GameManager.IsMicrophone == false && this.transform.name == "Entry")
        {
            PanelTwo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = " At the top left of the UI, above your hearts, is the charge metre of your special ability. " +
            "Killing enemies increases the charge on your special ability. You can only use your special ability when it is fully charged by pressing the " + Manager.Keys[5] + 
            " key to activate your ability.";

            PanelThree.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Press the " + Manager.Keys[5] + " key to activate your ability.";
        }
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.gameObject.tag == "Player")
        {

            TriggerInfo.GetComponent<PlayerMove>().Checkpoint = this.transform;
            TriggerInfo.GetComponent<Animator>().SetBool("IsWalking", false);
            ScoreManager.SaveScores();

            if (PanelOne != null && CanShow == true)
            {
                PanelOne.SetActive(true);
                GetEventSystem.firstSelectedGameObject = PanelOne;
                PanelOne.transform.GetChild(2).GetChild(0).GetComponent<Button>().Select();
                Time.timeScale = 0;
                CanShow = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.gameObject.tag == "Player")
        {
            if (PanelOne != null)
            {
                PanelOne.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    private void Update()
    {
        if (CanClose == true)
        {
            if (PanelOne != null)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (PanelOne.activeSelf || PanelTwo.activeSelf || PanelThree.activeSelf)
                    {
                        Manager.SaveSettings();
                        StartCoroutine(ClosePanel());
                    }
                    else
                    {
                        if(LivesManager.LivesRemaining > -1)
                        {
                            PanelOne.SetActive(true);
                            Time.timeScale = 0;
                        }
                    }
                }
            }
        }
    }

    //private void OnGUI()
    //{
    //    KeyEvent = Event.current;

    //    if(PanelOne != null)
    //    {
    //        if (KeyEvent.isKey && PanelOne.activeSelf || KeyEvent.isKey && PanelTwo.activeSelf || KeyEvent.isKey && PanelThree.activeSelf)
    //        {
    //            if (KeyEvent.keyCode == KeyCode.Escape)
    //            {
    //                StartCoroutine(ClosePanel());
    //            }
    //            else
    //            {
    //                PanelOne.SetActive(true);
    //            }
    //        }
    //    }
    //}

    IEnumerator ClosePanel()
    {
        yield return null;

        if (PanelOne != null)
        {
            PanelOne.SetActive(false);
            PanelTwo.SetActive(false);
            PanelThree.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private GameObject PanelOne;
    public GameObject PanelTwo;
    public GameObject PanelThree;
    private bool CanShow = true;
    [SerializeField] EventSystem GetEventSystem;
    public static bool CanClose = true;

    //Event KeyEvent;

    private void Start()
    {
        GetEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
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
                PanelOne.transform.GetChild(1).GetChild(0).GetComponent<Button>().Select();
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
                        StartCoroutine(ClosePanel());
                    }
                    else
                    {
                        PanelOne.SetActive(true);
                        Time.timeScale = 0;
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

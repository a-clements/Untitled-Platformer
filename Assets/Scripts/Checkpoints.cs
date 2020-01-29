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
    [SerializeField] EventSystem GetEventSystem;

    Event KeyEvent;

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

            if (PanelOne != null)
            {
                PanelOne.SetActive(true);
                GetEventSystem.firstSelectedGameObject = PanelOne;
                PanelOne.transform.GetChild(0).GetComponent<Button>().Select();
                Time.timeScale = 0;
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

    private void OnGUI()
    {
        KeyEvent = Event.current;

        if(PanelOne != null)
        {
            if (KeyEvent.isKey && PanelOne.activeSelf || KeyEvent.isKey && PanelTwo.activeSelf || KeyEvent.isKey && PanelThree.activeSelf)
            {
                if (KeyEvent.keyCode == KeyCode.Escape)
                {
                    StartCoroutine(ClosePanel());
                }
            }
        }
    }

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

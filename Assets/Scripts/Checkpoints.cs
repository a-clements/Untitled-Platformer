using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
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
            ScoreManager.SaveScores();

            if (Panel != null)
            {
                Panel.SetActive(true);
                GetEventSystem.firstSelectedGameObject = Panel.transform.GetChild(0).gameObject;
                Panel.transform.GetChild(0).GetComponent<Button>().Select();
                Time.timeScale = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.gameObject.tag == "Player")
        {
            if (Panel != null)
            {
                Panel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    private void OnGUI()
    {
        KeyEvent = Event.current;

        if(Panel != null)
        {
            if (KeyEvent.isKey && Panel.activeSelf)
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

        if (Panel != null)
        {
            Panel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

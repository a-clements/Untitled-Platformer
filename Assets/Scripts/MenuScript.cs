using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject LeftPanel;
    public GameObject RightPanel;
    public GameObject BottomPanel;

    [Header("Panel Speed")]
    [SerializeField] private float SidePanelSpeed = 1.0f;
    [SerializeField] private float BottomPanelSpeed = 1.0f;

    [Header("Panel Multiplier")]
    [SerializeField] private float SidePanelSlideInMultiplier = 1.5f;
    [SerializeField] private float BottomPanelSlideInMultiplier = 1.5f;
    [SerializeField] private float SidePanelSlideOutMultiplier = 2.5f;
    [SerializeField] private float BottomPanelSlideOutMultiplier = 2.5f;

    [Header("Coroutine Timers")]
    [SerializeField] private float WaitTimer = 0.01f;

    private bool Running = true;

    void Start()
    {
        GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);

        #region Left Panel Resize
        LeftPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<CanvasScaler>().referenceResolution.x * 0.25f, GetComponent<CanvasScaler>().referenceResolution.y);

        for (int i = 0; i < LeftPanel.transform.childCount; i++)
        {

            LeftPanel.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(LeftPanel.GetComponent<RectTransform>().rect.width * 0.5f,
            LeftPanel.GetComponent<RectTransform>().rect.height / LeftPanel.transform.childCount);
        }
        #endregion

        #region Right Panel Resize
        RightPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<CanvasScaler>().referenceResolution.x * 0.25f, GetComponent<CanvasScaler>().referenceResolution.y);

        for (int i = 0; i < RightPanel.transform.childCount; i++)
        {

            RightPanel.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(RightPanel.GetComponent<RectTransform>().rect.width * 0.5f,
            RightPanel.GetComponent<RectTransform>().rect.height / RightPanel.transform.childCount);
        }
        #endregion

        #region Bottom Panel Resize
        BottomPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<CanvasScaler>().referenceResolution.x * 0.5f,
            GetComponent<CanvasScaler>().referenceResolution.y * 0.25f);

        BottomPanel.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(BottomPanel.GetComponent<RectTransform>().rect.width * 0.5f, 
            BottomPanel.GetComponent<RectTransform>().rect.height * 0.5f);
        #endregion

        StartCoroutine(ScrollIn());
    }

    public void OnPlayButtonClick()
    {
        StartCoroutine(ScrollOut());
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        if(Running == false)
        {
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
            yield return null;
        }

        else
        {
            yield return new WaitForSeconds(1.0f);
            yield return null;
            StartCoroutine(LoadScene());
        }
    }

    #region Panel Enumerators do NOT touch
    private IEnumerator ScrollIn()
    {
        StartCoroutine(SidePanelScrollIn());
        StartCoroutine(BottomPanelScrollIn());
        yield return null;
    }

    IEnumerator SidePanelScrollIn()
    {
        float n;
        float i;

        yield return new WaitForSeconds(WaitTimer);

        n = LeftPanel.GetComponent<RectTransform>().localPosition.x;
        i = RightPanel.GetComponent<RectTransform>().localPosition.x;

        while (LeftPanel.GetComponent<RectTransform>().localPosition.x < -(LeftPanel.GetComponent<RectTransform>().rect.width * SidePanelSlideInMultiplier) && 
            RightPanel.GetComponent<RectTransform>().localPosition.x > (RightPanel.GetComponent<RectTransform>().rect.width * SidePanelSlideInMultiplier))
        {
            LeftPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(n, 0);
            n = n + SidePanelSpeed;

            RightPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(i, 0);
            i = i - SidePanelSpeed;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerator BottomPanelScrollIn()
    {
        float n;

        yield return new WaitForSeconds(WaitTimer);

        n = BottomPanel.GetComponent<RectTransform>().localPosition.y;

        while (BottomPanel.GetComponent<RectTransform>().localPosition.y < -(BottomPanel.GetComponent<RectTransform>().rect.height * BottomPanelSlideInMultiplier))
        {
            BottomPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, n);
            n = n + BottomPanelSpeed;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    private IEnumerator ScrollOut()
    {
        StartCoroutine(SidePanelScrollOut());
        StartCoroutine(BottomPanelScrollOut());
        yield return new WaitForSeconds(1.0f);
        Running = false;
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator SidePanelScrollOut()
    {
        float n;
        float i;

        yield return new WaitForSeconds(WaitTimer);

        n = LeftPanel.GetComponent<RectTransform>().localPosition.x;
        i = RightPanel.GetComponent<RectTransform>().localPosition.x;

        while (LeftPanel.GetComponent<RectTransform>().localPosition.x > -(LeftPanel.GetComponent<RectTransform>().rect.width * SidePanelSlideOutMultiplier) &&
            RightPanel.GetComponent<RectTransform>().localPosition.x < (RightPanel.GetComponent<RectTransform>().rect.width * SidePanelSlideOutMultiplier))
        {
            LeftPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(n, 0);
            n = n - SidePanelSpeed;

            RightPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(i, 0);
            i = i + SidePanelSpeed;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerator BottomPanelScrollOut()
    {
        float n;

        yield return new WaitForSeconds(WaitTimer);

        n = BottomPanel.GetComponent<RectTransform>().localPosition.y;

        while (BottomPanel.GetComponent<RectTransform>().localPosition.y > -(BottomPanel.GetComponent<RectTransform>().rect.height * BottomPanelSlideOutMultiplier))
        {
            BottomPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, n);
            n = n - BottomPanelSpeed;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        StartCoroutine(ScrollIn());
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
        yield return null;
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

        Debug.Log(-(BottomPanel.GetComponent<RectTransform>().rect.height * BottomPanelSlideOutMultiplier));

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

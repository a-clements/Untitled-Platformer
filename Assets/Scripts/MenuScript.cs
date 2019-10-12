using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject LeftPanel;
    public GameObject RightPanel;
    public GameObject BottomPanel;
    [SerializeField] private float SidePanelSpeed = 1.0f;
    [SerializeField] private float BottomPanelSpeed = 1.0f;
    [SerializeField] private float SidePanelMultiplier = 1.5f;
    [SerializeField] private float BottomPanelMultipler = 1.5f;
    [SerializeField] private float WaitTimer = 0.01f;

    void Start()
    {
        StartCoroutine(ScrollIn());
    }

    private IEnumerator ScrollIn()
    {
        StartCoroutine(LeftPanelScrollIn());
        StartCoroutine(LeftRightScrollIn());
        StartCoroutine(BottomPanelScrollIn());
        yield return null;
    }

    IEnumerator LeftPanelScrollIn()
    {
        float n;

        yield return new WaitForSeconds(WaitTimer);

        n = LeftPanel.GetComponent<RectTransform>().localPosition.x;
        while (LeftPanel.GetComponent<RectTransform>().localPosition.x < -(LeftPanel.GetComponent<RectTransform>().rect.width * SidePanelMultiplier))
        {
            LeftPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(n, 0);
            n = n + SidePanelSpeed;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerator LeftRightScrollIn()
    {
        float n;

        yield return new WaitForSeconds(WaitTimer);

        n = RightPanel.GetComponent<RectTransform>().localPosition.x;

        while (RightPanel.GetComponent<RectTransform>().localPosition.x > (RightPanel.GetComponent<RectTransform>().rect.width * SidePanelMultiplier))
        {
            RightPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(n, 0);
            n = n - SidePanelSpeed;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerator BottomPanelScrollIn()
    {
        float n;

        yield return new WaitForSeconds(WaitTimer);

        n = BottomPanel.GetComponent<RectTransform>().localPosition.y;

        while (BottomPanel.GetComponent<RectTransform>().localPosition.y < -(BottomPanel.GetComponent<RectTransform>().rect.height * SidePanelMultiplier))
        {
            BottomPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, n);
            n = n + BottomPanelSpeed;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    private IEnumerator ScrollOut()
    {
        yield return null;
    }
}

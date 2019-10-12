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

        yield return new WaitForSeconds(0.01f);

        n = LeftPanel.GetComponent<RectTransform>().localPosition.x;

        while (LeftPanel.GetComponent<RectTransform>().localPosition.x < -786)
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

        yield return new WaitForSeconds(0.01f);

        n = RightPanel.GetComponent<RectTransform>().localPosition.x;

        while (RightPanel.GetComponent<RectTransform>().localPosition.x > 786)
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

        yield return new WaitForSeconds(0.01f);

        n = BottomPanel.GetComponent<RectTransform>().localPosition.y;

        while (BottomPanel.GetComponent<RectTransform>().localPosition.y < -372)
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

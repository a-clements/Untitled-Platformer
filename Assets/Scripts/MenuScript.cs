using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Animator PanelAnimator;

    [Header("Game Objects")]
    public GameObject Panels;

    [Header("Coroutine Timers")]
    [SerializeField] private float WaitTimer = 0.01f;

    [Header("Coroutine Multiplier")]
    [SerializeField] private float[] WaitTimeMultiplier;

    [Header("Scene Name")]
    [SerializeField] private string SceneName;

    private bool Running = true;

    private void OnEnable()
    {
        Panels.transform.GetChild(0).GetComponent<ButtonPositioner>().Positioner();

        Panels.transform.GetChild(1).GetComponent<ButtonPositioner>().Positioner();

        Panels.transform.GetChild(2).GetComponent<ButtonPositioner>().Positioner();
    }

    void Start()
    {
        StartCoroutine(ScrollIn());
    }

    public void OnPlayButtonClick()
    {
        StartCoroutine(ScrollOut());
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        LivesManager.LivesRemaining = 3;

        if(Running == false)
        {
            while(PanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Scroll Out"))
            {
                yield return null;
            }

            SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
        }

        else
        {
            yield return new WaitForSeconds(WaitTimer);
            yield return null;
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator ScrollIn()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[0]);
        PanelAnimator.SetTrigger("Scroll In");
        //yield return null;
    }

    private IEnumerator ScrollOut()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[1]);
        PanelAnimator.SetTrigger("Scroll Out");
        Running = false;
        yield return new WaitForSeconds(WaitTimer);
    }
}

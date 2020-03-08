using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls how the panels on the main menu slide in and out. The designer can determine the length of the wait timer, how many
/// wait time multipliers there are, and the scene that will load when the play button is pressed. This script is also used to set the
/// start lives of the player and resets the level score when the play button is pressed.
/// </summary>

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

    [Header("Starting Lives")]
    [SerializeField] int StartingLives = 3;

    private bool Running = true;
    private bool Options = false;

    private GameManager Manager;

    private void OnEnable()
    {
        Manager = FindObjectOfType<GameManager>();

        Panels.transform.GetChild(0).GetComponent<ButtonPositioner>().Positioner();

        Panels.transform.GetChild(1).GetComponent<ButtonPositioner>().Positioner();

        Panels.transform.GetChild(2).GetComponent<ButtonPositioner>().Positioner();
    }

    void Start()
    {
        StartCoroutine(MainScrollIn());
    }

    public void OnOptionsButtonClick()
    {
        Options = !Options;

        if (Options == true)
        {
            StartCoroutine(MainScrollOut());
            StartCoroutine(OptionsScrollIn());
        }

        else
        {
            Manager.SaveSettings();
            StartCoroutine(OptionsScrollOut());
            StartCoroutine(MainScrollIn());
        }
    }

    public void OnPlayButtonClick()
    {
        StartCoroutine(MainScrollOut());
        StartCoroutine(LoadScene());
    }

    public void OnQuitButtonClick()
    {
        StartCoroutine(MainScrollOut());

        #if UNITY_STANDALONE
            Application.Quit();
        #endif

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    IEnumerator OptionsScrollIn()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[0]);
        PanelAnimator.SetTrigger("Options Scroll In");
    }

    IEnumerator OptionsScrollOut()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[1]);
        PanelAnimator.SetTrigger("Options Scroll Out");
        Running = true;
        yield return new WaitForSeconds(WaitTimer);
        StartCoroutine(MainScrollIn());
    }

    IEnumerator LoadScene()
    {
        ScoreManager.LevelScore = 0;
        LivesManager.LivesRemaining = StartingLives;
        MapManager.Counter = 0;

        if(Running == false)
        {
            while(PanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Main Scroll Out"))
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

    private IEnumerator MainScrollIn()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[0]);
        PanelAnimator.SetTrigger("Main Scroll In");
        //yield return null;
    }

    private IEnumerator MainScrollOut()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[1]);
        PanelAnimator.SetTrigger("Main Scroll Out");
        Running = false;
        yield return new WaitForSeconds(WaitTimer);
    }
}
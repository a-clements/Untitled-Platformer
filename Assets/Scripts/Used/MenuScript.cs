using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// This script controls how the panels on the main menu slide in and out. The designer can determine the length of the wait timer, how many
/// wait time multipliers there are, and the scene that will load when the play button is pressed. This script is also used to set the
/// start lives of the player and resets the level score when the play button is pressed.
/// </summary>

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Animator PanelAnimator;

    [Header("Game Objects")]
    [SerializeField] private GameObject Panels;
    [SerializeField] private GameObject ShoutControl;
    [SerializeField] private Text VersionNumber;

    [Header("Coroutine Timers")]
    [SerializeField] private float WaitTimer = 0.01f;

    [Header("Coroutine Multiplier")]
    [SerializeField] private float[] WaitTimeMultiplier;

    [Header("Scene Name")]
    [SerializeField] private string SceneName;

    [Header("Starting Lives")]
    [SerializeField] int StartingLives = 3;

    [SerializeField] private EventSystem GetEventSystem;

    private bool Running = true;
    private bool Options = false;
    private bool Credits = false;

    private GameManager Manager;

    private void OnEnable()
    {
        Manager = FindObjectOfType<GameManager>();
        GetEventSystem = FindObjectOfType<EventSystem>();

        Panels.transform.GetChild(0).GetChild(0).GetComponent<ButtonPositioner>().Positioner();

        Panels.transform.GetChild(1).GetChild(0).GetComponent<ButtonPositioner>().Positioner();

        Panels.transform.GetChild(2).GetChild(0).GetComponent<ButtonPositioner>().Positioner();

        GetEventSystem.firstSelectedGameObject = ShoutControl;
        ShoutControl.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>().Select();

        Time.timeScale = 1;

        VersionNumber.text = "Version: " + Application.version;
    }

    void Start()
    {
        StartCoroutine(MainScrollIn());
    }

    public void OnCreditsButtonClick()
    {
        Credits = !Credits;

        if (Credits == true)
        {
            StartCoroutine(MainScrollOut());
            StartCoroutine(CreditsScrollIn());

            Panels.transform.GetChild(4).GetChild(0).GetChild(6).GetComponent<Button>().Select();
        }

        else
        {
            Manager.SaveSettings();
            StartCoroutine(CreditsScrollOut());
            StartCoroutine(MainScrollIn());

            Panels.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Button>().Select();
        }
    }

    public void OnOptionsButtonClick()
    {
        Options = !Options;

        if (Options == true)
        {
            StartCoroutine(MainScrollOut());
            StartCoroutine(OptionsScrollIn());

            Panels.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>().Select();
        }

        else
        {
            Manager.SaveSettings();
            StartCoroutine(OptionsScrollOut());
            StartCoroutine(MainScrollIn());

            Panels.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Button>().Select();
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

    IEnumerator CreditsScrollIn()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[0]);
        PanelAnimator.SetBool("Credits", true);
    }

    IEnumerator CreditsScrollOut()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[1]);
        PanelAnimator.SetBool("Credits", false);
        Running = true;
        yield return new WaitForSeconds(WaitTimer);
        StartCoroutine(MainScrollIn());
    }

    IEnumerator OptionsScrollIn()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[0]);
        PanelAnimator.SetBool("Options", true);
    }

    IEnumerator OptionsScrollOut()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[1]);
        PanelAnimator.SetBool("Options", false);
        Running = true;
        yield return new WaitForSeconds(WaitTimer);
        StartCoroutine(MainScrollIn());
    }

    private IEnumerator MainScrollIn()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[0]);
        PanelAnimator.SetBool("Main", true);
        yield return null;
    }

    private IEnumerator MainScrollOut()
    {
        yield return new WaitForSeconds(WaitTimer * WaitTimeMultiplier[1]);
        PanelAnimator.SetBool("Main", false);
        Running = false;
        yield return new WaitForSeconds(WaitTimer);
    }

    IEnumerator LoadScene()
    {
        ScoreManager.LevelScore = 0;
        LivesManager.LivesRemaining = StartingLives;
        MapManager.Counter = 0;
        MapManager.MapOneComplete = false;
        MapManager.MapTwoComplete = false;

        if (Running == false)
        {
            while (PanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Main Scroll Out"))
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
}
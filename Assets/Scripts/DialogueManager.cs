using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> Dialogue = new Queue<string>();
    // Start is called before the first frame update

    public static DialogueManager Instance = null;

    [SerializeField] private Text DialogueText;
    [SerializeField] private string SceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Dialogue.Clear();

        foreach(string Sentence in dialogue.Sentences)
        {
            Dialogue.Enqueue(Sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(Dialogue.Count == 0)
        {
            EndDialogue();
            return;
        }

        else
        {
            StopAllCoroutines();
            StartCoroutine(Typing(Dialogue.Dequeue()));
        }
    }

    void EndDialogue()
    {
        SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
    }

    IEnumerator Typing(string dialogue)
    {
        DialogueText.text = "";

        yield return new WaitForFixedUpdate();

        foreach (char letter in dialogue.ToCharArray())
        {
            DialogueText.text += letter;

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    void Start()
    {

    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            DisplayNextSentence();
        }
    }
}

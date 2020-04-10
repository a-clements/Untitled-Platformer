using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script types a dialogue letter by letter at a rate determined by the WaitTimer, which is defined during design time. This script
/// will enqueue and dequeue dialogue and load the next scene once all dialogue has been displayed.
/// </summary>

public class DialogueManager : MonoBehaviour
{
    private Queue<string> Dialogue = new Queue<string>();
    private int i = 0;
    private int j;

    [SerializeField]private string[] Names;
    [SerializeField] private GameObject[] DialogueBox;
    [SerializeField] private Text DialogueSpeaker;
    [SerializeField] private Text DialogueText;
    [SerializeField] private string SceneName;
    [SerializeField] private float WaitTimer = 0.5f;

    public void StartDialogue(Dialogue dialogue)
    {
        j = 0;
        Dialogue.Clear();
        Names = dialogue.Name;
        DialogueSpeaker.text = Names[j];

        foreach (string Sentence in dialogue.Sentences)
        {
            Dialogue.Enqueue(Sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (Dialogue.Count == 0)
        {
            if (i < DialogueBox.Length - 1)
            {
                DialogueBox[i].SetActive(false);
                i++;
                DialogueSpeaker = DialogueBox[i].transform.GetChild(0).GetChild(1).GetComponent<Text>();
                DialogueText = DialogueBox[i].transform.GetChild(1).GetChild(1).GetComponent<Text>();
                DialogueBox[i].SetActive(true);
            }

            else
            {
                EndDialogue();
            }
        }

        else
        {
            if (Names.Length > 1)
            {
                j++;
            }

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

        yield return new WaitForSeconds(WaitTimer);

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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EndDialogue();
        }
        else if (Input.anyKeyDown)
        {
            if (Dialogue.Count > 0)
            {
                DialogueSpeaker.text = Names[j];
            }

            else
            {
                j = 0;
            }

            DisplayNextSentence();
        }
    }
}
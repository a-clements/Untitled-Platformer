
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private string SceneName;
	void Start ()
    {

	}

    private void OnTriggerStay2D(Collider2D TriggerInfo)
    {
        if(TriggerInfo.tag == "Player")
        {
            ScoreManager.SaveScores();
            SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
        }
    }
}

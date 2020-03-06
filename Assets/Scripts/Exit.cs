
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private string FinalCutScene;
	void Start ()
    {

	}

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if(TriggerInfo.tag == "Player")
        {
            ScoreManager.SaveScores();
            MapManager.Counter += 1;

            if (MapManager.Counter > 2)
            {
                SceneManager.LoadSceneAsync(FinalCutScene, LoadSceneMode.Single);
            }

            else
            {
                SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            }
        }
    }
}

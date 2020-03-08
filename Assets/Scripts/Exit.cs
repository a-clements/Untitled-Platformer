
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script increments the amount of cut scenes that have been played. It will also send the player back to the mapselection scene if
/// there are more cutscenes left so that the player can go to a new area of the map. If the all the intermediate cutscenes have been viewed,
/// the player will be sent to the final cutscene.
/// </summary>

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

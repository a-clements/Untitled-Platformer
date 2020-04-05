using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script plays a designer defined cut scene when a button is clicked. This script also keeps track of how many cut scenes have played.
/// </summary>

public class MapManager : MonoBehaviour
{
    public static bool MapOneComplete = false;
    public static bool MapTwoComplete = false;

    [SerializeField] private Button MapOneButton;
    [SerializeField] private Button MapTwoButton;
    [SerializeField] private string Map1;
    [SerializeField] private string Map2;
    [SerializeField] private Sprite MapCompletedSprite;
    public static int Counter;

    // Start is called before the first frame update
    void Start()
    {
        if(MapOneComplete == true)
        {
            MapOneButton.enabled = false;
            MapOneButton.gameObject.GetComponent<ButtonSounds>().enabled = false;
            MapOneButton.gameObject.GetComponent<Image>().sprite = MapCompletedSprite;
        }

        if (MapTwoComplete == true)
        {
            MapTwoButton.enabled = false;
            MapTwoButton.gameObject.GetComponent<ButtonSounds>().enabled = false;
            MapTwoButton.gameObject.GetComponent<Image>().sprite = MapCompletedSprite;
        }
    }

    public void ZoneOne()
    {
        SceneManager.LoadSceneAsync(Map1, LoadSceneMode.Single);
    }

    public void ZoneTwo()
    {
        SceneManager.LoadSceneAsync(Map2, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

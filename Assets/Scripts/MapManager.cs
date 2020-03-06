using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField] private string MapOne;
    [SerializeField] private string MapTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ZoneOne()
    {
        SceneManager.LoadSceneAsync(MapOne, LoadSceneMode.Single);
    }

    public void ZoneTwo()
    {
        SceneManager.LoadSceneAsync(MapTwo, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

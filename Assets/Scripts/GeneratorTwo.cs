using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTwo : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject DirtPrefab;
    [SerializeField] private GameObject GrassPrefab;
    [SerializeField] private GameObject StonePrefab;
    [SerializeField] private GameObject HazardPrefab;
    [SerializeField] private GameObject BridgePrefab;

    [Header("Variables")]
    [SerializeField] private int Height = 4;
    [SerializeField] private int Width = 120;
    [SerializeField] private int MaxHazards = 3;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceofHazard = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceofBridge = 0.1f;

    [Header("Number of Platforms")]
    [SerializeField] private int NumerOfPlatforms = 100;

    private int w = 0;
    private bool IsHazard;
    private GameObject Map;
    private GameObject Stone;
    private GameObject Dirt;
    private GameObject Grass;
    private GameObject Bridge;
    private GameObject Hazard;

    void OnEnable()
    {
        Map = new GameObject("Map");
        Generate();
    }

    private void OnDisable()
    {
        Destroy(Map);
    }

    private void Generate()
    {
        #region Method Two First Block
        Stone = Instantiate(StonePrefab, new Vector3(0, -2), Quaternion.identity) as GameObject;
        Instantiate(DirtPrefab, new Vector3(0, -1), Quaternion.identity);
        Instantiate(GrassPrefab, new Vector3(0, 0), Quaternion.identity);
        #endregion

        #region Method Two Loop
        for (int w = 1; w < Width; w++)
        {
            if (Random.value < ChanceofBridge)
            {
                Instantiate(BridgePrefab, new Vector3(w, 0), Quaternion.identity);
            }

            else if (Random.value < ChanceofHazard)
            {
                Instantiate(StonePrefab, new Vector3(w, -2), Quaternion.identity);
                Instantiate(DirtPrefab, new Vector3(w, -1), Quaternion.identity);
                Instantiate(HazardPrefab, new Vector3(w, 0), Quaternion.identity);
            }

            else
            {
                Instantiate(StonePrefab, new Vector3(w, -2), Quaternion.identity);
                Instantiate(DirtPrefab, new Vector3(w, -1), Quaternion.identity);
                Instantiate(GrassPrefab, new Vector3(w, 0), Quaternion.identity);
            }

            if (w < NumerOfPlatforms + 1)
            {
                PlacePlatform(Random.Range(0, 2), w, Random.Range(2, Height + 1));
            }
        }
        #endregion

        #region Method Two Last Block
        Instantiate(StonePrefab, new Vector3(Width, -2), Quaternion.identity);
        Instantiate(DirtPrefab, new Vector3(Width, -1), Quaternion.identity);
        Instantiate(GrassPrefab, new Vector3(Width, 0), Quaternion.identity);
        #endregion
    }

    void PlacePlatform(int value, int w, int h)
    {
        if (value < 1)
        {
            if (Random.value < ChanceofHazard)
            {
                Instantiate(HazardPrefab, new Vector3(w, h), Quaternion.identity);
            }

            else if (Random.value < ChanceofBridge)
            {
                Instantiate(BridgePrefab, new Vector3(w, h), Quaternion.identity);
            }

            else
            {
                Instantiate(GrassPrefab, new Vector3(w, h), Quaternion.identity);
            }
        }
    }
}

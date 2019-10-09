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
    [SerializeField] private int Width = 120;
    [SerializeField] private int MinBridgeSize = 1;
    [SerializeField] private int MaxBridgeSize = 3;
    [SerializeField] private int MinHazardSize = 1;
    [SerializeField] private int MaxHazardSize = 3;
    [SerializeField] private int LowPlatformOffset = 2;
    [SerializeField] private int HighPlatformOffset = 5;

    [Header("Chance Sliders")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceOfHazard = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceOfBridge = 0.1f;

    [Header("Number of Platforms")]
    [SerializeField] private int NumerOfPlatforms = 100;

    private int w = 1;
    private int i = 0;
    private bool IsHazard;
    private int BridgeSize;
    private int HazardSize;
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
        Dirt = Instantiate(DirtPrefab, new Vector3(0, -1), Quaternion.identity) as GameObject;
        Grass = Instantiate(GrassPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;

        Stone.transform.SetParent(Map.transform);
        Dirt.transform.SetParent(Map.transform);
        Grass.transform.SetParent(Map.transform);
        #endregion

        #region Method Two Loop
        while (w < Width)
        {
            if (Random.value < ChanceOfBridge)
            {
                BridgeSize = Mathf.RoundToInt(Random.Range(MinBridgeSize, MaxBridgeSize));
                for(i = 0; i < BridgeSize; i++)
                {
                    Bridge = Instantiate(BridgePrefab, new Vector3(w, 0), Quaternion.identity) as GameObject;
                    Bridge.transform.SetParent(Map.transform);
                    w++;
                }
            }

            if (IsHazard == true)
            {
                IsHazard = false;
            }

            else if (Random.value < ChanceOfHazard)
            {
                IsHazard = true;
            }

            else
            {
                IsHazard = false;
            }

            if (IsHazard == true)
            {
                HazardSize = Mathf.RoundToInt(Random.Range(MinHazardSize, MaxHazardSize));

                for(i = 0; i < HazardSize; i++)
                {
                    Stone = Instantiate(StonePrefab, new Vector3(w, -2), Quaternion.identity) as GameObject;
                    Dirt = Instantiate(DirtPrefab, new Vector3(w, -1), Quaternion.identity) as GameObject;
                    Hazard = Instantiate(HazardPrefab, new Vector3(w, 0), Quaternion.identity) as GameObject;

                    Stone.transform.SetParent(Map.transform);
                    Dirt.transform.SetParent(Map.transform);
                    Hazard.transform.SetParent(Map.transform);
                    w++;
                }
            }

            else
            {
                HazardSize = Mathf.RoundToInt(Random.Range(MinHazardSize, MaxHazardSize));

                for (i = 0; i < HazardSize; i++)
                {
                    Stone = Instantiate(StonePrefab, new Vector3(w, -2), Quaternion.identity) as GameObject;
                    Dirt = Instantiate(DirtPrefab, new Vector3(w, -1), Quaternion.identity) as GameObject;
                    Grass = Instantiate(GrassPrefab, new Vector3(w, 0), Quaternion.identity) as GameObject;

                    Stone.transform.SetParent(Map.transform);
                    Dirt.transform.SetParent(Map.transform);
                    Grass.transform.SetParent(Map.transform);
                    w++;
                }
            }
        }

        for(i = 0; i < NumerOfPlatforms; i++)
        {
            PlacePlatform(Random.Range(0, 2), i, Random.Range(LowPlatformOffset, HighPlatformOffset));
        }
        #endregion

        #region Method Two Last Block
        Stone = Instantiate(StonePrefab, new Vector3(Width, -2), Quaternion.identity) as GameObject;
        Dirt = Instantiate(DirtPrefab, new Vector3(Width, -1), Quaternion.identity) as GameObject;
        Grass = Instantiate(GrassPrefab, new Vector3(Width, 0), Quaternion.identity) as GameObject;

        Stone.transform.SetParent(Map.transform);
        Dirt.transform.SetParent(Map.transform);
        Grass.transform.SetParent(Map.transform);
        #endregion
    }

    void PlacePlatform(int value, int w, int h)
    {
        if (value < 1)
        {
            if (IsHazard == true)
            {
                IsHazard = false;
            }

            else if (Random.value < ChanceOfHazard)
            {
                IsHazard = true;
            }

            else
            {
                IsHazard = false;
            }

            if (IsHazard == true)
            {
                Instantiate(HazardPrefab, new Vector3(w, h), Quaternion.identity);
            }

            else if (Random.value < ChanceOfBridge)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorOne : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject DirtPrefab;
    [SerializeField] private GameObject GrassPrefab;
    [SerializeField] private GameObject StonePrefab;
    [SerializeField] private GameObject[] HazardPrefab;
    [SerializeField] private GameObject BridgePrefab;

    [Header("Variables")]
    [SerializeField] private int Height = 30;
    [SerializeField] private int Width = 120;
    [SerializeField] private int SpacerMinimum = 12;
    [SerializeField] private int SpacerMaximum = 24;
    [SerializeField] private int MinPlatformSize = 1;
    [SerializeField] private int MaxPlatformSize = 4;
    [SerializeField] private int LowLandscapeOffset = 1;
    [SerializeField] private int HighLandscapeOffset = 2;
    [SerializeField] private int LowPlatformOffset = 3;
    [SerializeField] private int HighPlatformOffset = 5;

    [Header("Chance Sliders")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceOfHazard = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceOfBridge = 0.1f;

    [Header("Number of Platforms")]
    [SerializeField] private int NumerOfPlatforms = 100;

    private int i = 0;
    private int w = 1;
    private int j = 0;
    private int PlatformSize;
    private int LandscapeLowPoint;
    private int LandscapeHighPoint;
    private int PlatformLowPoint;
    private int PlatformHighPoint;
    private int Spacer;
    private int TileSpace;
    private int Distance;
    private int PlatformPosition;
    private bool IsHazard;
    private GameObject Map;
    private GameObject Platforms;
    private GameObject Stone;
    private GameObject Dirt;
    private GameObject Grass;
    private GameObject Bridge;
    private GameObject Hazard;

    void OnEnable()
    {
        Map = new GameObject("Map");
        Platforms = new GameObject("Platforms");
        Generate();
    }

    private void OnDisable()
    {
        Destroy(Map);
        Destroy(Platforms);
    }

    private void Generate()
    {
        Distance = Height;

        #region Method One First Block
        LandscapeLowPoint = Distance - 1;
        LandscapeHighPoint = Distance + 2;
        Distance = Random.Range(LandscapeLowPoint, LandscapeHighPoint);
        Spacer = Random.Range(SpacerMinimum, SpacerMaximum);
        TileSpace = Distance - Spacer;

        for (i = 0; i < TileSpace; i++)
        {
            Stone = Instantiate(StonePrefab, new Vector3(0, i), Quaternion.identity) as GameObject;
            Stone.transform.SetParent(Map.transform);
        }

        for (i = TileSpace; i < Distance; i++)
        {
            Dirt = Instantiate(DirtPrefab, new Vector3(0, i), Quaternion.identity) as GameObject;
            Dirt.transform.SetParent(Map.transform);
        }

        Grass = Instantiate(GrassPrefab, new Vector3(0, Distance), Quaternion.identity) as GameObject;
        Grass.transform.SetParent(Map.transform);
        #endregion

        #region Method One Loop
        while(w < Width)
        {
            if (Random.value < ChanceOfBridge)
            {
                PlatformSize = Mathf.RoundToInt(Random.Range(MinPlatformSize, MaxPlatformSize));

                for(i = 0; i < PlatformSize; i++)
                {
                    Bridge = Instantiate(BridgePrefab, new Vector3(w, Distance), Quaternion.identity) as GameObject;
                    Bridge.transform.SetParent(Map.transform);
                    w++;
                }
            }

            else
            {
                LandscapeLowPoint = Distance - LowLandscapeOffset;
                LandscapeHighPoint = Distance + HighLandscapeOffset;
                Distance = Random.Range(LandscapeLowPoint, LandscapeHighPoint);
                Spacer = Random.Range(SpacerMinimum, SpacerMaximum);
                TileSpace = Distance - Spacer;

                PlatformSize = Mathf.RoundToInt(Random.Range(MinPlatformSize, MaxPlatformSize));

                for(j = 0; j < PlatformSize; j++)
                {
                    for (i = 0; i < TileSpace; i++)
                    {
                        Stone = Instantiate(StonePrefab, new Vector3(w, i), Quaternion.identity) as GameObject;
                        Stone.transform.SetParent(Map.transform);
                    }

                    for (i = TileSpace; i < Distance; i++)
                    {
                        Dirt = Instantiate(DirtPrefab, new Vector3(w, i), Quaternion.identity) as GameObject;
                        Dirt.transform.SetParent(Map.transform);
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
                        Hazard = Instantiate(HazardPrefab[Random.Range(0, 3)], new Vector3(w, Distance), Quaternion.identity) as GameObject;
                        Hazard.transform.SetParent(Map.transform);
                    }

                    else
                    {
                        Grass = Instantiate(GrassPrefab, new Vector3(w, Distance), Quaternion.identity) as GameObject;
                        Grass.transform.SetParent(Map.transform);
                    }

                    PlatformLowPoint = Distance + LowPlatformOffset;
                    PlatformHighPoint = Distance + HighPlatformOffset;
                    PlatformPosition = Random.Range(PlatformLowPoint, PlatformHighPoint);
                    PlacePlatform(Random.Range(0, 2), w, PlatformPosition);
                    w++;
                }
            }
        }
        #endregion

        #region Method One Last Block
        LandscapeLowPoint = Distance - 1;
        LandscapeHighPoint = Distance + 2;
        Distance = Random.Range(LandscapeLowPoint, LandscapeHighPoint);
        Spacer = Random.Range(SpacerMinimum, SpacerMaximum);
        TileSpace = Distance - Spacer;

        for (i = 0; i < TileSpace; i++)
        {
            Stone = Instantiate(StonePrefab, new Vector3(Width, i), Quaternion.identity) as GameObject;
            Stone.transform.SetParent(Map.transform);
        }

        for (i = TileSpace; i < Distance; i++)
        {
            Dirt = Instantiate(DirtPrefab, new Vector3(Width, i), Quaternion.identity) as GameObject;
            Dirt.transform.SetParent(Map.transform);
        }

        Grass = Instantiate(GrassPrefab, new Vector3(Width, Distance), Quaternion.identity) as GameObject;
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
                Hazard = Instantiate(HazardPrefab[Random.Range(0,2)], new Vector3(w, h), Quaternion.identity) as GameObject;
                Hazard.transform.SetParent(Platforms.transform);
            }

            else if (Random.value < ChanceOfBridge)
            {
                Bridge = Instantiate(BridgePrefab, new Vector3(w, h), Quaternion.identity) as GameObject;
                Bridge.transform.SetParent(Platforms.transform);
            }

            else
            {
                Grass = Instantiate(GrassPrefab, new Vector3(w, h), Quaternion.identity) as GameObject;
                Grass.transform.SetParent(Platforms.transform);
            }
        }
    }
}
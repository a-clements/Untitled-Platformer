using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Custom Inspector Value")]
    [SerializeField] private int Index;

    [Header("Prefabs")]
    [SerializeField] private GameObject DirtPrefab;
    [SerializeField] private GameObject GrassPrefab;
    [SerializeField] private GameObject StonePrefab;
    [SerializeField] private GameObject HazardPrefab;
    [SerializeField] private GameObject BridgePrefab;

    [Header("Method 1 variables")]
    [SerializeField] private int MethodOneHeight = 30;
    [SerializeField] private int MethodOneWidth = 120;
    [SerializeField] private int SpacerMinimum = 12;
    [SerializeField] private int SpacerMaximum = 24;
    [SerializeField] private int Distance;

    [Header("Method 2 variables")]
    [SerializeField] private int MethodTwoHeight = 4;
    [SerializeField] private int MethodTwoWidth = 120;
    [SerializeField] private int MinPlatformSize = 1;
    [SerializeField] private int MaxPlatformSize = 10;
    [SerializeField] private int MaxHazards = 3;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceofHazard = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceofBridge = 0.1f;

    [Header("Number of Platforms")]
    [SerializeField] private int NumerOfPlatforms = 100;

    private int i = 0;
    private int w = 0;
    private int LowPoint;
    private int HighPoint;
    private int Spacer;
    private int TileSpace;
    private bool IsHazard;
    private GameObject Map;
    private GameObject Stone;
    private GameObject Dirt;
    private GameObject Grass;
    private GameObject Bridge;
    private GameObject Hazard;

    public int MethodIndex
    {
        get
        {
            return Index;
        }

        set
        {
            Index = value;
        }
    }

    void Start()
    {
        Map = new GameObject("Map");
        Generate();
    }

    private void Generate()
    {
        switch(Index)
        {
            case 0:
                Distance = MethodOneHeight;

                #region Method One First Block
                LowPoint = Distance - 1;
                HighPoint = Distance + 2;
                Distance = Random.Range(LowPoint, HighPoint);
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
                for (int w = 1; w < MethodOneWidth; w++)
                {
                    if (Random.value < ChanceofBridge)
                    {
                        Bridge = Instantiate(BridgePrefab, new Vector3(w, Distance), Quaternion.identity) as GameObject;
                        Bridge.transform.SetParent(Map.transform);
                    }

                    else
                    {
                        LowPoint = Distance - 1;
                        HighPoint = Distance + 2;
                        Distance = Random.Range(LowPoint, HighPoint);
                        Spacer = Random.Range(SpacerMinimum, SpacerMaximum);
                        TileSpace = Distance - Spacer;

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

                        if (Random.value < ChanceofHazard)
                        {
                            Hazard = Instantiate(HazardPrefab, new Vector3(w, Distance), Quaternion.identity) as GameObject;
                            Hazard.transform.SetParent(Map.transform);
                        }

                        else
                        {
                            Grass = Instantiate(GrassPrefab, new Vector3(w, Distance), Quaternion.identity) as GameObject;
                            Grass.transform.SetParent(Map.transform);
                        }
                    }

                    if (w < NumerOfPlatforms + 1)
                    {
                        PlacePlatform(Random.Range(0, 2), w, Random.Range(Distance + 3, Distance + 5));
                    }
                }
                #endregion

                #region Method One Last Block
                LowPoint = Distance - 1;
                HighPoint = Distance + 2;
                Distance = Random.Range(LowPoint, HighPoint);
                Spacer = Random.Range(SpacerMinimum, SpacerMaximum);
                TileSpace = Distance - Spacer;

                for (i = 0; i < TileSpace; i++)
                {
                    Stone = Instantiate(StonePrefab, new Vector3(MethodOneWidth, i), Quaternion.identity) as GameObject;
                    Stone.transform.SetParent(Map.transform);
                }

                for (i = TileSpace; i < Distance; i++)
                {
                    Dirt = Instantiate(DirtPrefab, new Vector3(MethodOneWidth, i), Quaternion.identity) as GameObject;
                    Dirt.transform.SetParent(Map.transform);
                }

                Grass = Instantiate(GrassPrefab, new Vector3(MethodOneWidth, Distance), Quaternion.identity) as GameObject;
                Grass.transform.SetParent(Map.transform);
                #endregion
                break;

            case 1:
                #region Method Two First Block
                Stone = Instantiate(StonePrefab, new Vector3(0, -2), Quaternion.identity) as GameObject;
                Instantiate(DirtPrefab, new Vector3(0, -1), Quaternion.identity);
                Instantiate(GrassPrefab, new Vector3(0, 0), Quaternion.identity);
                #endregion

                #region Method Two Loop
                for (int w = 1; w < MethodTwoWidth; w++)
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
                        PlacePlatform(Random.Range(0, 2), w, Random.Range(2, MethodTwoHeight + 1));
                    }
                }
                #endregion

                #region Method Two Last Block
                Instantiate(StonePrefab, new Vector3(MethodTwoWidth, -2), Quaternion.identity);
                Instantiate(DirtPrefab, new Vector3(MethodTwoWidth, -1), Quaternion.identity);
                Instantiate(GrassPrefab, new Vector3(MethodTwoWidth, 0), Quaternion.identity);
                #endregion
                break;

            case 2:
                Debug.Log("nothing here yet");
                break;
        }
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
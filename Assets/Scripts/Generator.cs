using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public bool MethodChange;

    [Header("Prefabs")]
    [SerializeField] private GameObject DirtPrefab;
    [SerializeField] private GameObject GrassPrefab;
    [SerializeField] private GameObject StonePrefab;
    [SerializeField] private GameObject PlatformPrefab;
    [SerializeField] private GameObject HazardPrefab;
    [SerializeField] private GameObject BridgePrefab;

    [Header("Common")]
    [SerializeField] private int Distance;

    [Header("Method 1 variables")]
    [SerializeField] private int MethodOneHeight = 30;
    [SerializeField] private int MethodOneWidth = 120;
    [SerializeField] private int SpacerMinimum = 12;
    [SerializeField] private int SpacerMaximum = 24;

    private int LowPoint;
    private int HighPoint;
    private int Spacer;
    private int TileSpace;

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

    private bool IsHazard;

    [Header("Number of Platforms")]
    [SerializeField] private int NumerOfPlatforms = 100;


    private int i;


    void Start()
    {
        Generate();
    }

    void Generate()
    {
        if (MethodChange == false)
        {
            Distance = MethodOneHeight;

            for (int w = 0; w < MethodOneWidth; w++)
            {
                LowPoint = Distance - 1;
                HighPoint = Distance + 2;
                Distance = Random.Range(LowPoint, HighPoint);
                Spacer = Random.Range(SpacerMinimum, SpacerMaximum);
                TileSpace = Distance - Spacer;

                for (i = 0; i < TileSpace; i++)
                {
                    Instantiate(StonePrefab, new Vector3(w, i), Quaternion.identity);
                }

                for (i = TileSpace; i < Distance; i++)
                {
                    Instantiate(DirtPrefab, new Vector3(w, i), Quaternion.identity);
                }

                Instantiate(GrassPrefab, new Vector3(w, Distance), Quaternion.identity);

                PlacePlatform(Random.Range(0,2),  w, Random.Range(Distance + 3, Distance + 5));
            }
        }

        else
        {
            for (int w = 0; w < MethodTwoWidth; w++)
            {
                if(Random.value < ChanceofBridge)
                {
                    Instantiate(BridgePrefab, new Vector3(w, 0),Quaternion.identity);
                }

                else if(Random.value < ChanceofHazard)
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

                PlacePlatform(Random.Range(0, 2), w, Random.Range(2, MethodTwoHeight + 1));
            }
        }
    }

    void PlacePlatform(int value, int w, int h)
    {
        if(value < 1)
        {
            if (Random.value < ChanceofHazard)
            {
                GameObject Platform = Instantiate(HazardPrefab, new Vector3(w, h), Quaternion.identity) as GameObject;
            }

            else if (Random.value < ChanceofBridge)
            {
                GameObject Platform = Instantiate(BridgePrefab, new Vector3(w, h), Quaternion.identity) as GameObject;
            }

            else
            {
                GameObject Platform = Instantiate(PlatformPrefab, new Vector3(w, h), Quaternion.identity) as GameObject;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorOne : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject DirtPrefab;
    [SerializeField] private GameObject GrassPrefab;
    [SerializeField] private GameObject StonePrefab;
    [SerializeField] private GameObject HazardPrefab;
    [SerializeField] private GameObject BridgePrefab;

    [Header("Variables")]
    [SerializeField] private int Height = 30;
    [SerializeField] private int Width = 120;
    [SerializeField] private int SpacerMinimum = 12;
    [SerializeField] private int SpacerMaximum = 24;
    [SerializeField] private int MaxHazardSize = 3;
    [SerializeField] private int Distance;

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
        Distance = Height;

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
        for (int w = 1; w < Width; w++)
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

                if (IsHazard == true)
                {
                    IsHazard = false;
                }

                else if (Random.value < ChanceofHazard)
                {
                    IsHazard = true;
                }

                else
                {
                    IsHazard = false;
                }

                if (IsHazard == true)
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

            else if (Random.value < ChanceofHazard)
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
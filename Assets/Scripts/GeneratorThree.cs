using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorThree : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject GrassPrefab;
    [SerializeField] private GameObject[] HazardPrefab;
    [SerializeField] private GameObject BridgePrefab;

    [Header("Variables")]
    [SerializeField] private int MaxHazardSize = 4;
    [SerializeField] private int MinPlatformSize = 1;
    [SerializeField] private int MaxPlatformSize = 10;
    [SerializeField] private int LowPlatformOffset = 3;
    [SerializeField] private int HighPlatformOffset = -3;

    [Header("Chance Sliders")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceOfHazard = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceOfTrap = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceofBridge = 0.1f;

    [Header("Number of Platforms")]
    [SerializeField] private int NumerOfPlatforms = 120;

    private int w = 0;
    private int i = 0;
    private int PlatformSize;
    private int HazardSize;
    private int YPosition = 0;
    private int XPosition = 1;
    private bool IsHazard;
    private GameObject Map;
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

    void Generate()
    {
        #region Method Three First Block
        Grass = Instantiate(GrassPrefab, new Vector2(0,0), Quaternion.identity) as GameObject;
        Grass.transform.SetParent(Map.transform);
        #endregion

        #region Method Three Loop
        for (w = 1; w < NumerOfPlatforms; w++)
        {
            if(IsHazard == true)
            {
                IsHazard = false;
            }

            else if(Random.value < ChanceOfHazard)
            {
                IsHazard = true;
            }

            else
            {
                IsHazard = false;
            }

            if(IsHazard == true)
            {
                if (Random.value < ChanceOfTrap)
                {
                    for (i = 0; i < HazardSize; i++)
                    {
                        XPosition++;
                    }
                }

                else
                {
                    HazardSize = Mathf.RoundToInt(Random.Range(1, MaxHazardSize));

                    for (i = 0; i < HazardSize; i++)
                    {
                        if (i == 0 || i == HazardSize - 1)
                        {
                            Grass = Instantiate(GrassPrefab, new Vector2(XPosition, YPosition), Quaternion.identity) as GameObject;
                            Grass.transform.SetParent(Map.transform);
                        }

                        else
                        {
                            Hazard = Instantiate(HazardPrefab[Random.Range(0, 3)], new Vector2(XPosition, YPosition), Quaternion.identity) as GameObject;
                            Hazard.transform.SetParent(Map.transform);
                        }

                        XPosition++;
                    }
                }
            }

            else if(Random.value < ChanceofBridge)
            {
                PlatformSize = Mathf.RoundToInt(Random.Range(MinPlatformSize, MaxPlatformSize));
                YPosition += Random.Range(LowPlatformOffset, HighPlatformOffset);

                for (i = 0; i < PlatformSize; i++)
                {
                    if(i == 0 || i == PlatformSize - 1)
                    {
                        Grass = Instantiate(GrassPrefab, new Vector2(XPosition, YPosition), Quaternion.identity) as GameObject;
                        Grass.transform.SetParent(Map.transform);
                    }

                    else
                    {
                        Bridge = Instantiate(BridgePrefab, new Vector2(XPosition, YPosition), Quaternion.identity) as GameObject;
                        Bridge.transform.SetParent(Map.transform);
                    }

                    XPosition++;
                }
            }
            
            else
            {
                PlatformSize = Mathf.RoundToInt(Random.Range(MinPlatformSize, MaxPlatformSize));
                YPosition += Random.Range(LowPlatformOffset, HighPlatformOffset);

                for (i = 0; i < PlatformSize; i++)
                {
                    Grass = Instantiate(GrassPrefab, new Vector2(XPosition, YPosition), Quaternion.identity) as GameObject;
                    Grass.transform.SetParent(Map.transform);
                    XPosition++;
                }
            }

        }
        #endregion

        #region Method Three Last Block
        YPosition += Random.Range(LowPlatformOffset, HighPlatformOffset);
        Grass = Instantiate(GrassPrefab, new Vector2(XPosition, YPosition), Quaternion.identity) as GameObject;
        Grass.transform.SetParent(Map.transform);
        #endregion
    }
}

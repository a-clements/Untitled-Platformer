using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorThree : MonoBehaviour
{
    [Header("Prefabs")]
    //[SerializeField] private GameObject DirtPrefab;
    [SerializeField] private GameObject GrassPrefab;
    //[SerializeField] private GameObject StonePrefab;
    [SerializeField] private GameObject HazardPrefab;
    [SerializeField] private GameObject BridgePrefab;

    [Header("Variables")]
    [SerializeField] private int MaxHazardSize = 4;
    [SerializeField] private int MinPlatformSize = 1;
    [SerializeField] private int MaxPlatformSize = 10;
    [SerializeField] private int Height = 3;
    [SerializeField] private int Drop = -3;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceofHazard = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float ChanceofTrap = 0.5f;
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
    //private GameObject Stone;
    //private GameObject Dirt;
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

            else if(Random.value < ChanceofHazard)
            {
                IsHazard = true;
            }

            else
            {
                IsHazard = false;
            }

            if(IsHazard == true)
            {
                if (Random.value < ChanceofTrap)
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
                        }

                        else
                        {
                            Hazard = Instantiate(HazardPrefab, new Vector2(XPosition, YPosition), Quaternion.identity) as GameObject;
                            //Bridge.transform.SetParent(Map.transform);
                        }

                        XPosition++;
                    }
                }
            }

            else if(Random.value < ChanceofBridge)
            {
                PlatformSize = Mathf.RoundToInt(Random.Range(MinPlatformSize, MaxPlatformSize));
                YPosition += Random.Range(Drop, Height);

                for (i = 0; i < PlatformSize; i++)
                {
                    if(i == 0 || i == PlatformSize - 1)
                    {
                        Grass = Instantiate(GrassPrefab, new Vector2(XPosition, YPosition), Quaternion.identity) as GameObject;
                    }

                    else
                    {
                        Bridge = Instantiate(BridgePrefab, new Vector2(XPosition, YPosition), Quaternion.identity) as GameObject;
                        //Bridge.transform.SetParent(Map.transform);
                    }

                    XPosition++;
                }
            }
            
            else
            {
                PlatformSize = Mathf.RoundToInt(Random.Range(MinPlatformSize, MaxPlatformSize));
                YPosition += Random.Range(Drop, Height);

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
        YPosition += Random.Range(Drop, Height);
        Grass = Instantiate(GrassPrefab, new Vector2(XPosition, YPosition), Quaternion.identity) as GameObject;
        //Grass.transform.SetParent(Map.transform);
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private int Height;
    [SerializeField] private int Width;
    [SerializeField] private int Distance;
    [SerializeField] private int SpacerMinimum;
    [SerializeField] private int SpacerMaximum;

    [SerializeField] private GameObject Dirt;
    [SerializeField] private GameObject Grass;
    [SerializeField] private GameObject Stone;

    [SerializeField] private float HeightPoint;
    [SerializeField] private float HeightPoint2;

    private int LowPoint;
    private int HighPoint;
    private int Spacer;
    private int TileSpace;

    private int i;


    void Start()
    {
        Generate();
    }

    void Generate()
    {
        Distance = Height;

        for(int w = 0; w <Width; w++)
        {
            LowPoint = Distance - 1;
            HighPoint = Distance + 2;
            Distance = Random.Range(LowPoint, HighPoint);
            Spacer = Random.Range(SpacerMinimum, SpacerMaximum);
            TileSpace = Distance - Spacer;

            for(i = 0; i < TileSpace; i++)
            {
                Instantiate(Stone, new Vector3(w, i), Quaternion.identity);
            }

            for(i = TileSpace; i < Distance; i++)
            {
                Instantiate(Dirt, new Vector3(w, i), Quaternion.identity);
            }

            Instantiate(Grass, new Vector3(w, Distance), Quaternion.identity);
        }
    }
}

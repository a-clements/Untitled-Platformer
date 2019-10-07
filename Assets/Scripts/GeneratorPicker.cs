using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorPicker : MonoBehaviour
{
    [SerializeField] private int RandomNumber;
    [SerializeField] private GeneratorOne GeneratorOne;
    [SerializeField] private GeneratorTwo GeneratorTwo;

    void Awake()
    {
        //executed first
        GeneratorOne = GetComponent<GeneratorOne>();
        GeneratorTwo = GetComponent<GeneratorTwo>();
        RandomNumber = Random.Range(0,3);
    }

    void OnEnable()
    {
        //executed second

        switch (RandomNumber)
        {
            case 0:
                GeneratorOne.enabled = true;
                break;

            case 1:
                GeneratorTwo.enabled = true;
                break;

            case 2:
                Debug.Log("not yet defined");
                break;
        }
    }

    private void OnDisable()
    {
        GeneratorOne.enabled = false;
        GeneratorTwo.enabled = false;
    }

    private void Start()
    {
        //executed third
    }
}

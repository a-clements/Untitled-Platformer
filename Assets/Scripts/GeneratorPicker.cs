using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorPicker : MonoBehaviour
{
    [SerializeField] private int RandomNumber;
    [SerializeField] private GeneratorOne GeneratorOne;
    [SerializeField] private GeneratorTwo GeneratorTwo;
    [SerializeField] private GeneratorThree GeneratorThree;

    void Awake()
    {
        //executed first
        GeneratorOne = GetComponent<GeneratorOne>();
        GeneratorTwo = GetComponent<GeneratorTwo>();
        GeneratorThree = GetComponent<GeneratorThree>();
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
                GeneratorThree.enabled = true;
                break;
        }
    }

    private void OnDisable()
    {
        GeneratorOne.enabled = false;
        GeneratorTwo.enabled = false;
        GeneratorThree.enabled = false;
    }

    private void Start()
    {
        //executed third
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningCredits : MonoBehaviour
{
    public Image OpeningImage;
    public Sprite[] Sprites;
    public float Timer;

    // Use this for initialization
    void Start()
    {
        OpeningImage.sprite = Sprites[0];
        OpeningImage.CrossFadeAlpha(0, 0.0f, true);
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        for (int i = 0; i < Sprites.Length; i++)
        {
            yield return new WaitForSeconds(Timer);

            OpeningImage.CrossFadeAlpha(1, 2.0f, true);

            yield return new WaitForSeconds(Timer);

            OpeningImage.CrossFadeAlpha(0, 2.0f, true);

            OpeningImage.sprite = Sprites[i];
        }

        yield return new WaitForSeconds(Timer);

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        yield return null;
    }
}
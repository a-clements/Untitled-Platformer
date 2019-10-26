using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningCredits : MonoBehaviour
{
    public Image OpeningImage;
    public Sprite[] Sprites;
    public float FadeTimer;
    public float WaitTimer;

    // Use this for initialization
    void Start()
    {
        OpeningImage.CrossFadeAlpha(0, 0.0f, true);
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        for (int i = 0; i < Sprites.Length; i++)
        {
            OpeningImage.sprite = Sprites[i];

            yield return new WaitForSeconds(WaitTimer);

            OpeningImage.CrossFadeAlpha(1, FadeTimer, true);

            yield return new WaitForSeconds(WaitTimer);

            OpeningImage.CrossFadeAlpha(0, FadeTimer, true);

            yield return new WaitForSeconds(WaitTimer);
        }

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        yield return null;
    }
}
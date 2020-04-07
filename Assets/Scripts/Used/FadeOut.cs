using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This script determines the speed of the enemy fade out after death. This script will also play a death sound and disable the game object
/// once it is faded out entirely.
/// </summary>

public class FadeOut : MonoBehaviour
{
    [Tooltip("The audio clip for death goes here.")]
    [SerializeField] private AudioClip Death;
    [SerializeField] private float WaitTimer = 0.1f;

    private void Start()
    {
        
    }

    public IEnumerator FadingOut(SpriteRenderer Sprite, float FadeOutTime)
    {
        Color Colour = Sprite.color;

        yield return new WaitForSeconds(WaitTimer);

        GetComponent<AudioSource>().PlayOneShot(Death);

        while (Colour.a > 0.0f)
        {
            Colour.a -= Time.deltaTime / FadeOutTime;
            Sprite.color = Colour;

            if (Colour.a <= 0.0f)
            {
                Colour.a = 0.0f;
            }

            Sprite.color = Colour;

            yield return null;
        }

        yield return null;

        gameObject.SetActive(false);
    }
}

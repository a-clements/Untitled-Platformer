using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeath : MonoBehaviour
{   
    /// <summary>
    /// How an emeny death behaves.
    /// </summary>
    /// 
    [Tooltip("The amount of the shout metre that is filled.")]
    [SerializeField] private float RefillAmount = 0.1f;
    [Tooltip("A declaration of how many points will be added to the score when the flyer dies.")]
    [SerializeField] private int PointValue;
    [Tooltip("A declaration of the time frame over which the flyer will fade after death.")]
    [SerializeField] private float FadeOutTime = 1.0f;
    [Tooltip("The sprite to show upon death of the flyer goes here.")]
    [SerializeField] private Sprite DeathSprite;

    private Transform ThisTransform;
    private Image ShoutMetre;
    private FadeOut Fade;

    //public bool Dead = false;
    //public bool Shocked = false;
    public bool CanShowPanel = true;

    private void Start()
    {
        ShoutMetre = GameObject.Find("Post Process Canvas").transform.GetChild(0).GetComponent<Image>();
        ThisTransform = transform;
        Fade = GetComponent<FadeOut>();
    }

    public void Dead()
    {
        if (ShoutMetre.fillAmount < 1.0f)
        {
            ShoutMetre.fillAmount += RefillAmount;

            if (CanShowPanel == true)
            {
                GameObject.Find("Entry").GetComponent<Checkpoints>().PanelTwo.SetActive(true);
                Time.timeScale = 0;

                var Monsters = FindObjectsOfType<EnemyDeath>();

                foreach (EnemyDeath monster in Monsters)
                {
                    monster.CanShowPanel = false;
                }
            }
        }

        ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, 2.0f);
        ScoreManager.UpdateScores(PointValue);
        ThisTransform.GetComponent<EnemyDeath>().enabled = false;
        ThisTransform.GetComponent<Animator>().enabled = false;
        ThisTransform.GetComponent<SpriteRenderer>().sprite = DeathSprite;
        ThisTransform.localScale = new Vector3(ThisTransform.localScale.x, ThisTransform.localScale.y / 2, ThisTransform.localScale.z);

        StartCoroutine(Fade.FadingOut(GetComponent<SpriteRenderer>(), FadeOutTime));
    }

    public void Shocked()
    {
        ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, 2.0f);
        ScoreManager.UpdateScores(PointValue);
        ThisTransform.GetComponent<EnemyDeath>().enabled = false;
        ThisTransform.GetComponent<Animator>().enabled = false;
        ThisTransform.GetComponent<SpriteRenderer>().sprite = DeathSprite;
        ThisTransform.localScale = new Vector3(ThisTransform.localScale.x, ThisTransform.localScale.y / 2, ThisTransform.localScale.z);

        StartCoroutine(Fade.FadingOut(GetComponent<SpriteRenderer>(), FadeOutTime));
    }

    private void Update()
    {
        //if (Dead == true)
        //{

        //}

        //if (Shocked == true)
        //{

        //}
    }
}

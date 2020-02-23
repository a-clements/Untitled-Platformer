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
    private CapsuleCollider2D Collider;

    //public bool Dead = false;
    //public bool Shocked = false;
    public bool CanShowPanel = true;
    public bool IsDead = false;

    private void Start()
    {
        ShoutMetre = GameObject.Find("Post Process Canvas").transform.GetChild(0).GetComponent<Image>();
        ThisTransform = transform;
        Fade = GetComponent<FadeOut>();
        Collider = GetComponent<CapsuleCollider2D>();
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

        Shocked();
    }

    public void Shocked()
    {
        Collider.enabled = false;
        ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, 0.0f);
        ScoreManager.UpdateScores(PointValue);

        ThisTransform.GetComponent<Rigidbody2D>().gravityScale = Mathf.Abs(ThisTransform.GetComponent<Rigidbody2D>().gravityScale - 1);

        switch(ThisTransform.name)
        {
            case "Imp One":
            case "Imp Two":
                GetComponent<FlyingEnemyMove>().enabled = false;
                break;

            case "Skeleton":
                GetComponent<SkeletonMove>().StopEverything();
                GetComponent<SkeletonMove>().enabled = false;
                break;

            case "Short Range Knight":
                GetComponent<ShortKnightMove>().StopEverything();
                GetComponent<ShortKnightMove>().enabled = false;
                break;

            case "Long Range Knight":
                GetComponent<LongKnightMove>().StopEverything();
                GetComponent<LongKnightMove>().enabled = false;
                break;
        }

        ThisTransform.GetComponent<EnemyDeath>().enabled = false;
        ThisTransform.GetComponent<Animator>().enabled = false;
        ThisTransform.GetComponent<SpriteRenderer>().sprite = DeathSprite;
        ThisTransform.localScale = new Vector3(ThisTransform.localScale.x, ThisTransform.localScale.y / 2, ThisTransform.localScale.z);

        //StartCoroutine(Fade.FadingOut(GetComponent<SpriteRenderer>(), FadeOutTime));
    }

    private void FixedUpdate()
    {
        if(IsDead == true)
        {
            Dead();
        }
    }
}

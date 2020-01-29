using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlyingEnemyMove : MonoBehaviour
{
    /// <summary>
    /// This script details how a flyer will move between point A and point B. If the flyer does not move then leave all variables blank.
    /// </summary>
    /// 

    private Transform ThisTransform;
    private Rigidbody2D RigidBody;
    private SpriteRenderer Sprite;
    private FadeOut Fade;
    private AudioSource SFX;
    private Vector3 NextPosition;
    private int PointNumber = 0;

    [SerializeField]private Image ShoutMetre;

    [Tooltip("A declaration of how many points will be added to the score when the flyer dies.")]
    [SerializeField] private int PointValue;
    [Tooltip("A declaration of the time frame over which the flyer will fade after death.")]
    [SerializeField] private float FadeOutTime = 1.0f;
    [Tooltip("The sprite to show upon death of the flyer goes here.")]
    [SerializeField] private Sprite DeathSprite;
    [Tooltip("A boolean to show if the enemy is moving or not.")]
    [SerializeField] private bool IsMoving;
    [Tooltip("A declaration of how fast the flyer is moving. Only positive numbers.")]
    [SerializeField] private float Speed;
    [Tooltip("An array of points at which the enemy will stop.")]
    [SerializeField] private Vector3[] Points;
    [SerializeField] private float RefillAmount = 0.1f;
    public bool Dead = false;
    public bool Shocked = false;
    public bool CanShowPanel = true;

    void Start()
    {
        ShoutMetre = GameObject.Find("Post Process Canvas").transform.GetChild(0).GetComponent<Image>();
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
        Fade = GetComponent<FadeOut>();

        if (IsMoving == true)
        {
            NextPosition = Points[PointNumber];
        }
    }

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if(CollisionInfo.transform.tag == "Ground")
        {
            //Reverse = !Reverse;
        }

        if(CollisionInfo.transform.tag == "Player")
        {
            CollisionInfo.transform.GetComponent<PlayerHealth>().LoseLife();
        }
    }

    void Update()
    {
        if(IsMoving == true)
        {
            if(ThisTransform.position == Points[PointNumber])
            {
                PointNumber++;

                if(PointNumber == Points.Length)
                {
                    PointNumber = 0;
                }

                NextPosition = Points[PointNumber];

                Sprite.flipX = !Sprite.flipX;
            }

            ThisTransform.position = Vector3.MoveTowards(ThisTransform.position, NextPosition, Speed * Time.deltaTime);
        }  

        if(Dead == true)
        {
            if(ShoutMetre.fillAmount < 1.0f)
            {
                ShoutMetre.fillAmount += RefillAmount;

                if(CanShowPanel == true)
                {
                    GameObject.Find("Entry").GetComponent<Checkpoints>().PanelTwo.SetActive(true);
                    Time.timeScale = 0;

                    var Monsters = FindObjectsOfType<FlyingEnemyMove>();
                    
                    foreach(FlyingEnemyMove monster in Monsters)
                    {
                        monster.CanShowPanel = false;
                    }
                }
            }

            ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, 2.0f);
            ScoreManager.UpdateScores(PointValue);
            ThisTransform.GetComponent<FlyingEnemyMove>().enabled = false;
            ThisTransform.GetComponent<Animator>().enabled = false;
            ThisTransform.GetComponent<SpriteRenderer>().sprite = DeathSprite;
            ThisTransform.localScale = new Vector3(ThisTransform.localScale.x, ThisTransform.localScale.y / 2, ThisTransform.localScale.z);

            StartCoroutine(Fade.FadingOut(GetComponent<SpriteRenderer>(), FadeOutTime));
        }

        if(Shocked == true)
        {
            ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, 2.0f);
            ScoreManager.UpdateScores(PointValue);
            ThisTransform.GetComponent<FlyingEnemyMove>().enabled = false;
            ThisTransform.GetComponent<Animator>().enabled = false;
            ThisTransform.GetComponent<SpriteRenderer>().sprite = DeathSprite;
            ThisTransform.localScale = new Vector3(ThisTransform.localScale.x, ThisTransform.localScale.y / 2, ThisTransform.localScale.z);

            StartCoroutine(Fade.FadingOut(GetComponent<SpriteRenderer>(), FadeOutTime));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyingEnemyMove : MonoBehaviour
{
    /// <summary>
    /// This script details how a flyer will move between point A and point B. If the flyer does not move then leave all variables blank.
    /// </summary>
    /// 

    private Transform ThisTransform;
    private Rigidbody RigidBody;
    private SpriteRenderer Sprite;
    private FadeOut Fade;
    private AudioSource SFX;

    [Tooltip("A declaration on if the direction of the flyer has been reversed. If it is false then the Y rotation must be set to 0. If it is true then the Y rotation must be set to 180.")]
    [SerializeField] private bool Reverse;
    [Tooltip("The sprite to show upon death of the flyer goes here.")]
    [SerializeField] private Sprite DeathSprite;
    [Tooltip("A declaration of the time frame over which the flyer will fade after death.")]
    [SerializeField] private float FadeOutTime = 1.0f;
    [Tooltip("A declaration of how many points will be added to the score when the flyer dies.")]
    [SerializeField] private int PointValue;
    [Tooltip("A decleration of which axis the flyer will move on.")]
    [SerializeField] private string Direction;
    [Tooltip("A declaration of how fast the flyer is moving. Only positive numbers.")]
    [SerializeField] private float Speed;
    [Tooltip("A declaration of whether the flyer is moving between point to point on the X axis, rather than by collision.")]
    [SerializeField] private bool XAxis;
    [Tooltip("A declaration of the maximum position of the flyer while from point to point on the X axis")]
    [SerializeField] private float MaxXDistance = 0.0f;
    [Tooltip("A declaration of the minimum position of the flyer while from point to point on the X axis")]
    [SerializeField] private float MinXDistance = 0.0f;
    [Tooltip("A declaration of whether the flyer is moving between point to point on the Y axis, rather than by collision.")]
    [SerializeField] private bool YAxis;
    [Tooltip("A declaration of the maximum position of the flyer while from point to point on the Y axis")]
    [SerializeField] private float MaxYDistance = 0.0f;
    [Tooltip("A declaration of the minimum position of the flyer while from point to point on the Y axis")]
    [SerializeField] private float MinYDistance = 0.0f;
    [Tooltip("Is the flyer dead. This variable does not get used by the designer but needs to be public for other scripts to access it.")]
    public bool Dead;

    void Start()
    {
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody>();
        Sprite = GetComponent<SpriteRenderer>();
        Fade = GetComponent<FadeOut>();
        SFX = GetComponent<AudioSource>();

        SFX.loop = true;

        SFX.Play();
    }

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Rock")
        {

            Dead = true;
        }

        if (CollisionInfo.transform.tag == "Ground" || CollisionInfo.transform.tag == "Enemy")
        {
            Reverse = !Reverse;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(Time.timeScale == 0)
        {
            SFX.Pause();
        }
        else
        {
            SFX.UnPause();
        }

        if (XAxis == true)
        {
            if (ThisTransform.localPosition.x >= MaxXDistance || ThisTransform.localPosition.x <= MinXDistance)
            {
                Reverse = !Reverse;

            }

            if(Reverse == true)
            {
                ThisTransform.eulerAngles = new Vector3(0, 0, 0);
            }

            else
            {
                ThisTransform.eulerAngles = new Vector3(0, 180, 0);
            }

        }

        if (YAxis == true)
        {
            if (ThisTransform.localPosition.y >= MaxYDistance || ThisTransform.localPosition.y <= MinYDistance)
            {
                Speed *= -1.0f;
            }
        }

        switch (Direction)
        {
            case "Up":
                ThisTransform.Translate(Vector2.down * Speed * Time.deltaTime);
                break;
            case "Left":
                ThisTransform.Translate(Vector2.left * Speed * Time.deltaTime);
                break;
        }

        if(Dead == true)
        {
            ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, 2.0f);
            ScoreManager.UpdateScores(PointValue);
            ThisTransform.GetComponent<FlyingEnemyMove>().enabled = false;
            ThisTransform.GetComponent<Animator>().enabled = false;
            ThisTransform.GetComponent<SpriteRenderer>().sprite = DeathSprite;
            ThisTransform.localScale = new Vector3(ThisTransform.localScale.x, ThisTransform.localScale.y / 2, ThisTransform.localScale.z);

            StartCoroutine(Fade.FadingOut(GetComponent<SpriteRenderer>(),FadeOutTime));
        }
	}
}

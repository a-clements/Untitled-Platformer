using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody RigidBody;
    private SpriteRenderer Sprite;
    [SerializeField] private Sprite DeathSprite;
    private FadeOut Fade;
    [SerializeField] private float FadeOutTime = 1.0f;
    [SerializeField] private int PointValue;
    [SerializeField] private string Direction;
    [SerializeField] private float Speed;
    [SerializeField] private bool XAxis;
    [SerializeField] private float MaxXDistance = 0.0f;
    [SerializeField] private float MinXDistance = 0.0f;
    [SerializeField] private bool YAxis;
    [SerializeField] private float MaxYDistance = 0.0f;
    [SerializeField] private float MinYDistance = 0.0f;
    public bool Dead;
    private AudioSource SFX;

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
            Speed *= -1.0f;

            if (Speed > 0)
            {
                Sprite.flipX = false;
            }
            else
            {
                Sprite.flipX = true;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        ThisTransform.Translate(new Vector3(Speed, 0, 0) * Time.deltaTime);


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
            if (ThisTransform.localPosition.x > MaxXDistance || ThisTransform.localPosition.x < MinXDistance)
            {
                Speed *= -1.0f;
            }

        }

        if (YAxis == true)
        {
            if (ThisTransform.localPosition.y > MaxYDistance || ThisTransform.localPosition.y < MinYDistance)
            {
                Speed *= -1.0f;
            }
        }

        switch (Direction)
        {
            case "Up":
                ThisTransform.Translate(new Vector3(0, Speed, 0) * Time.deltaTime);
                break;
            case "Left":
                ThisTransform.Translate(new Vector3(Speed, 0, 0) * Time.deltaTime);
                break;
        }

        if(Dead == true)
        {
            ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, 2.0f);
            ScoreManager.UpdateScores(PointValue);
            ThisTransform.GetComponent<Enemy>().enabled = false;
            ThisTransform.GetComponent<Animator>().enabled = false;
            ThisTransform.GetComponent<SpriteRenderer>().sprite = DeathSprite;
            ThisTransform.localScale = new Vector3(ThisTransform.localScale.x, ThisTransform.localScale.y / 2, ThisTransform.localScale.z);

            if (Speed > 0)
            {
                Sprite.flipX = false;
            }
            else
            {
                Sprite.flipX = true;
            }

            StartCoroutine(Fade.FadingOut(GetComponent<SpriteRenderer>(),FadeOutTime));
        }
	}
}

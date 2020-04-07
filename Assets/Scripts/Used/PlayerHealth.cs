using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script allows the designer to determine how many hearts there are in a life of the character. It will also take hearts off the player
/// based on how far they fall. It is entirely possible for the player to die from falling too far.
/// </summary>

public class PlayerHealth : MonoBehaviour
{
    public AnimatorClipInfo[] ClipInfo;

    [SerializeField] private Image[] Hearts;
    [SerializeField] private Text Lives;
    [SerializeField] private AudioClip DeathMusic;
    [SerializeField] private GameObject OneUp;
    public static int HeartsRemaining;


    void Start()
    {
        HeartsRemaining = Hearts.Length - 1;
        Lives.text = "X " + LivesManager.LivesRemaining;
    }

    public void GainHeart()
    {
        if (HeartsRemaining != (Hearts.Length - 1))
        {
            HeartsRemaining++;
            Hearts[HeartsRemaining].gameObject.SetActive(true);
        }

        else
        {
            LivesManager.LivesRemaining++;
            Lives.text = "X " + LivesManager.LivesRemaining;
            Hearts[3].gameObject.SetActive(false);
            Hearts[2].gameObject.SetActive(false);
            Hearts[1].gameObject.SetActive(false);
            HeartsRemaining = 0;
            StartCoroutine(ExtraLife());
        }
    }

    private IEnumerator ExtraLife()
    {
        OneUp.SetActive(true);
        ClipInfo = this.transform.GetChild(4).GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        yield return new WaitForSeconds(ClipInfo[0].clip.length);
        OneUp.SetActive(false);
        yield return null;
    }

    public void LoseHeart()
    {
        if (HeartsRemaining > -1)
        {
            Hearts[HeartsRemaining].gameObject.SetActive(false);
            HeartsRemaining--;
        }

        if (HeartsRemaining == -1)
        {
            ScoreManager.SaveScores();

            if(LivesManager.LivesRemaining > -1)
            {
                LivesManager.LivesRemaining--;

                if(LivesManager.LivesRemaining > -1)
                {
                    Lives.text = "X " + LivesManager.LivesRemaining;
                }
            }

            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation()
    {
        if (LivesManager.LivesRemaining > -1)
        {
            GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsIdle", false);

            GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsSleeping", false);

            GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsDead", true);

            GetComponent<PlayerMove>().enabled = false;

            yield return new WaitForSeconds(GetComponent<PlayerMove>().PlayerAnimator.GetCurrentAnimatorStateInfo(0).length);

            this.transform.position = this.GetComponent<PlayerMove>().Checkpoint.position;

            GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsDead", false);

            GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsIdle", true);

            //GetComponent<CapsuleCollider2D>().enabled = true;

            this.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.transform.rotation = Quaternion.identity;

            for (int i = 0; i < Hearts.Length; i++)
            {
                GainHeart();
            }

            GetComponent<PlayerMove>().CanJump = true;
            GetComponent<PlayerMove>().JumpCount = 1;
            GetComponent<PlayerMove>().enabled = true;
        }

        else
        {
            StopCoroutine(GetComponent<PlayerMove>().GoBackToSleep());
            StopCoroutine(GetComponent<PlayerMove>().Snooze());

            GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsIdle", false);

            GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsSleeping", false);

            GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsDead", true);

            yield return new WaitForSeconds(GetComponent<PlayerMove>().PlayerAnimator.GetCurrentAnimatorStateInfo(0).length);

            this.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.transform.rotation = Quaternion.identity;

            GetComponent<AudioSource>().PlayOneShot(DeathMusic);

            yield return new WaitForSeconds(DeathMusic.length);

            SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
        }

        yield return null;
    }
}
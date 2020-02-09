using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image[] Hearts;
    [SerializeField] private Text Lives;
    [SerializeField] public static int HeartsRemaining;

    void Start()
    {
        HeartsRemaining = Hearts.Length - 1;
        Lives.text = "X " + LivesManager.LivesRemaining;
    }

    public void GainHeart()
    {
        if(HeartsRemaining != (Hearts.Length - 1))
        {
            HeartsRemaining++;
            Hearts[HeartsRemaining].gameObject.SetActive(true);
        }

        else
        {
            LivesManager.LivesRemaining++;
            Lives.text = "X " + LivesManager.LivesRemaining;
        }
    }

    public void LoseHeart()
    {
        if(HeartsRemaining > -1)
        {
            Hearts[HeartsRemaining].gameObject.SetActive(false);
            HeartsRemaining--;
        }

        if(HeartsRemaining == -1)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            ScoreManager.SaveScores();
            StartCoroutine(DeathAnimation());
            LivesManager.LivesRemaining--;
            Lives.text = "X " + LivesManager.LivesRemaining;
        }
    }

    IEnumerator DeathAnimation()
    {
        if(LivesManager.LivesRemaining > 0)
        {
            GetComponent<PlayerMove>().enabled = false;

            GetComponent<PlayerMove>().PlayerAnimator.SetTrigger("IsDead");

            yield return new WaitForSeconds(GetComponent<PlayerMove>().PlayerAnimator.GetCurrentAnimatorStateInfo(0).length);

            this.transform.position = this.GetComponent<PlayerMove>().Checkpoint.position;

            GetComponent<CapsuleCollider2D>().enabled = true;

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
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }

        yield return null;
    }
}
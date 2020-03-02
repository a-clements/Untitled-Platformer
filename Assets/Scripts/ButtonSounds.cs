using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip MouseEnter;
    [SerializeField] private AudioClip MouseClick;
    [SerializeField] private Vector2 EffectDistance;

    private Outline ButtonOutline;
    private AudioSource ButtonAudio;


    void Start()
    {
        ButtonAudio = GetComponent<AudioSource>();
        ButtonOutline = GetComponent<Outline>();

        ButtonOutline.effectColor = Color.yellow;
        ButtonOutline.effectDistance = EffectDistance;
        ButtonOutline.enabled = false;

        if (SceneManager.GetActiveScene().name == "MapSelection")
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonOutline.enabled = true;
        //ButtonAudio.PlayOneShot(MouseEnter);

        if(SceneManager.GetActiveScene().name == "MapSelection")
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonOutline.enabled = false;
        //ButtonAudio.PlayOneShot(MouseClick);

        if (SceneManager.GetActiveScene().name == "MapSelection")
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}

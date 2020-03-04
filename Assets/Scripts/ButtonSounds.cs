using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonOutline.enabled = true;
        ButtonAudio.Stop();
        ButtonAudio.PlayOneShot(MouseEnter);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonOutline.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ButtonAudio.Stop();
        ButtonAudio.PlayOneShot(MouseClick);
    }
}

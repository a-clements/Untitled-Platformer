using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip MouseEnter;
    [SerializeField] private AudioClip MouseClick;
    [SerializeField] private Vector2 EffectDistance;

    private Outline ButtonOutline;
    private AudioSource ButtonAudio;

    //SerializedObject Halo;


    void Start()
    {
        ButtonAudio = GetComponent<AudioSource>();
        ButtonOutline = GetComponent<Outline>();

        ButtonOutline.effectColor = Color.yellow;
        ButtonOutline.effectDistance = EffectDistance;
        ButtonOutline.enabled = false;

        if (SceneManager.GetActiveScene().name == "MapSelection")
        {
            //Halo = new SerializedObject(GetComponent("Halo"));
            //Halo.FindProperty("m_Enabled").boolValue = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
            //Halo.ApplyModifiedProperties();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonOutline.enabled = true;
        //ButtonAudio.PlayOneShot(MouseEnter);

        if (SceneManager.GetActiveScene().name == "MapSelection")
        {
            //Halo.FindProperty("m_Enabled").boolValue = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
            //Halo.ApplyModifiedProperties();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonOutline.enabled = false;
        //ButtonAudio.PlayOneShot(MouseClick);

        if (SceneManager.GetActiveScene().name == "MapSelection")
        {
            //Halo.FindProperty("m_Enabled").boolValue = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
            //Halo.ApplyModifiedProperties();
        }
    }
}

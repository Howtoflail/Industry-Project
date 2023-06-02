using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSprite : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    [SerializeField]
    AudioClip downSound, upSound;
    // [SerializeField]
    // UiScriptable UiScriptableValues;

    public bool useDefaultValues;
    
    [SerializeField]
    private Sprite upImage;

    [SerializeField]
    private Sprite downImage;

    [SerializeField]
    private float buttonShrink;

    [SerializeField]
    private float buttonMove;

    [SerializeField]
    GameObject buttonObject;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() { }

    // public void PopButton()
    // {
    //     transform.localScale -= new Vector3(buttonShrink, buttonShrink, 0);
    //     transform.position += new Vector3(buttonMove, buttonMove, 0);
    // }

    public void ButtonDown()
    {
        audioSource.clip = downSound;
        audioSource.Play();
        
        gameObject.GetComponent<Image>().sprite = downImage;
    }

    public void ButtonUp()
    {
        audioSource.clip = upSound;
        audioSource.Play();

        gameObject.GetComponent<Image>().sprite = upImage;
    }
}

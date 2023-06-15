using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerBoxOpening : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float tapsToOpen;
    private float tapCount;
    private Animator animator;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip tapAudio;
    [SerializeField]
    private AudioClip openAudio;
    
    void Start()
    {
        tapCount = 0;
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tap()
    {
        audioSource.clip = tapAudio;
        audioSource.Play();
        animator.SetTrigger("Tap");
        tapCount++;
        if (tapCount >= tapsToOpen)
        {
            Open();
        }
    }

    private void Open()
    {
        audioSource.clip = openAudio;
        audioSource.Play();
        animator.SetTrigger("Open");
    }

}

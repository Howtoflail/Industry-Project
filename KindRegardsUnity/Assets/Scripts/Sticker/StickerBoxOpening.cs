using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private bool isOpen;

    void Start()
    {
        isOpen = false;
        tapCount = 0;
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() { }

    public void Tap()
    {
        if (!isOpen)
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
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Open()
    {
        isOpen = true;
        audioSource.clip = openAudio;
        audioSource.Play();
        animator.SetTrigger("Open");
    }
}

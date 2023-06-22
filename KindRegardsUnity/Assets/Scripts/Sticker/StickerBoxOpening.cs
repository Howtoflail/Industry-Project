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
    private bool stickerShown;

    void Start()
    {
        stickerShown = false;
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
            if (stickerShown)
            {
                SceneManager.UnloadSceneAsync(2);
            }
        }
    }

    void OnDisable()
    {
        Debug.Log("Scene disabled with OnDisable");
    }

    public void StickerShown()
    {
        stickerShown = true;
        // Debug.Log("Sticker shown");
    }

    private void Open()
    {
        isOpen = true;
        audioSource.clip = openAudio;
        audioSource.Play();
        animator.SetTrigger("Open");
    }
}

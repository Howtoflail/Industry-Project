using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerAnimation : MonoBehaviour
{
    // Script for the animation
    Animator stickerAnimator;

    void Start()
    {
        stickerAnimator = GetComponent<Animator>();
    }

    public void AnimationReceivingSticker()
    {
        // Play the receiving sticker animation
        stickerAnimator.SetTrigger("Receive sticker");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class AddSticker : MonoBehaviour
{
    Animator animator;
    StickerAnimation[] stickers;
    StickerCounter[] stickerInStickerbook;

    void Start()
    {
        // Get sticker animation script
        GetComponentsInChildren<StickerAnimation>();
        stickers = GetComponentsInChildren<StickerAnimation>();

        // Get sticker counter script
        GetComponentsInChildren<StickerCounter>();
        stickerInStickerbook = GetComponentsInChildren<StickerCounter>();

    }

    void Update()
    {
        // Instead of keypress, connect the database so it uses values of the database
        if (Input.GetKeyDown(KeyCode.O))
        {
            // Gets a random sticker
            Random randomNumber = new Random();
            int stickerNumber = randomNumber.Next(0, 4);
            Debug.Log(stickerNumber + 1);
            StickerAnimationLoop(stickerNumber);
            StickerCounterLoop(stickerNumber);
            AntiStickerSpam();
        }
    }

    private void StickerAnimationLoop(int stickerNumber)
    {
        // Loop through the stickers
        foreach (StickerAnimation stickerCheck in stickers) { }

        // Play the animation
        stickers[stickerNumber].AnimationReceivingSticker();
    }

    private void StickerCounterLoop(int stickerNumber)
    {
        // Loop through sticker counter
        foreach (StickerCounter stickerCounterCheck in stickerInStickerbook)
        {
        //Debug.Log(stickerCounterCheck.ToString());
        }

        // Adds the sticker counter
        stickerInStickerbook[stickerNumber].AddingASticker();
    }

    // Makes it so the sticker animation doesn't play consecutively on accident
    private IEnumerator AntiStickerSpam()
    {
        yield return new WaitForSeconds(2);
    }
}

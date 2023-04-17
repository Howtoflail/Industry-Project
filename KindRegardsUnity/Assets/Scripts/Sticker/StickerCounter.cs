using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StickerCounter : MonoBehaviour
{
    int stickerAmount = 0;
    //private TextMeshProUGUI stickerCounterText;
    [SerializeField] TextMeshProUGUI stickerCounterText;

    private void Awake()
    {
        stickerCounterText = GetComponentInChildren<TextMeshProUGUI>();
        stickerCounterText.enabled = false;
    }

    public void AddingASticker()
    {
        if (stickerAmount > 0)
        {
            stickerAmount++;
            CounterNextToSticker(stickerAmount);
            //Debug.Log("Stickercount = " + stickerAmount);
        }

        if (stickerAmount == 0)
        {
            gameObject.SetActive(true);
            stickerAmount =+1;
        }
    }

    private void CounterNextToSticker(int stickerAmount)
    {
        // Text will be the number that is added
        stickerCounterText.text = stickerAmount.ToString();
        stickerCounterText.enabled = true;
    }
}

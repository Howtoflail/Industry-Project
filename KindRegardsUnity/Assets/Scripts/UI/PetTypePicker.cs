using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PetTypePicker : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    [SerializeField]
    private List<AudioClip> animalAudioClips;
    [SerializeField]
    private PetController petController;
    [SerializeField]
    private Text typeText;
    private AudioSource audioSource;

    PetInfo petInfo;
    void Start()
    {
        petInfo = GameObject.FindWithTag("PetInfo").GetComponent<PetInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTypeText()
    {
        switch (Enum.Parse(typeof(PetStateEnum), petInfo.petType))
        {
            case PetStateEnum.Rabbit:
            typeText.text = "Rabbit";
            break;
            case PetStateEnum.Owl:
            typeText.text = "Owl";
            break;
            case PetStateEnum.Cat:
            typeText.text = "Cat";
            break;
            case PetStateEnum.Dog:
            typeText.text = "Dog";
            break;
            default:
            typeText.text = "-";
            break;
        }
    }
    // public void PlayTypeSound()
    // {
    //     switch (Enum.Parse(typeof(PetStateEnum), petInfo.petType))
    //     {
    //         case PetStateEnum.Rabbit:
    //         break;
    //         case PetStateEnum.Owl:
    //         break;
    //         case PetStateEnum.Cat:
    //         break;
    //         case PetStateEnum.Dog:
    //         break;
    //         default:
    //         typeText.text = "-";
    //         break;
    //     }
    // }
}

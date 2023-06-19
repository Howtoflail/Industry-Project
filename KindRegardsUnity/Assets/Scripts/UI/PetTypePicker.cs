using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private int currentSelectedType;

    PetInfo petInfo;

    void Start()
    {
        currentSelectedType = 0;
        petInfo = GameObject.FindWithTag("PetInfo").GetComponent<PetInfo>();
    }

    // Update is called once per frame
    void Update() { }

    public void UpdateTypeText()
    {
        switch (currentSelectedType)
        {
            case 0:
                typeText.text = PetStateEnum.Rabbit.ToString();
                break;
            case 1:
                typeText.text = PetStateEnum.Owl.ToString();
                break;
            case 2:
                typeText.text = PetStateEnum.Cat.ToString();
                break;
            case 3:
                typeText.text = PetStateEnum.Dog.ToString();
                break;
            default:
                break;
        }
    }

    public void Next()
    {
        currentSelectedType++;
        if (currentSelectedType > 3)
        {
            currentSelectedType = 0;
        }
        return;
    }

    public void Previous()
    {
        currentSelectedType--;
        if (currentSelectedType < 0)
        {
            currentSelectedType = 3;
        }
        return;
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

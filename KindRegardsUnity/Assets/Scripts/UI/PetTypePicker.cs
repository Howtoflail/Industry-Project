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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTypeText()
    {
        switch (petController.GetPetDTO().petKind)
        {
            case 0:
            typeText.text = "Rabbit";
            break;
            case 1:
            typeText.text = "Owl";
            break;
            case 2:
            typeText.text = "Cat";
            break;
            case 3:
            typeText.text = "Dog";
            break;
            default:
            typeText.text = "-";
            break;
        }
    }
    public void PlayTypeSound()
    {
        switch (petController.GetPetDTO().petKind)
        {
            case 0:
            typeText.text = "Rabbit";
            break;
            case 1:
            typeText.text = "Owl";
            break;
            case 2:
            typeText.text = "Cat";
            break;
            case 3:
            typeText.text = "Dog";
            break;
            default:
            typeText.text = "-";
            break;
        }
    }
}

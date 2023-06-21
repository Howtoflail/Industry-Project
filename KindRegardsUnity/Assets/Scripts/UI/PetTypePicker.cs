using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetTypePicker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float rabbitVolume, owlVolume, catVolume, dogVolume, pitchMin, pitchMax;

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
        audioSource = GetComponent<AudioSource>();
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
        PlayTypeSound();
        UpdateTypeText();
    }

    public void Previous()
    {
        currentSelectedType--;
        if (currentSelectedType < 0)
        {
            currentSelectedType = 3;
        }
        PlayTypeSound();
        UpdateTypeText();
    }

    public void PlayTypeSound()
    {
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        switch (currentSelectedType)
        {
            case (int)PetStateEnum.Rabbit:
                audioSource.clip = animalAudioClips[0];
                audioSource.volume = rabbitVolume;
                audioSource.Play();
                break;
            case (int)PetStateEnum.Owl:
                audioSource.clip = animalAudioClips[1];
                audioSource.volume = owlVolume;
                audioSource.Play();
                break;
            case (int)PetStateEnum.Cat:
                audioSource.clip = animalAudioClips[2];
                audioSource.volume = catVolume;
                audioSource.Play();
                break;
            case (int)PetStateEnum.Dog:
                audioSource.clip = animalAudioClips[3];
                audioSource.volume = dogVolume;
                audioSource.Play();
                break;
            default:
                typeText.text = "-";
                break;
        }
    }
}

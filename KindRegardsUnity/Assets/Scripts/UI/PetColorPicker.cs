using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetColorPicker : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    [SerializeField]
    AudioClip changeColorClip;
    [SerializeField]
    private PetController petController;
    private Color currentPetColor;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentPetColor = Color.white;
    }

    // Update is called once per frame
    public void UpdateColor(Color color)
    {
        currentPetColor = color;
        petController.SetColorCurrentPet(color);
        audioSource.clip = changeColorClip;
        audioSource.Play();
    }

    public Color GetPetColor()
    {
        return currentPetColor;
    }

}

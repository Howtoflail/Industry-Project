using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetColorPicker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private PetController petController;
    private Color currentPetColor;
    void Start()
    {
        currentPetColor = Color.white;
    }

    // Update is called once per frame
    public void UpdateColor(Color color)
    {
        currentPetColor = color;
        petController.SetColorCurrentPet(color);
    }

    public Color GetPetColor()
    {
        return currentPetColor;
    }

}

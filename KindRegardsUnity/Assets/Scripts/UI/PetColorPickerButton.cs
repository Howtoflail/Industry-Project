using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PetColorPickerButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private PetColorPicker colorPicker;
    private Image buttonColor;
    void Start()
    {
        buttonColor = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    public void UpdatePetColor()
    {
        colorPicker.UpdateColor(buttonColor.color);
    }
}

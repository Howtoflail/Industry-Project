using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDropdown : MonoBehaviour
{
    [SerializeField]
    private GameObject outputObject;

    private TextMeshProUGUI output;
    private TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        output = outputObject.GetComponent<TextMeshProUGUI>();
        dropdown = gameObject.GetComponent<TMP_Dropdown>();
    
        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        output.text = dropdown.options[index].text;
    }
}

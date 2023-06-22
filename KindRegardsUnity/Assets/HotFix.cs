using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotFix : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject characterCuz;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowCharacterCustomization()
    {
        characterCuz.SetActive(true);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;
    [SerializeField]
    private GameObject button;
    void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject btn = Instantiate(button);
            btn.name = "button:" + i;
            btn.transform.SetParent(puzzleField, false);
        }
    }
}
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryButton : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(DisableButtonOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisableButtonOnClick()
    {
        button.gameObject.SetActive(false);
    }
}

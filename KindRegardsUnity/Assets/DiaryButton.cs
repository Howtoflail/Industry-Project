using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryButton : MonoBehaviour
{
    [SerializeField]
    private Animator diaryNotifcation;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(DisableButtonOnClick);
        StartCoroutine(PlayNudge());
        Invoke("PlayNudge", 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisableButtonOnClick()
    {
        button.gameObject.SetActive(false);
    }
    private void PlayNudge()
    {

        diaryNotifcation.Play("LittleNudge");
    }
}

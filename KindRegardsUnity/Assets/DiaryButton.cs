using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryButton : MonoBehaviour
{
    [SerializeField]
    public Animator diaryNotifcation;
    public Button button;
    [SerializeField]
    public GameObject diary;
    private bool animationPlayed;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(DisableButtonOnClick);
        diaryNotifcation = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(button.gameObject.activeInHierarchy == true)
        {
            Invoke(nameof(PlayNudge), 3);
        }

        if (diary.gameObject.activeInHierarchy == true)
        {
            DisableButtonOnClick();
        }
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

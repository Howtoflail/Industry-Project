using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderClass : MonoBehaviour
{
    public Slider slider;
    public TMP_Text emotionText;
    public Emotions emotions;
    public GameObject handleSlider;

    private void Update()
    {
        ChangeImage();
    }

    private void ChangeImage()
    {
        //if (slider.value <= 10)
        //{
        //    handleSlider.GetComponent<Image>().sprite = emotions.cryingFace;
        //    emotionText.text = "Very sad";
        //}
        //else if( slider.value > 10 && slider.value <= 20)
        //{
        //    handleSlider.GetComponent<Image>().sprite = emotions.sadFace;
        //    emotionText.text = "Sad";
        //}
        //else if (slider.value > 20 && slider.value <= 30)
        //{
        //    handleSlider.GetComponent<Image>().sprite = emotions.angryFace;
        //    emotionText.text = "Angry";
        //}
        //else if (slider.value > 30 && slider.value <= 40)
        //{
        //    handleSlider.GetComponent<Image>().sprite = emotions.surprisedFace;
        //    emotionText.text = "Surprised";
        //}
        //else if (slider.value > 40 && slider.value <= 50)
        //{
        //    handleSlider.GetComponent<Image>().sprite = emotions.sickFace;
        //    emotionText.text = "Sick";
            
        //}
        //else if (slider.value > 50 && slider.value <= 60)
        //{
        //    handleSlider.GetComponent<Image>().sprite = emotions.tiredFace;
        //    emotionText.text = "Tired";
        //}
        //else if (slider.value > 60 && slider.value <= 70)
        //{
        //    handleSlider.GetComponent<Image>().sprite = emotions.hungryFace;
        //    emotionText.text = "Hungry";
        //}
        //else if (slider.value > 70 && slider.value <= 80)
        //{
        //    handleSlider.GetComponent<Image>().sprite = emotions.coolFace;
        //    emotionText.text = "Cool";
        //}
        //else if (slider.value > 80 && slider.value < 90)
        //{
        //    handleSlider.GetComponent<Image>().sprite = emotions.smileFace;
        //    emotionText.text = "Happy";
        //}
        //else if (slider.value >= 90)
        //{
        //    handleSlider.GetComponent<Image>().sprite = emotions.happyFace;
        //    emotionText.text = "Very happy";
        //}
    }

    public Slider GetSlider()
    {
        return slider;
    }
}

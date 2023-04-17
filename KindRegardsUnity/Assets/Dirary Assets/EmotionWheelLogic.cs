using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmotionWheelLogic : MonoBehaviour
{
    [Header("Images, sprites and text")]
    //public TextMeshProUGUI distanceText;
    //public TextMeshProUGUI angleText;

    public TextMeshProUGUI currentEmotionText;
    public string currentEmotion;
    public string currentMainEmotion;
    public string previousMainEmotion;
    public Image circle;
    public Image glow;
    public Image emotionSelectionIndicator;
    public Image spriteRenderer;
    public Emotions emotions;

    public DiaryUI diaryUI;
    public GameObject emotionWheelParentPanel;

    [Header("Measurements")]
    public float circleRadius = 100f;
    [Range(0.0f, 1.0f)]
    public float glowOpacity = 0.3f;

    Vector2 circlePos;
    Vector2 circlePosOffset;

    [Header("Pizza slices")]
    public Image happyPizzaSlice;
    public Image proudPizzaSlice;
    public Image angryPizzaSlice;
    public Image annoyedPizzaSlice;
    public Image sadPizzaSlice;
    public Image boredPizzaSlice;
    public Image tiredPizzaSlice;
    public Image calmPizzaSlice;


    Vector3 circleCenterInScreenSpace;




    [Header("Mouse position data")]
    public float distance; // The distance between the mouse and the center of the circle
    public float angle; // The angle between the mouse and the center of the circle

    public string amount;

    private void Start()
    {
        HideAllPizzaSlices();
    }

    void Update()
    {
        if(emotionWheelParentPanel.active)
        {
            // Check if the mouse is being clicked or held
            if (Input.GetMouseButton(0))
            {
                // Get the mouse position in screen space
                Vector2 mousePos = Input.mousePosition;

                // Get the center point of the circle in screen space
                circlePos = circle.rectTransform.position;

                Vector3 circlePosWithZ = new Vector3(circlePos.x, circlePos.y, -100);

                //circlePosOffset = new Vector2((circlePos.x + xOffset), (circlePos.y + yOffset));
                //circleCenterInScreenSpace = Camera.main.WorldToScreenPoint(circle.rectTransform.position);

                //triangle_p1 = circlePos;



                //List<Vector3> vertices = new List<Vector3>(circlePosWithZ);

                // Calculate the distance between the mouse and the center of the circle
                distance = Vector2.Distance(mousePos, circlePos);


                if (distance < circleRadius)
                {
                    // Calculate the angle between the mouse and the center of the circle
                    angle = Mathf.Atan2(mousePos.y - circlePos.y, mousePos.x - circlePos.x) * Mathf.Rad2Deg;
                    if (angle < 0)
                    {
                        angle += 360;
                    }

                    if (distance / circleRadius < 0.4)
                    { amount = "a bit "; }
                    else if (distance / circleRadius > 0.7)
                    { amount = "very "; }
                    else
                    { amount = ""; }



                    //distanceText.text = distance.ToString();
                    //angleText.text = angle.ToString();
                    DetermineSelectedEmotion();
                    diaryUI.newEntryEmotionText.text = currentEmotion;
                    diaryUI.newEntryEmotionImage.sprite = spriteRenderer.sprite;
                }
            }
        }
    }


    private void GenereateTriangle(int angleInterval)
    {


        //shape.CleanVertices();
        //Vector3 pointA = new Vector3(circlePosOffset.x, circlePosOffset.y, -13);
        //Vector3 pointB = new Vector3(
        //    (circlePosOffset.x + (circleRadius * Mathf.Cos(angleInterval * Mathf.Deg2Rad))),
        //    (circlePosOffset.y + (circleRadius * Mathf.Sin(angleInterval * Mathf.Deg2Rad))),
        //    -13
        //    );
        //Vector3 pointC = new Vector3(
        //   (circlePosOffset.x + (circleRadius * Mathf.Cos((angleInterval - 45) * Mathf.Deg2Rad))),
        //   (circlePosOffset.y + (circleRadius * Mathf.Sin((angleInterval - 45) * Mathf.Deg2Rad))),
        //   -13
        //   );

        //this.shape.vertices.Add(pointA);
        //this.shape.vertices.Add(pointB);
        //this.shape.vertices.Add(pointC);

        //shape.MakeMesh();

        //Debug.Log("Circle: " + pointA.x + ", " + pointA.y);
        //Debug.Log("PointB: " + pointB.x + ", " + pointB.y);
        //Debug.Log("PointC: " + pointC.x + ", " + pointC.y);
        //Debug.Log("Angle: " + angle);
    }

    private void HideAllPizzaSlices()
    {
        happyPizzaSlice.enabled = false;
        proudPizzaSlice.enabled = false;
        angryPizzaSlice.enabled = false;
        annoyedPizzaSlice.enabled = false;
        sadPizzaSlice.enabled = false;
        boredPizzaSlice.enabled = false;
        tiredPizzaSlice.enabled = false;
        calmPizzaSlice.enabled = false;
    }



    private void DetermineSelectedEmotion()
    {
        if (angle < 45)
        {
            currentMainEmotion = "happy";
            currentEmotion = amount + currentMainEmotion;
            currentEmotionText.text = currentEmotion;
            

            if (glow != null)
                glow.color = new Color(0.0166f, 0.830f, 0.0573f, glowOpacity); // Green


           
                HideAllPizzaSlices();
                happyPizzaSlice.enabled = true;
            


            switch (amount)
            {
                case "a bit ":
                    spriteRenderer.sprite = emotions.aBitHappy;
                    break;
                case "":
                    spriteRenderer.sprite = emotions.happy;
                    break;
                case "very ":
                    spriteRenderer.sprite = emotions.veryHappy;
                    break;
            }
        }
        else if (angle >= 45 && angle < 90)
        {
            currentMainEmotion = "proud";
            currentEmotion = amount + currentMainEmotion;
            currentEmotionText.text = currentEmotion;

            if (glow != null)
                glow.color = new Color(0.900f, 0.839f, 0.171f, glowOpacity); // Gold

   
                HideAllPizzaSlices();
                proudPizzaSlice.enabled = true;
          

            switch (amount)
            {
                case "a bit ":
                    spriteRenderer.sprite = emotions.aBitProud;
                    break;
                case "":
                    spriteRenderer.sprite = emotions.proud;
                    break;
                case "very ":
                    spriteRenderer.sprite = emotions.veryProud;
                    break;
            }

        }
        else if (angle >= 90 && angle < 135)
        {
            currentMainEmotion = "angry";
            currentEmotion = amount + currentMainEmotion;
            currentEmotionText.text = currentEmotion;

            if (glow != null)
                glow.color = new Color(0.730f, 0.00f, 0.00f, glowOpacity); // Red

           
                HideAllPizzaSlices();
                angryPizzaSlice.enabled = true;
            
            

            switch (amount)
            {
                case "a bit ":
                    spriteRenderer.sprite = emotions.aBitAngry;
                    break;
                case "":
                    spriteRenderer.sprite = emotions.angry;
                    break;
                case "very ":
                    spriteRenderer.sprite = emotions.veryAngry;
                    break;
            }
        }
        else if (angle >= 135 && angle < 180)
        {
            currentMainEmotion = "annoyed";
            currentEmotion = amount + currentMainEmotion;
            currentEmotionText.text = currentEmotion;

            if (glow != null)
                glow.color = new Color(0.920f, 0.419f, 0.00920f, glowOpacity); // Red

        
                HideAllPizzaSlices();
                annoyedPizzaSlice.enabled = true;
           

            switch (amount)
            {
                case "a bit ":
                    spriteRenderer.sprite = emotions.aBitAnnoyed;
                    break;
                case "":
                    spriteRenderer.sprite = emotions.annoyed;
                    break;
                case "very ":
                    spriteRenderer.sprite = emotions.veryAnnoyed;
                    break;
            }
        }
        else if (angle >= 180 && angle < 225)
        {
            currentMainEmotion = "sad";
            currentEmotion = amount + currentMainEmotion;
            currentEmotionText.text = currentEmotion;

            if (glow != null)
                glow.color = new Color(0.198f, 0.316f, 0.790f, glowOpacity); // Blue

          
                HideAllPizzaSlices();
                sadPizzaSlice.enabled = true;
           

            switch (amount)
            {
                case "a bit ":
                    spriteRenderer.sprite = emotions.aBitSad;
                    break;
                case "":
                    spriteRenderer.sprite = emotions.sad;
                    break;
                case "very ":
                    spriteRenderer.sprite = emotions.verySad;
                    break;
            }
        }
        else if (angle >= 225 && angle < 270)
        {
            currentMainEmotion = "bored";
            currentEmotion = amount + currentMainEmotion;
            currentEmotionText.text = currentEmotion;

            if (glow != null)
                glow.color = new Color(0.290f, 0.290f, 0.290f, glowOpacity); // Grey

           
                HideAllPizzaSlices();
                boredPizzaSlice.enabled = true;
         

            switch (amount)
            {
                case "a bit ":
                    spriteRenderer.sprite = emotions.aBitBored;
                    break;
                case "":
                    spriteRenderer.sprite = emotions.bored;
                    break;
                case "very ":
                    spriteRenderer.sprite = emotions.veryBored;
                    break;
            }

        }
        else if (angle >= 270 && angle < 315)
        {
            currentMainEmotion = "tired";
            currentEmotion = amount + currentMainEmotion;
            currentEmotionText.text = currentEmotion;

            if (glow != null)
                glow.color = new Color(0.547f, 0.710f, 0.574f, glowOpacity); // Pale green

      
                HideAllPizzaSlices();
                tiredPizzaSlice.enabled = true;
            

            switch (amount)
            {
                case "a bit ":
                    spriteRenderer.sprite = emotions.aBitTired;
                    break;
                case "":
                    spriteRenderer.sprite = emotions.tired;
                    break;
                default:
                    spriteRenderer.sprite = emotions.veryTired;
                    break;
            }
        }
        else if (angle >= 315 && angle < 360)
        {
            currentMainEmotion = "calm";
            currentEmotion = amount + currentMainEmotion;
            currentEmotionText.text = currentEmotion;

            if (glow != null)
                glow.color = new Color(0.0621f, 0.690f, 0.596f, glowOpacity); // Green

           
                HideAllPizzaSlices();
                calmPizzaSlice.enabled = true;
           

            switch (amount)
            {
                case "a bit ":
                    spriteRenderer.sprite = emotions.aBitCalm;
                    break;
                case "":
                    spriteRenderer.sprite = emotions.calm;
                    break;
                default:
                    spriteRenderer.sprite = emotions.veryCalm;
                    break;
            }
        }
    }
}
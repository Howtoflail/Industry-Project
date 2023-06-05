using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PaperAnimation : MonoBehaviour
{
    [SerializeField]
    private Text textRenderer;
    [SerializeField]
    private float animationTime = 6f;

    private Material material;
    private bool animationStarted;
    private bool animationFinished;
    private bool animationReversedFinished;
    private float timeWhenObjectActivated;
    private float timeWhenStarted;
    private float timeWhenFinished;
    private float timeToDisplayAnimation = 2.5f;
    private float timeToDisplayAnimationReversed = 0.8f;
    private float timeToWaitUntilStartingReverseAnimation;
    private float visibilityPercentage;

    public void AnimateTextOnPaper()
    {
        animationStarted = true;
        timeWhenStarted = Time.time;
    }

    void Start()
    {
        timeWhenObjectActivated = Time.time;
        material = textRenderer.material;
        visibilityPercentage = 0f;
        material.SetFloat("_Visibility", visibilityPercentage);  
    }

    void Update()
    {
        if (animationStarted == true && visibilityPercentage < 1f)
        {
            Debug.Log($"Visibility percentage is: {visibilityPercentage}");
            visibilityPercentage = Time.time / (timeWhenStarted + timeToDisplayAnimation);
            material.SetFloat("_Visibility", visibilityPercentage);
        }
        if (visibilityPercentage >= 0.999f && animationFinished == false)
        {
            animationFinished = true;
            animationReversedFinished = true;
            animationStarted = false;
            timeWhenFinished = Time.time;
            timeToWaitUntilStartingReverseAnimation = timeWhenFinished + 1.5f;
            visibilityPercentage = 0f;
        }
        if(animationReversedFinished == true && Time.time >= timeToWaitUntilStartingReverseAnimation) 
        {
            Debug.Log($"Time when finished at start is: {timeToWaitUntilStartingReverseAnimation}");
            visibilityPercentage = 1f - (Time.time / (timeToWaitUntilStartingReverseAnimation + timeToDisplayAnimationReversed));
            material.SetFloat("_Visibility", visibilityPercentage);
        }

        if(Time.time >= (timeWhenObjectActivated + animationTime))
        {
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPop : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    UiScriptable UiScriptableValues;

    public bool useDefaultValues;

    [SerializeField]
    private float buttonShrink;

    [SerializeField]
    private float buttonMove;

    [SerializeField]
    GameObject buttonObject;

    void Start() { }

    // Update is called once per frame
    void Update() { }

    // public void PopButton()
    // {
    //     transform.localScale -= new Vector3(buttonShrink, buttonShrink, 0);
    //     transform.position += new Vector3(buttonMove, buttonMove, 0);
    // }

    public void ButtonDown()
    {
        float shrink;
        float shift;

        if (useDefaultValues)
        {
            shrink = UiScriptableValues.uiButtonDownShrinkAmount;
            shift = UiScriptableValues.uiButtionDownShiftAmount;
        }
        else
        {
           shrink = buttonShrink;
           shift = buttonMove; 
        }
        buttonObject.transform.localScale -= new Vector3(shrink, shrink, 0);
        buttonObject.transform.position += new Vector3(0, shift, 0);
    }

    public void ButtonUp()
    {
        float shrink;
        float shift;

        if (useDefaultValues)
        {
            shrink = UiScriptableValues.uiButtonDownShrinkAmount;
            shift = UiScriptableValues.uiButtionDownShiftAmount;
        }
        else
        {
           shrink = buttonShrink;
           shift = buttonMove; 
        }
        buttonObject.transform.localScale += new Vector3(shrink, shrink, 0);
        buttonObject.transform.position -= new Vector3(0, shift, 0);
    }
}

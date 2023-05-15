using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float buttonShrink;

    [SerializeField]
    private float buttonMove;

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
        transform.localScale -= new Vector3(buttonShrink, buttonShrink, 0);
        transform.position += new Vector3(0, buttonMove, 0);
    }

    public void ButtonUp()
    {
        transform.localScale += new Vector3(buttonShrink, buttonShrink, 0);
        transform.position -= new Vector3(0, buttonMove, 0);
    }
}

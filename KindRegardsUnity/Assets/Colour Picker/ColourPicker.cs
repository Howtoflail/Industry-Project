using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourPicker : MonoBehaviour
{
    [SerializeField] 
    RectTransform _texture;

    //owl
    [SerializeField] 
    GameObject owlBody;
    [SerializeField] 
    GameObject owlFace;
    [SerializeField] 
    GameObject owlWingRight;
    [SerializeField] 
    GameObject owlWingLeft;
    [SerializeField] 
    GameObject owlTail;

    //cat
    [SerializeField]
    GameObject catBody;
    [SerializeField]
    GameObject catFace;
    [SerializeField]
    GameObject catEarLeft;
    [SerializeField]
    GameObject catEarRight;
    [SerializeField]
    GameObject catTail;

    //dog
    [SerializeField]
    GameObject dogBody;
    [SerializeField]
    GameObject dogFace;
    [SerializeField]
    GameObject dogEarLeft;
    [SerializeField]
    GameObject dogEarRight;
    [SerializeField]
    GameObject dogTail;

    //rabbit
    [SerializeField]
    GameObject rabbitBody;
    [SerializeField]
    GameObject rabbitFace;
    [SerializeField]
    GameObject rabbitEarLeft;
    [SerializeField]
    GameObject rabbitEarRight;

    //owl messages
    [SerializeField] 
    GameObject owlBody2;
    [SerializeField] 
    GameObject owlFace2;
    [SerializeField] 
    GameObject owlWingRight2;
    [SerializeField] 
    GameObject owlWingLeft2;
    [SerializeField] 
    GameObject owlTail2;

    //cat messages
    [SerializeField]
    GameObject catBody2;
    [SerializeField]
    GameObject catFace2;
    [SerializeField]
    GameObject catEarLeft2;
    [SerializeField]
    GameObject catEarRight2;
    [SerializeField]
    GameObject catTail2;

    //dog messages
    [SerializeField]
    GameObject dogBody2;
    [SerializeField]
    GameObject dogFace2;
    [SerializeField]
    GameObject dogEarLeft2;
    [SerializeField]
    GameObject dogEarRight2;
    [SerializeField]
    GameObject dogTail2;

    //rabbit messages
    [SerializeField]
    GameObject rabbitBody2;
    [SerializeField]
    GameObject rabbitFace2;
    [SerializeField]
    GameObject rabbitEarLeft2;
    [SerializeField]
    GameObject rabbitEarRight2;


    [SerializeField]
    Texture2D _refSprite;

    private Color c;

    public void OnClickPickerColor()
    {
        SetColour();
    }

    private void SetColour()
    {
        Vector3 imagePos = _texture.position;
        float globalPosX = Input.mousePosition.x - imagePos.x;
        float globalPosY = Input.mousePosition.y - imagePos.y;

        int localPosX = (int)(globalPosX *(_refSprite.width / _texture.rect.width));
        int localPosy = (int)(globalPosY *(_refSprite.height / _texture.rect.height));

        c = _refSprite.GetPixel(localPosX, localPosy);
        SetActualColour(c);
    }

    public void SetActualColour (Color c)
    {
        //owl
        owlBody.GetComponent<MeshRenderer>().material.color = c;
        owlFace.GetComponent<MeshRenderer>().material.color = c;
        owlWingLeft.GetComponent<MeshRenderer>().material.color = c;
        owlWingRight.GetComponent<MeshRenderer>().material.color = c;
        owlTail.GetComponent<MeshRenderer>().material.color = c;

        //cat
        catBody.GetComponent<MeshRenderer>().material.color = c;
        catFace.GetComponent<MeshRenderer>().material.color = c;
        catEarLeft.GetComponent<MeshRenderer>().material.color = c;
        catEarRight.GetComponent<MeshRenderer>().material.color = c;
        catTail.GetComponent<MeshRenderer>().material.color = c;

        //dog
        dogBody.GetComponent<MeshRenderer>().material.color = c;
        dogFace.GetComponent<MeshRenderer>().material.color = c;
        dogEarLeft.GetComponent<MeshRenderer>().material.color = c;
        dogEarRight.GetComponent<MeshRenderer>().material.color = c;
        dogTail.GetComponent<MeshRenderer>().material.color = c;

        //rabbit
        rabbitBody.GetComponent<MeshRenderer>().material.color = c;
        rabbitFace.GetComponent<MeshRenderer>().material.color = c;
        rabbitEarRight.GetComponent<MeshRenderer>().material.color = c;
        rabbitEarLeft.GetComponent<MeshRenderer>().material.color = c;

        //owl messages
        owlBody2.GetComponent<MeshRenderer>().material.color = c;
        owlFace2.GetComponent<MeshRenderer>().material.color = c;
        owlWingLeft2.GetComponent<MeshRenderer>().material.color = c;
        owlWingRight2.GetComponent<MeshRenderer>().material.color = c;
        owlTail2.GetComponent<MeshRenderer>().material.color = c;

        //cat messages
        catBody2.GetComponent<MeshRenderer>().material.color = c;
        catFace2.GetComponent<MeshRenderer>().material.color = c;
        catEarLeft2.GetComponent<MeshRenderer>().material.color = c;
        catEarRight2.GetComponent<MeshRenderer>().material.color = c;
        catTail2.GetComponent<MeshRenderer>().material.color = c;

        //dog messages
        dogBody2.GetComponent<MeshRenderer>().material.color = c;
        dogFace2.GetComponent<MeshRenderer>().material.color = c;
        dogEarLeft2.GetComponent<MeshRenderer>().material.color = c;
        dogEarRight2.GetComponent<MeshRenderer>().material.color = c;
        dogTail2.GetComponent<MeshRenderer>().material.color = c;

        //rabbit messages
        rabbitBody2.GetComponent<MeshRenderer>().material.color = c;
        rabbitFace2.GetComponent<MeshRenderer>().material.color = c;
        rabbitEarRight2.GetComponent<MeshRenderer>().material.color = c;
        rabbitEarLeft2.GetComponent<MeshRenderer>().material.color = c;
    }

    public Color getColor()
    {
        UnityEngine.Debug.Log(c);
        return c;
    }
}

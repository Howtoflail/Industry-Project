using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraClickController : MonoBehaviour
{
    private UIController uiController;
    private MessageController messageController;
    public float rayLength;
    public LayerMask layermask;

    private void Start()
    {
       uiController = Resources.FindObjectsOfTypeAll<UIController>()[0];
       messageController = Resources.FindObjectsOfTypeAll<MessageController>()[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, rayLength, layermask))
            {
                if (Enum.TryParse<PetStateEnum>(hit.collider.name, out _))
                {
                    uiController.Forward(14);

                    // Reset selected message
                    messageController.messageText = "";

                    // Set the pet name in the letter
                    TextMeshProUGUI t = messageController.regardsTextObj.GetComponent<TMPro.TextMeshProUGUI>();
                    t.SetText("Kind regards,\nfriendly " + hit.collider.name + " owner");
                }
            }
        }
    }
}

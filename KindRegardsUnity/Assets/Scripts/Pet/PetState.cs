using System.Collections.Generic;
using UnityEngine;

public class PetState : MonoBehaviour
{
    [SerializeField] List<PetStateEnum> petStates;

    public void DetectActive(PetStateEnum currentState)
    {
        if (petStates.Contains(currentState)) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}

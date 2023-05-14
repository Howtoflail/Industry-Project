using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRemove : MonoBehaviour
{
    void Awake()
	{
		Object.DontDestroyOnLoad(this.gameObject);
	}
}

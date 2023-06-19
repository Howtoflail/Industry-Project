using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TransitionBehaviour : MonoBehaviour
{
	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void Open()
	{
		animator.SetBool("Open", true);
	}

	public void Close()
	{
		animator.SetBool("Close", false);
	}
}

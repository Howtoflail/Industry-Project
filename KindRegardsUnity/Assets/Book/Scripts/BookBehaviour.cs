using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BookBehaviour : MonoBehaviour
{
	private Animator animator;
	
	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
	public void SetOpen(bool open)
	{
		animator.SetBool("Open", open);
	}
	
	public void FlipRight()
	{
		animator.SetTrigger("FlipRight");
	}
	
	public void FlipLeft()
	{
		animator.SetTrigger("FlipLeft");
	}
}

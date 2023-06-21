using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class BookDirector : MonoBehaviour
{
	private PlayableDirector director;
	[SerializeField]
	private PlayableAsset openScene;
	[SerializeField]
	private PlayableAsset closeScene;
	
	void Start()
	{
		director = GetComponent<PlayableDirector>();
	}
	
	public void Open()
	{
		director.Play(openScene);
	}
	
	public void Close()
	{
		director.Play(closeScene);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollTexture : MonoBehaviour
{
	public float scrollSpeed = 0.5f;
	private RawImage render;

	void Start()
	{
		render = GetComponent<RawImage>();
	}

	void Update()
	{
		render.uvRect = new Rect(new Vector2(1, 1) * ((Time.time * scrollSpeed) % 1), render.uvRect.size);
	}
}

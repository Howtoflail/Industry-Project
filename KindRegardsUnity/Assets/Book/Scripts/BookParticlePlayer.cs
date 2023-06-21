using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookParticlePlayer : MonoBehaviour
{
	public ParticleSystem closingParticles;

	public void PlayCloseDust()
	{
		if (closingParticles != null)
		{
			closingParticles.Play();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VFXDisabler : MonoBehaviour
{
	[SerializeField] ParticleSystem particleSystem;

	private void Update()
	{
		if(particleSystem.isPlaying)
		{
			return;
		}
		else
		{
			this.gameObject.SetActive(false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenDetector : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Top"))
		{
			GameManager.Instance.OnTopFallen();
		}
		else if(other.gameObject.CompareTag("Mid"))
		{
			GameManager.Instance.OnMidFallen();
		}
	}
}

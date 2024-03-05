using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenDetector : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Top"))
		{
			OnTopFallen();
		}
		else if(other.gameObject.CompareTag("Mid"))
		{
			OnMidFallen();
		}
	}

	public void OnTopFallen()
	{
		GameManager.Instance.IncrementTopFallenObjectsCount();
		Debug.Log("Top Object fallen");
	}
	public void OnMidFallen()
	{
		GameManager.Instance.IncrementMidFallenObjectsCount();
		Debug.Log("Mid Object fallen");
	}
}

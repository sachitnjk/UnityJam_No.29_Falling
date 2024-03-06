using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenDetector : MonoBehaviour
{
	[SerializeField] private int maxFallenTopObjects;
	[SerializeField] private int maxFallenMidObjects;

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
		NextLevelCheck();
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

	private void NextLevelCheck()
	{
		if(GameManager.Instance.currentFallenTopObjects >= maxFallenTopObjects && GameManager.Instance.currentFallenMidObjects >= maxFallenMidObjects)
		{
			Debug.Log("Next Level Available");
		}
	}
}

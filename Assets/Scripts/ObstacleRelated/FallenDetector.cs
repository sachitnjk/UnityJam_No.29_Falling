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
			Debug.Log(other.gameObject.name);
		}
		else if(other.gameObject.CompareTag("Mid"))
		{
			GameManager.Instance.OnMidFallen();
			Debug.Log(other.gameObject.name);
		}
	}
}

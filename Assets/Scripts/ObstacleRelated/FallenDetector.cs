using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenDetector : MonoBehaviour
{
	[SerializeField] private int maxFallenTopObjects;
	[SerializeField] private int maxFallenMidObjects;

	private int currentFallenTopCount;
	private int currentFallenMidCount;

	private void Start()
	{
		if(currentFallenTopCount > 0 || currentFallenMidCount > 0)
		{
			ResetFallenObjectsCount(0);
		}

		EventManager.Instance.OnNextlevelTrigger += HandleOnNextLevelTrigger;
	}
	private void OnDestroy()
	{
		EventManager.Instance.OnNextlevelTrigger += HandleOnNextLevelTrigger;
	}

	private void Update()
	{
		Debug.Log(currentFallenTopCount);
		Debug.Log(currentFallenMidCount);
	}

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

		GameManager.Instance.SetCurrentFallenTopCount(currentFallenTopCount);
		GameManager.Instance.SetCurrentFallenMidCount(currentFallenMidCount);
	}

	private void OnTopFallen()
	{
		currentFallenTopCount++;
	}
	private void OnMidFallen()
	{
		currentFallenMidCount++;
	}
	private void ResetFallenObjectsCount(int resetCount)
	{
		currentFallenTopCount = resetCount;
		currentFallenMidCount = resetCount;
	}
	private void NextLevelCheck()
	{
		if(currentFallenTopCount >= maxFallenTopObjects && currentFallenMidCount >= maxFallenMidObjects)
		{
			EventManager.Instance.InvokeOnNextLevelAvailable();
		}
	}

	private void HandleOnNextLevelTrigger()
	{
		ResetFallenObjectsCount(0);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenDetector : MonoBehaviour
{
	[SerializeField] int maxFallenTopObjects;
	[SerializeField] int maxFallenMidObjects;
	[SerializeField] ScoreManager scoreManager;

	private int currentFallenTopCount;
	private int currentFallenMidCount;

	private void Start()
	{
		if(currentFallenTopCount > 0 || currentFallenMidCount > 0)
		{
			ResetFallenObjectsCount(0);
		}

		GameManager.Instance.maxFallenMidObjects = maxFallenMidObjects;
		GameManager.Instance.maxFallenTopObjects = maxFallenTopObjects;

		EventManager.Instance.OnNextLevelTrigger += HandleOnNextLevelTrigger;
	}
	private void OnDestroy()
	{
		EventManager.Instance.OnNextLevelTrigger += HandleOnNextLevelTrigger;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Top"))
		{
			if (other.GetComponent<ObstacleData>() != null)
			{
				scoreManager.AddScore(other.GetComponent<ObstacleData>().GetScore());
			}
			OnTopFallen();
		}
		else if (other.gameObject.CompareTag("Mid"))
		{
			if (other.GetComponent<ObstacleData>() != null)
			{
				scoreManager.AddScore(other.GetComponent<ObstacleData>().GetScore());
			}
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

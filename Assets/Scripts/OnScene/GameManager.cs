using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	private int fallenTopObjects;
	private int fallenMidObjects;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}

		ResetFallenObjectCounts();
	}

	private void OnLevelComplete()
	{
		ResetFallenObjectCounts();
	}
	private void ResetFallenObjectCounts()
	{
		fallenTopObjects = 0;
		fallenMidObjects = 0;
	}

	public void OnTopFallen()
	{
		Debug.Log("Top Object fallen");
	}
	public void OnMidFallen()
	{
		Debug.Log("Mid Object fallen");
	}
}

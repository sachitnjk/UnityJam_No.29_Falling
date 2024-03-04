using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public Action OnTopFallen;
	public Action OnMidFallen;

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
	}

	public void InvokeOnTopFallen()
	{
		OnTopFallen?.Invoke();
	}
	public void InvokeOnMidFallen()
	{
		OnMidFallen?.Invoke();
	}
}

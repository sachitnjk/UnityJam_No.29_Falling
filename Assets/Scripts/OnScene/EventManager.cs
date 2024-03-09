using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager Instance;

	public Action OnNextLevelAvailable;
	public Action OnNextlevelTrigger;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{ 
			Destroy(this.gameObject); 
		}
	}

	//Action event invoke functions
	public void InvokeOnNextLevelAvailable()
	{
		OnNextLevelAvailable?.Invoke();
	}
	public void InvokeOnNextLevelTrigger()
	{
		OnNextlevelTrigger?.Invoke();
	}
}

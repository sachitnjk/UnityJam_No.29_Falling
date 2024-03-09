using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager Instance;

	public Action OnNextLevelAvailable;
	public Action OnNextLevelTrigger;
	public Action OnLevelReset;
	public Action OnCannonShoot;
	public Action<float> OnAddScoreTrigger;

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
	public void InvokeOnLevelReset()
	{
		OnLevelReset?.Invoke();
	}
	public void InvokeOnNextLevelAvailable()
	{
		OnNextLevelAvailable?.Invoke();
	}
	public void InvokeOnNextLevelTrigger()
	{
		OnNextLevelTrigger?.Invoke();
	}
	public void InvokeOnAddScoreTrigger(float score)
	{
		OnAddScoreTrigger?.Invoke(score);
	}
	public void InvokeOnCannonShoot()
	{
		OnCannonShoot?.Invoke();
	}
}

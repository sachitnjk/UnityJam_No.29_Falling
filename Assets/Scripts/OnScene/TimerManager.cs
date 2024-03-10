using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float elaspedTime;
    private bool timerStop;

	private void Start()
	{
        timerStop = false;

        EventManager.Instance.OnNextLevelAvailable += HandleOnNextLevelAvailable;
        EventManager.Instance.OnNextLevelTrigger += HandleOnNextLevelTrigger;
		EventManager.Instance.OnLevelReset += HandleOnLevelReset;
	}
	private void OnDestroy()
	{
		EventManager.Instance.OnNextLevelAvailable -= HandleOnNextLevelAvailable;
		EventManager.Instance.OnNextLevelTrigger -= HandleOnNextLevelTrigger;
		EventManager.Instance.OnLevelReset -= HandleOnLevelReset;
	}
	void Update()
    {
        if(!timerStop)
        {
            elaspedTime += Time.deltaTime;
        }
        else
        {
            return;
        }
    }

	public string GetTimerTime()
	{
		int seconds = Mathf.FloorToInt(elaspedTime % 60);
		int minutes = Mathf.FloorToInt(elaspedTime / 60);
        return string.Format("{00:00} : {01:00}", minutes, seconds);
	}

    private void HandleOnNextLevelAvailable()
    {
        timerStop = true;
    }
	private void HandleOnNextLevelTrigger()
	{
        elaspedTime = 0;
        timerStop = false;
	}
    private void HandleOnLevelReset()
    {
		timerStop = true;

		elaspedTime = 0;
		timerStop = false;
	}
}

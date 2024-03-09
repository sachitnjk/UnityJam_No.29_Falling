using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[Range(1f, 5f)]
	[SerializeField] float maxScoreMultiplier;
	[Range(0, 1f)]
	[SerializeField] float mutliplierDecRateInSec;

    private float currentScore;
    private float currentScoreMultiplier;

	void Start()
    {
        currentScoreMultiplier = (Mathf.Round(maxScoreMultiplier * 10.0f) * 0.1f);

        EventManager.Instance.OnNextlevelTrigger += HandleNextLevelTrigger;
    }

    void Update()
    {
        if(currentScoreMultiplier > 1f){
            currentScoreMultiplier -= Time.deltaTime * mutliplierDecRateInSec;
        }
        else
        {
            currentScoreMultiplier = 1f;
        }
        currentScoreMultiplier = (Mathf.Round(currentScoreMultiplier * 10.0f) * 0.1f);
    }


	public void AddScore(float amount)
	{
		currentScore += amount * (Mathf.Round(currentScoreMultiplier * 10.0f) * 0.1f);

        if(GameManager.Instance != null)
        {
            GameManager.Instance.CurrentScore = currentScore;
        }
		Debug.Log(GameManager.Instance.CurrentScore);
	}

	private void HandleNextLevelTrigger()
	{
		currentScoreMultiplier = (Mathf.Round(maxScoreMultiplier * 10.0f) * 0.1f);
	}
}

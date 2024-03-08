using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] float maxScoreMultiplier;
    [SerializeField] [Range(0, 1f)] float mutliplierDecRateInSec;

    private float currentScore;
    float currentScoreMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        currentScoreMultiplier = maxScoreMultiplier;
        GameManager.Instance.currentScore = Mathf.Round(currentScore);
        GameManager.Instance.currentScoreMultiplier = (Mathf.Round(currentScoreMultiplier * 10.0f) * 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isScoreXDecreaseActive){
            if(currentScoreMultiplier > 1f){
                currentScoreMultiplier -= Time.deltaTime * mutliplierDecRateInSec;
            }else{
                currentScoreMultiplier = 1f;
            }
            GameManager.Instance.currentScoreMultiplier = (Mathf.Round(currentScoreMultiplier * 10.0f) * 0.1f);
        }
    }

    public void AddScore(float amount){
        currentScore += amount * (Mathf.Round(currentScoreMultiplier * 10.0f) * 0.1f);
        GameManager.Instance.currentScore = Mathf.Round(currentScore);
    }
}

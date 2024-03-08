using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[SerializeField] private GameObject nextLevelPanel;
	[SerializeField] private TextMeshProUGUI timerText;
	[SerializeField] private TextMeshProUGUI ammoText;
	[SerializeField] private TextMeshProUGUI topObjectLeftText;
	[SerializeField] private TextMeshProUGUI midObjectLeftText;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI scoreMultiplierText;

	//result
	[SerializeField] private TextMeshProUGUI resultTimerText;
	[SerializeField] private TextMeshProUGUI resultScoreText;
	[SerializeField] private TextMeshProUGUI resultHighScoreText;

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

	private void Start()
	{
		nextLevelPanel.SetActive(false);

		EventManager.Instance.OnNextLevelAvailable += HandleOnNextLevelAvailable;
	}

	private void Update(){
		SetTimerPanel(GameManager.Instance.elaspedTime);
		SetAmmoText(GameManager.Instance.projectilePool);
		SetRemainingBoxText((GameManager.Instance.maxFallenTopObjects - GameManager.Instance.currentFallenTopObjects), (GameManager.Instance.maxFallenMidObjects - GameManager.Instance.currentFallenMidObjects));
		SetScoreText(GameManager.Instance.currentScore, GameManager.Instance.currentScoreMultiplier);
	}

	private void OnDestroy()
	{
		EventManager.Instance.OnNextLevelAvailable -= HandleOnNextLevelAvailable;
	}

	//Button OnClick functions
	public void ResetNextLevelPanel()
	{
		GameManager.Instance.SetCanMoveStatus(true);
		EventManager.Instance.InvokeOnNextLevelTrigger();
		nextLevelPanel.SetActive(false);
	}

	//Event handles
	private void HandleOnNextLevelAvailable()
	{
		GameManager.Instance.SetCanMoveStatus(false);
		GameManager.Instance.SetIsTimerActiveStatus(false);
		GameManager.Instance.isScoreXDecreaseActive = false;
		SetResultText(GameManager.Instance.currentScore);
		nextLevelPanel.SetActive(true);
	}

	//Set String of Timer Panel to the time parameter
	public void SetTimerPanel(float time){
		int minutes = Mathf.FloorToInt(time/60);
		int seconds = Mathf.FloorToInt(time%60);
		timerText.text = string.Format("{00:00} : {01:00}", minutes, seconds);
	}
	public void SetAmmoText(List<GameObject> projectilePools){
		var builder = new StringBuilder();
		int remainingAmmo = 0;
		foreach(GameObject projectilePool in projectilePools){
			if(!projectilePool.activeSelf){
				remainingAmmo++;
			}
		}
		for(int i = 0; i < projectilePools.Count; i++){
			if(remainingAmmo > 0){
				builder.Append('O');
				remainingAmmo--;
			}else{
				builder.Append('X');
			}
		}
		ammoText.text = builder.ToString();
	}
	public void SetRemainingBoxText(int topBox, int midBox){
		if(topBox < 0) topBox = 0;
		if(midBox < 0) midBox = 0;
		topObjectLeftText.text = "TOP : " + topBox;
		midObjectLeftText.text = "MID : " + midBox;
	}
	public void SetScoreText(float score, float multiplier){
		scoreText.text = "SCORE : " + score.ToString("00000");
		scoreMultiplierText.text = "x" + multiplier.ToString("0.0");
	}

	public void SetResultText(float score){
		resultTimerText.text = timerText.text;
		resultScoreText.text = scoreText.text;
		resultHighScoreText.text = "HIGHSCORE : " + score.ToString("00000");
	}
}

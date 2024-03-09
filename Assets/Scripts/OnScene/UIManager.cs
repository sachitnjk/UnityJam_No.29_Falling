using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[SerializeField] private GameObject nextLevelPanel;
	[SerializeField] private TextMeshProUGUI currentScoreTextBox;
	[SerializeField] private TextMeshProUGUI currentAmmoTextBox;

	[SerializeField] private PlayerShoot playerShootScript;

	private int currentSceneIndex;
	private int nextSceneIndex;
	private float currentUpdatedScore;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private void Start()
	{
		nextLevelPanel.SetActive(false);

		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		nextSceneIndex = currentSceneIndex + 1;

		EventManager.Instance.OnNextLevelAvailable += HandleOnNextLevelAvailable;
		EventManager.Instance.OnAddScoreTrigger += HandleOnAddScoreTrigger;
	}
	private void OnDestroy()
	{
		EventManager.Instance.OnNextLevelAvailable -= HandleOnNextLevelAvailable;
		EventManager.Instance.OnAddScoreTrigger -= HandleOnAddScoreTrigger;
	}
	private void Update()
	{
		SetAmmoText();
	}

	private void IncreaseCurrentSceneIndex()
	{
		currentSceneIndex = nextSceneIndex;
		nextSceneIndex = currentSceneIndex + 1;
	}
	private void ResetCurrentScore()
	{
		currentUpdatedScore = 0;
		currentScoreTextBox.text = currentUpdatedScore.ToString();
	}
	public void SetAmmoText()
	{
		currentAmmoTextBox.text = playerShootScript.ReturnAmmoCount();
	}

	//Button OnClick functions
	public void ResetNextLevelPanel()
	{
		if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
		{
			EventManager.Instance.InvokeOnNextLevelTrigger();
			GameManager.Instance.SetCanMoveStatus(true);
			nextLevelPanel.SetActive(false);

			SceneManager.LoadScene(nextSceneIndex);
			IncreaseCurrentSceneIndex();

			ResetCurrentScore();
		}
	}


	//Event handles
	private void HandleOnNextLevelAvailable()
	{
		GameManager.Instance.SetCanMoveStatus(false);
		nextLevelPanel.SetActive(true);
	}
	private void HandleOnAddScoreTrigger(float amount)
	{
		currentScoreTextBox.text = amount.ToString();
	}
}

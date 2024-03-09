using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[SerializeField] private GameObject nextLevelPanel;

	int currentSceneIndex;
	int nextSceneIndex;

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
	}
	private void OnDestroy()
	{
		EventManager.Instance.OnNextLevelAvailable -= HandleOnNextLevelAvailable;
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
		}
	}

	private void IncreaseCurrentSceneIndex()
	{
		currentSceneIndex = nextSceneIndex;
		nextSceneIndex = currentSceneIndex + 1;
	}

	//Event handles
	private void HandleOnNextLevelAvailable()
	{
		//Debug.Log("going here");
		GameManager.Instance.SetCanMoveStatus(false);
		nextLevelPanel.SetActive(true);
	}
}

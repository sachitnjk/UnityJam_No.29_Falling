using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[SerializeField] private GameObject nextLevelPanel;

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
	private void OnDestroy()
	{
		EventManager.Instance.OnNextLevelAvailable -= HandleOnNextLevelAvailable;
	}

	//Button OnClick functions
	public void ResetNextLevelPanel()
	{
		EventManager.Instance.InvokeOnNextLevelTrigger();
		nextLevelPanel.SetActive(false);
	}

	//Event handles
	private void HandleOnNextLevelAvailable()
	{
		nextLevelPanel.SetActive(true);
	}

}

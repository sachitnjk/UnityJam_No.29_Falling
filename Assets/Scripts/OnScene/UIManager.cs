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
	}

	private void Update()
	{
		//Note to self: Make this into an event later and replace the IsNextLevelAvailable
		if(GameManager.Instance.IsNextLevelAvailable)
		{
			nextLevelPanel.SetActive(true);
		}
		else
		{
			nextLevelPanel.SetActive(false);
		}
	}
}

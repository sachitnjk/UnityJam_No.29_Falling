using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] private GameObject mainMenuPanel;
	[SerializeField] private GameObject settingsPanel;
	[SerializeField] private GameObject audioPanel;
	[SerializeField] private GameObject controlsPanel;

	public void PlayButton()
	{
		SceneManager.LoadScene(1);
	}
	public void OpenSettings()
	{
		mainMenuPanel.SetActive(false);
		settingsPanel.SetActive(true);
	}
	public void OpenAudio()
	{
		settingsPanel.SetActive(false);
		audioPanel.SetActive(true);
	}
	public void OpenControls()
	{
		settingsPanel.SetActive(false);
		controlsPanel.SetActive(true);
	}
	public void ReturnToMainMenu()
	{
		settingsPanel.SetActive(false);
		mainMenuPanel.SetActive(true);
	}
	public void ReturnToSettings()
	{
		audioPanel.SetActive(false);
		controlsPanel.SetActive(false);
		settingsPanel.SetActive(true);
	}
	public void ExitGame()
	{
		Application.Quit();
	}
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerShoot : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction shootAction;
	private InputAction aimAction;
	private InputAction resetLevel;

	[SerializeField] private SimulatedProjection trajectorySimulation;
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Projectile prefabProjectileScript;
	[SerializeField] private Transform spawnPoint;

	[SerializeField] private float shootVelocity;

	private GameObject cannonProjectile;
	private Projectile projectileScript;

	private bool isReloading;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		if(playerInput != null) 
		{
			shootAction = playerInput.actions["Shoot"];
			aimAction = playerInput.actions["Aim"];
			resetLevel = playerInput.actions["resetLevel"];
		}
	}

	private void Update()
	{
		if (GameManager.Instance != null && GameManager.Instance.CanMove)
		{
			Shoot();
			Aim();
			ResetLevelCheck();
		}
	}

	private void Aim()
	{
		if(aimAction != null && aimAction.IsPressed())
		{
			trajectorySimulation.SimulateTrajectory(prefabProjectileScript, spawnPoint.position, spawnPoint.up * shootVelocity);
		}
		else if(aimAction.WasReleasedThisFrame())
		{
			trajectorySimulation.ResetSimulationAndTrajectory();
		}
	}

	private void Shoot()
	{
		if(shootAction != null && shootAction.WasPerformedThisFrame())
		{
			cannonProjectile = ObjectPooler.Instance.GetPooledObject(projectilePrefab);

			if(cannonProjectile != null) 
			{
				isReloading = false;

				projectileScript = cannonProjectile.GetComponent<Projectile>();

				cannonProjectile.SetActive(true);
				cannonProjectile.transform.position = spawnPoint.position;
				projectileScript.Init(spawnPoint.up * shootVelocity, false);
			}
			else
			{
				isReloading=true;
			}

			GameManager.Instance.SetIsReloadStatus(isReloading);
		}
	}
	private void ResetLevelCheck()
	{
		if(resetLevel.WasPerformedThisFrame()) 
		{
			string currentSceneName = SceneManager.GetActiveScene().name;
			SceneManager.LoadScene(currentSceneName);

			EventManager.Instance.InvokeOnLevelReset();
		}
	}

	public string GetAmmoCount()
	{
		var builder = new StringBuilder();
		int remainingAmmo = 0;
		foreach (GameObject projectilePool in ObjectPooler.Instance.GetPooledObjectsList(projectilePrefab))
		{
			if (!projectilePool.activeSelf)
			{
				remainingAmmo++;
			}
		}
		for (int i = 0; i < ObjectPooler.Instance.GetPooledObjectsList(projectilePrefab).Count; i++)
		{
			if (remainingAmmo > 0)
			{
				builder.Append('|');
				remainingAmmo--;
			}
			else
			{
				builder.Append('X');
			}
		}
		return builder.ToString();
	}
}

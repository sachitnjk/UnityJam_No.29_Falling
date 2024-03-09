using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction shootAction;
	private InputAction aimAction;

	[SerializeField] private SimulatedProjection trajectorySimulation;
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Transform spawnPoint;

	[SerializeField] private float shootVelocity;

	private GameObject cannonProjectile;
	private Projectile projectileScript;
	private Projectile prefabProjectileScript;

	private bool isReloading;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		if(playerInput != null) 
		{
			shootAction = playerInput.actions["Shoot"];
			aimAction = playerInput.actions["Aim"];
		}

		prefabProjectileScript = GameManager.Instance.prefabProjectileScript;

		//GameManager.Instance.projectilePool = GetProjectilePool();
	}

	private void Update()
	{
		if (GameManager.Instance != null && GameManager.Instance.CanMove)
		{
			Shoot();
			Aim();
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

	////Change
	//public List<GameObject> GetProjectilePool()
	//{
	//	return ObjectPooler.Instance.GetPooledObject(projectilePrefab);
	//}
}

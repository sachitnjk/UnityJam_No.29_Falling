using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction shootAction;

	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Transform spawnPoint;

	[SerializeField] private float shootVelocity;

	private GameObject cannonProjectile;
	private Projectile projectileScript;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		if(playerInput != null) 
		{
			shootAction = playerInput.actions["Shoot"];
		}
	}

	private void Update()
	{
		Shoot();
	}

	private void Shoot()
	{
		if(shootAction != null && shootAction.WasPerformedThisFrame())
		{
			cannonProjectile = ObjectPooler.Instance.GetPooledObject(projectilePrefab);
			projectileScript = cannonProjectile.GetComponent<Projectile>();

			if(cannonProjectile != null) 
			{
				cannonProjectile.SetActive(true);
				cannonProjectile.transform.position = spawnPoint.position;
				projectileScript.Init(shootVelocity, spawnPoint);

			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction shootAction;

	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Transform spawnPoint;

	[SerializeField] private float shootForce;

	private GameObject cannonProjectile;

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

			if(cannonProjectile != null) 
			{
				cannonProjectile.transform.position = spawnPoint.position;

				cannonProjectile.SetActive(true);

				Rigidbody cannonRb = cannonProjectile.GetComponent<Rigidbody>();
				if(cannonRb != null)
				{
					cannonRb.velocity = Vector3.zero;
					cannonRb.AddForce(spawnPoint.up * shootForce, ForceMode.Impulse);
				}

			}
		}
	}
}

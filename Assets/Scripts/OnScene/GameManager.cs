using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public Projectile prefabProjectileScript;
	[field: SerializeField] public Transform obstacleWallParent{ get; private set; }
	public int fallenTopObjects{ get; private set; }
	public int fallenMidObjects { get; private set; }

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

		ResetFallenObjectCounts();
	}

	private void HandleOnLevelComplete()
	{
		ResetFallenObjectCounts();
	}
	private void ResetFallenObjectCounts()
	{
		fallenTopObjects = 0;
		fallenMidObjects = 0;
	}

	public void IncrementTopFallenObjectsCount()
	{
		fallenTopObjects++;
	}
	public void IncrementMidFallenObjectsCount()
	{
		fallenMidObjects++;
	}
}

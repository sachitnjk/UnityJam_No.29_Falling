using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public Projectile prefabProjectileScript;

	public int currentFallenTopObjects{ get; private set; }
	public int maxFallenTopObjects{ get; set; }
	public int currentFallenMidObjects { get; private set; }
	public int maxFallenMidObjects{ get; set; }
	public bool IsReloading { get; private set; }
	public bool CanMove { get; private set; }
	public List<GameObject> projectilePool {get; set;}

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

		CanMove = true;
	}

	//---Setter functions---
	public void SetIsReloadStatus(bool isReloading)
	{
		IsReloading = isReloading;
	}
	public void SetCurrentFallenTopCount(int fallenTopCount)
	{
		currentFallenTopObjects = fallenTopCount;
	}
	public void SetCurrentFallenMidCount(int fallenMidCount)
	{
		currentFallenMidObjects = fallenMidCount;
	}
	public void SetCanMoveStatus(bool canMove)
	{
		CanMove = canMove;
	}
}

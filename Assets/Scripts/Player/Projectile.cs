using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;

	private float projectileSpeed;

	public void Init(float velocity, Transform spawnPoint)
	{
		rb.velocity = spawnPoint.up * velocity;
		StartCoroutine(ProjectileLifetime());
	}

	private IEnumerator ProjectileLifetime()
	{
		yield return new WaitForSeconds(5f);
		this.gameObject.SetActive(false);
	}
}

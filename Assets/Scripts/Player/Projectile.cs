using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name);
	}
}

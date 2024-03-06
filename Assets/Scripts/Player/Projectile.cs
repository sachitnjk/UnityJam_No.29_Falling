using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;

	private bool _isSimulated;

	public void Init(Vector3 velocity, bool isSimulated)
	{
		_isSimulated = isSimulated;
		rb.velocity = velocity;
		StartCoroutine(ProjectileLifetime());
	}

	private IEnumerator ProjectileLifetime()
	{
		yield return new WaitForSeconds(5f);
		this.gameObject.SetActive(false);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(_isSimulated) { return; }
		//Instantiate or enable VFX and Sounds here
	}
}

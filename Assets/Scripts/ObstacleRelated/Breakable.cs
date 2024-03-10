using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	[SerializeField] private GameObject brokenVersion;
	[SerializeField] private float breakForce = 2f;
	[SerializeField] private float onCollisionMultiplier = 100f;

	private bool isBroken;

	private void OnCollisionEnter(Collision collision)
	{
		if (isBroken) return;	
		if(collision.relativeVelocity.magnitude >= breakForce)
		{
			isBroken = true;

			GameObject altVersion = GameObject.Instantiate(brokenVersion, this.transform.position, this.transform.rotation);
			altVersion.transform.localScale = this.gameObject.transform.lossyScale;

			var brokenRbs = altVersion.GetComponentsInChildren<Rigidbody>();
			foreach(var rb in brokenRbs) 
			{
				rb.AddExplosionForce(collision.relativeVelocity.magnitude * onCollisionMultiplier, collision.contacts[0].point, 2);
			}

			this.gameObject.SetActive(false);
		}
	}
}

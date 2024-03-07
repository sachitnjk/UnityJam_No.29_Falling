using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ExplosiveBox : MonoBehaviour
{
	[SerializeField] private float triggerForce = 2f;
	[SerializeField] private float explodeRadius = 5f;
	[SerializeField] private float explodeForce = 100f;

	[SerializeField] private GameObject explosionEffect;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.relativeVelocity.magnitude >= triggerForce)
		{
			Collider[] envObjects = Physics.OverlapSphere(transform.position, explodeRadius);

			foreach (Collider obj in envObjects) 
			{
				Rigidbody rb = obj.GetComponent<Rigidbody>();
				if (rb == null) continue;

				rb.AddExplosionForce(explodeForce, transform.position, explodeRadius);
			}

			//rememebr to call the particles and sound here
			GameObject explosionParticles = ObjectPooler.Instance.GetPooledObject(explosionEffect);
			explosionParticles.transform.position = transform.position;
			explosionParticles.SetActive(true);

			this.gameObject.SetActive(false);
		}
	}
}

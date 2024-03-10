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
	[SerializeField] private AudioSource explosionSource;


	private const int maxDetectedColliders = 300;
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.relativeVelocity.magnitude >= triggerForce)
		{

			Collider[] envobjects = new Collider[maxDetectedColliders];
			int detectedColliders = Physics.OverlapSphereNonAlloc(transform.position, explodeRadius, envobjects);

			for (int i = 0; i < detectedColliders; i++)
			{
				Rigidbody rb = envobjects[i].GetComponent<Rigidbody>();
				if (rb != null)
				{
					rb.AddExplosionForce(explodeForce, transform.position, explodeRadius);
				}
			}

			#region Old OverlapSphere way

			//Collider[] envObjects = Physics.OverlapSphere(transform.position, explodeRadius);

			//foreach (Collider obj in envObjects)
			//{
			//	Rigidbody rb = obj.GetComponent<Rigidbody>();
			//	if (rb == null) continue;

			//	rb.AddExplosionForce(explodeForce, transform.position, explodeRadius);
			//}

			#endregion

			//rememebr to call the particles and sound here
			GameObject explosionParticles = ObjectPooler.Instance.GetPooledObject(explosionEffect);
			explosionParticles.transform.position = transform.position;
			explosionSource.PlayOneShot(explosionSource.clip);
			explosionParticles.SetActive(true);

			this.gameObject.SetActive(false);
		}
	}
}

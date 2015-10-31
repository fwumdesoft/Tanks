using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShellProjectile : NetworkBehaviour
{
	public float maxLifetime;

	void Start()
	{
		StartCoroutine(DestoryByTime());
	}

	IEnumerator DestoryByTime()
	{
		yield return new WaitForSeconds(maxLifetime);
		Destroy(gameObject);
	}

	public void Fire(Vector3 force)
	{
		GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
		NetworkServer.Spawn(gameObject);
	}

	void OnCollisionEnter(Collision collision)
	{
		//tell the tank to die! (and consequently everything else the collider hits)
		collision.collider.SendMessageUpwards("Die", SendMessageOptions.DontRequireReceiver);

		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().AddExplosionForce(300f, transform.position, 4f);
		StartCoroutine(PlayExplosionAndDestroy());
	}

	IEnumerator PlayExplosionAndDestroy()
	{
		GetComponent<AudioSource>().Play();
		GetComponent<ParticleSystem>().Play();
		float waitTime = Mathf.Max(GetComponent<AudioSource>().clip.length, GetComponent<ParticleSystem>().duration);
		yield return new WaitForSeconds(waitTime);
		Destroy(gameObject);
	}
}

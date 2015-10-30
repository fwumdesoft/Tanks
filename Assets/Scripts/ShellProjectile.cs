using UnityEngine;
using System.Collections;

public class ShellProjectile : MonoBehaviour
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
	}

	void OnCollisionEnter(Collision collision)
	{
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
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

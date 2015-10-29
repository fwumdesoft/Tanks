using UnityEngine;
using System.Collections;

public class ShellProjectile : MonoBehaviour
{
	public float lifetime;

	void Start()
	{
		StartCoroutine(DestoryByTime());
	}

	IEnumerator DestoryByTime()
	{
		yield return new WaitForSeconds(lifetime);
		Destroy(gameObject);
	}

	public void SetForce(Vector3 force)
	{
		GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
	}

	void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject);
	}
}

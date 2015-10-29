using UnityEngine;
using System.Collections;

public class ShellProjectile : MonoBehaviour
{
	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void SetForce(Vector3 force)
	{
		rb.AddForce(force, ForceMode.Impulse);
	}
	
	void FixedUpdate()
	{
		Vector3 rot = rb.velocity;
		rot.Normalize();
		transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
	}
}

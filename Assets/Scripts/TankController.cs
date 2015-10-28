using UnityEngine;
using System.Collections;

public class TankController : MonoBehaviour
{
	[System.Serializable]
	public struct TurretProperties
	{
		public float xRotationSpeed;
		public float yRotationSpeed;
	}

	public float tankMoveSpeed = 10f;
	public float yRotationSpeed = 10f;
	public TurretProperties tProps;

	Rigidbody rb;
	Transform turret, barrel;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		turret = transform.Find("Turret");
		barrel = turret.Find("Turret Barrel");
	}

	void Update()
	{
		float turretRotY = Input.GetAxis("Mouse X") * tProps.yRotationSpeed * Time.deltaTime;
		turret.Rotate(0, turretRotY, 0);
	}

	void FixedUpdate()
	{
		//Move forward and backward
		float move = Input.GetAxis("Vertical") * tankMoveSpeed;
		rb.AddForce(transform.forward * move, ForceMode.Impulse);
//		Debug.Log(rb.velocity.ToString());

		float tankRotate = Input.GetAxis("Horizontal") * yRotationSpeed;
		transform.Rotate(transform.up * tankRotate);
	}
}

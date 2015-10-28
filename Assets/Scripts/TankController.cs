using UnityEngine;
using System.Collections;

public class TankController : MonoBehaviour
{
	public float turretRotationSpeed = 10f;
	public float tankRotationSpeed = 10f;

	Rigidbody rb;
	Transform turret, barrel;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		turret = transform.Find("Turret");
		barrel = turret.transform.Find("Turret Barrel");
	}
	
	void Update()
	{
		float turretRotY = Input.GetAxis("Mouse X") * turretRotationSpeed * Time.deltaTime;
		float barrelRotX = -Input.GetAxis("Mouse Y") * turretRotationSpeed * Time.deltaTime;
		turret.Rotate(0, turretRotY, 0);
		barrel.Rotate(barrelRotX, 0, 0);
		Camera.main.transform.RotateAround(turret.position, Vector3.up, turretRotY);
	}
}

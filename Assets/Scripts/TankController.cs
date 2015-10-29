using UnityEngine;
using System.Collections;

public class TankController : MonoBehaviour
{
	[System.Serializable]
	public struct TurretProperties
	{
		public float xRotationSpeed;
		public float yRotationSpeed;
		public float xMinRotation;
		public float xMaxRotation;
	}

	[System.Serializable]
	public struct ShellProperties
	{
		public float initialMagnitude;
		public float deltaMagnitude;
		public float maxMagnitude;
	}

	public GameObject shellPrefab;
	public ShellProperties shellProperties;
	public TurretProperties turretProperties;
	public float tankMoveSpeed = 10f;
	public float yRotationSpeed = 10f;

	Rigidbody rb;
	Transform turret, barrel, shell;
	float shellMagnitude;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		turret = transform.Find("Turret");
		barrel = turret.Find("Turret Barrel");
	}

	void Update()
	{
		float turretRotY = Input.GetAxis("Mouse X") * turretProperties.yRotationSpeed * Time.deltaTime;
		turret.Rotate(0, turretRotY, 0);
		Camera.main.transform.RotateAround(turret.position, turret.up, turretRotY);

		float barrelRotX = -Input.GetAxis("Mouse Y") * turretProperties.xRotationSpeed * Time.deltaTime;
		barrel.Rotate(barrelRotX, 0, 0);
	}

	void FixedUpdate()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			shellMagnitude = shellProperties.initialMagnitude;
		}
		else if(Input.GetButton("Fire1"))
		{
			shellMagnitude += shellProperties.deltaMagnitude;
			Mathf.Clamp(shellMagnitude, shellProperties.initialMagnitude, shellProperties.maxMagnitude);
		}
		else if(Input.GetButtonUp("Fire1"))
		{
			GameObject shell = Instantiate(shellPrefab, barrel.Find("Shell Spawn").position, barrel.rotation) as GameObject;
			shell.GetComponent<ShellProjectile>().SetForce(barrel.forward * shellMagnitude);
		}

		//Move forward and backward
		float move = Input.GetAxis("Vertical") * tankMoveSpeed;
		rb.AddForce(transform.forward * move);

		//Rotate tank body and counter the rotation for the turret head
		float tankRotate = Input.GetAxis("Horizontal") * yRotationSpeed;
		transform.Rotate(0, tankRotate, 0);
		turret.Rotate(0, -tankRotate, 0);
		Camera.main.transform.RotateAround(turret.position, turret.up, -tankRotate);
	}
}

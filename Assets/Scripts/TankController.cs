using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

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
	public struct Shell
	{
		public float initialMagnitude;
		public float deltaMagnitude;
		public float maxMagnitude;
	}

	public Slider powerBar;
	public Shell shellFireSettings;
	public TurretProperties turretProperties;
	public float moveSpeed = 1000f;
	[Tooltip("Rotation speed of the tank")] public float yRotationSpeed = 1f;
	[Tooltip("Delay between shots in seconds")] public float shotDelay = 2f;

	Rigidbody rb;
	Transform turret, barrel;
	float shellMagnitude, deltaShotDelay;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		turret = transform.Find("Turret");
		barrel = turret.Find("Turret Barrel");
		Camera.main.transform.rotation = Quaternion.Euler(11, 0, 0);
	}

	void Update()
	{
		//rotate turret around y axis and move camera to follow
		float turretRotY = Input.GetAxis("Mouse X") * turretProperties.yRotationSpeed * Time.deltaTime;
		turret.Rotate(0, turretRotY, 0);
		Camera.main.transform.RotateAround(turret.position, Vector3.up, turretRotY);

		//rotate barrel up and down
		float barrelRotX = -Input.GetAxis("Mouse Y") * turretProperties.xRotationSpeed * Time.deltaTime;
		barrel.Rotate(barrelRotX, 0, 0);
		//clamp turret rotation at some point!

		//Rotate tank body and counter the rotation for the turret head
		float tankRotate = Input.GetAxis("Horizontal") * yRotationSpeed;
		transform.Rotate(0, tankRotate, 0);
		turret.Rotate(0, -tankRotate, 0);

		Camera.main.transform.position = transform.position;
		Camera.main.transform.Translate(new Vector3(0f, 2f, -5.8f));

		//time between firing shells
		deltaShotDelay += Time.deltaTime;
		if(deltaShotDelay >= shotDelay)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				shellMagnitude = shellFireSettings.initialMagnitude;
			}
			else if(Input.GetButton("Fire1"))
			{
				shellMagnitude += shellFireSettings.deltaMagnitude * Time.deltaTime;
				shellMagnitude = Mathf.Clamp(shellMagnitude, shellFireSettings.initialMagnitude, shellFireSettings.maxMagnitude);
				powerBar.value = shellMagnitude / shellFireSettings.maxMagnitude;
			}
			else if(Input.GetButtonUp("Fire1"))
			{
				barrel.SendMessage("Fire", barrel.forward * shellMagnitude + rb.velocity);
				shellMagnitude = shellFireSettings.initialMagnitude;
				deltaShotDelay = 0;
				powerBar.value = 0;
			}
		}
	}

	void FixedUpdate()
	{
		//Move forward and backward
		float move = Input.GetAxis("Vertical") * moveSpeed;
		rb.AddForce(transform.forward * move);
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class AITankController : MonoBehaviour
{
	[System.Serializable]
	public struct TurretProperties
	{
		public float xRotationSpeed;
		public float yRotationSpeed;
		public float maxFiringAngle;
	}

	[System.Serializable]
	public struct Shell
	{
		public float initialMagnitude;
		public float deltaMagnitude;
		public float maxMagnitude;
	}

	public float boostTime;
	public Shell shellFireSettings;
	public TurretProperties turretProperties;
	public float moveSpeed = 1000f;
	[Tooltip("Rotation speed of the tank")] public float yRotationSpeed = 1f;
	[Tooltip("Delay between shots in seconds")] public float shotDelay = 2f;

	Rigidbody rb;
	Transform turret, barrel, player;
	float deltaShotDelay, shellMagnitude, maxFiringDistance, deltaFireMagnitude;

	void Start() {
		rb = GetComponent<Rigidbody>();
		turret = transform.Find("Turret");
		barrel = turret.Find("Turret Barrel");

		//Calculate max firing distance
		maxFiringDistance = CalcFireRange(shellFireSettings.maxMagnitude, Mathf.PI/4);
	}

	void Update() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		Vector3 toPlayer = player.position - transform.position;

		//Rotate AITank to point turret at player Tank
		turret.LookAt(player.FindChild("Turret").position);
		turret.rotation = Quaternion.Euler(0, turret.eulerAngles.y, 0);

		//Calculate the angle and magnitude to fire the shell at to maximize time efficiency
		Vector3 firingForce = new Vector3(0, 0, 0);
		if(toPlayer.magnitude <= maxFiringDistance) {
			for(int deg = 1; deg < turretProperties.maxFiringAngle; deg++) {
				for(int mag = 1; mag < shellFireSettings.maxMagnitude; mag++) {
					float theta = deg * Mathf.Deg2Rad;
					float firingDistance = CalcFireRange(mag, theta);
					if(firingDistance > toPlayer.magnitude-3 && firingDistance < toPlayer.magnitude+3) {
						firingForce = new Vector3(0, mag*Mathf.Sin(theta), mag*Mathf.Cos(theta));
						goto exit;
					}
				}
			}
		}
		exit:

		//Determine if a shell can be fired and begin charging shell
		//deltaShotDelay += Time.deltaTime;
		if(deltaShotDelay > shotDelay) {
			if(firingForce.sqrMagnitude != 0) {
				deltaFireMagnitude += shellFireSettings.deltaMagnitude * Time.deltaTime;
				//Fire a shell
				if(deltaFireMagnitude >= firingForce.magnitude) {
					deltaFireMagnitude = shellFireSettings.initialMagnitude;
					deltaShotDelay = 0;
					barrel.SendMessage("Fire", barrel.forward * deltaFireMagnitude);
				}
			}
		}
	}

	void FixedUpdate() {

	}

	float CalcFireRange(float magnitude, float theta) {
		float ycomp = magnitude*Mathf.Sin(theta);
		float xcomp = magnitude*Mathf.Cos(theta);
		float hangtime = ycomp / (0.5f*Physics.gravity.magnitude);
		return xcomp*hangtime;
	}

	public void Die() {
		Destroy(gameObject);
	}
}

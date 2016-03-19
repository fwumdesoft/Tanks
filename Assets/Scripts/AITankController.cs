using UnityEngine;
using System.Collections;

public class AITankController : TankController
{
	public float deltaTurretRotation;

	Rigidbody rb;
	Transform turret, barrel;
	Vector3 playerPos;

	void Start() {
		rb = GetComponent<Rigidbody>();
		turret = transform.Find("Turret");
		barrel = turret.Find("Turret Barrel");
	}

	void Update() {
		playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		Vector3 toPlayer = playerPos - turret.position;

		Quaternion newRot = Quaternion.FromToRotation(turret.localPosition, toPlayer);
		turret.rotation = newRot;
	}

	void FixedUpdate() {

	}
}

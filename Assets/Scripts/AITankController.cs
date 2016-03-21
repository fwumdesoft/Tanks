using UnityEngine;
using System.Collections;

public class AITankController : MonoBehaviour
{
	public float deltaTurretRotation;

	Rigidbody rb;
	Transform turret, barrel, player;

	void Start() {
		rb = GetComponent<Rigidbody>();
		turret = transform.Find("Turret");
		barrel = turret.Find("Turret Barrel");
	}

	void Update() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		Vector3 toPlayer = player.position - turret.position;


	}

	void FixedUpdate() {

	}
}

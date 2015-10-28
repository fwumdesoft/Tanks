using UnityEngine;
using System.Collections;

public class IgnoreCollision : MonoBehaviour
{
	public Collider otherCollider;

	void Start()
	{
		Physics.IgnoreCollision(GetComponent<Collider>(), otherCollider);
	}
}

using UnityEngine;
using System.Collections;

public class TankSpawner : MonoBehaviour
{
	public GameObject tankPrefab;

	void Start()
	{
		Instantiate(tankPrefab, new Vector3(1, 0.2f, 1)*10f, Quaternion.identity);
	}
}

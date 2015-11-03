using UnityEngine;
using System.Collections;

public class TankSpawner : MonoBehaviour
{
	public GameObject aiTank;
	public float aiTanks;

	void Start()
	{
		Transform spawnPoints = GameObject.FindGameObjectWithTag("SpawnPoints").transform;
		for(int i = 0; i < aiTanks; i++)
		{
			Vector3 spawnPoint = spawnPoints.GetChild((int)Random.Range(0, spawnPoints.childCount)).position;
			Instantiate(aiTank, spawnPoint, Quaternion.identity);
		}
	}
}

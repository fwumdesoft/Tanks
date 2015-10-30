using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour
{
	void Start()
	{
		Color randColor = new Color(Random.value, Random.value, Random.value);
		MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
		foreach(MeshRenderer r in renderers)
			r.material.color = randColor;
	}
}

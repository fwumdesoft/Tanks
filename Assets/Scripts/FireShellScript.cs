using UnityEngine;
using System.Collections;

public class FireShellScript : MonoBehaviour
{
	public GameObject shellPrefab;

	public void Fire(Vector3 force)
	{
		GameObject shell = Instantiate(shellPrefab, transform.Find("Shell Spawn").position, transform.rotation) as GameObject;
		GetComponent<AudioSource>().Play(); //play fire shell sound
		shell.SendMessage("Fire", force);

		//change this so that barrel movement variables can be controlled in inspector
		//also make this work properly!!
//		transform.position = transform.position - transform.forward*0.2f;
//		StartCoroutine(TranslateNormal());
	}

	IEnumerator TranslateNormal()
	{
		Vector3 targetPos = transform.position + transform.forward * 0.2f;

		float step = 0.05f;
		for(float timePassed = 0f; timePassed < 0.2f; timePassed += step)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
			yield return new WaitForSeconds(0.1f);
		}
	}
}

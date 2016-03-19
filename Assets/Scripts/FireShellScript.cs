using UnityEngine;
using System.Collections;

public class FireShellScript : MonoBehaviour
{
	public GameObject shellPrefab;
	public float recoil;

	public void Fire(Vector3 force)
	{
		GameObject shell = Instantiate(shellPrefab, transform.Find("Shell Spawn").position, transform.rotation) as GameObject;
		GetComponent<AudioSource>().Play(); //play fire shell sound
		shell.SendMessage("Fire", force);

		transform.Translate(0, 0, -transform.forward.z*recoil);
		StartCoroutine(TranslateNormal());
	}

	IEnumerator TranslateNormal()
	{
		Vector3 targetPos = new Vector3(0, 0, transform.localPosition.z + transform.forward.z*recoil);

		float step = Time.deltaTime;
		for(float timePassed = 0f; timePassed < recoil; timePassed += step)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, step);
			yield return new WaitForEndOfFrame();
		}
	}
}

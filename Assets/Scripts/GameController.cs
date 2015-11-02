using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.LeftAlt))
			ToggleLockState();
	}

	void ToggleLockState()
	{
		if(Cursor.lockState == CursorLockMode.None)
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		else
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}
}

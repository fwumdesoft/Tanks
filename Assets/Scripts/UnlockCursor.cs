using UnityEngine;
using System.Collections;

public class UnlockCursor : MonoBehaviour
{
	void Start()
	{
		Cursor.lockState = CursorLockMode.None;
	}
}

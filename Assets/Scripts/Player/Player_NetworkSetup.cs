using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_NetworkSetup : NetworkBehaviour
{
	public Camera characterCamera;
	public AudioListener audioListener;

	void Start ()
	{
		if (isLocalPlayer) {
			
			GetComponent<PlayerMovement> ().enabled = true;

			GetComponent<PlayerFire> ().enabled = true;

			characterCamera.enabled = true;
			audioListener.enabled = true;

			GetComponent<PlayerHealth> ().healthBar.color = Color.green;
		}

		//GetComponent<Player_DressSetup> ().enabled = true;
	}
}

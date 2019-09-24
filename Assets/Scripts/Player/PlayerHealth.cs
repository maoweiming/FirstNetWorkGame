using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerHealth : NetworkBehaviour
{
	public int playerHealthMax = 100;
	[SyncVar (hook = "onHealthChange")]
	public int playerHealthCur;

	public Image healthBar;

	void Start ()
	{
		playerHealthCur = playerHealthMax;
	}

	bool isDie = false;

	public void TakeDamage (int damage)
	{
		if (!isServer)
			return;
		
		playerHealthCur -= damage;
		if (playerHealthCur <= 0) {
			playerHealthCur = 0;

			if (!isDie)
				RpcPlayerDie ();
			isDie = true;
		}
	}

	void onHealthChange (int curHealth)
	{
		float healthRate = (float)curHealth / playerHealthMax;
		healthBar.fillAmount = healthRate;
	}

	private Vector3 hitPosition;

	public void DamagePosition (Vector3 hitPosition)
	{
		this.hitPosition = hitPosition;
	}

	[ClientRpc]
	void RpcPlayerDie ()
	{
		if (!isLocalPlayer)
			return;

		GetComponent<PlayerMovement> ().enabled = false;

//		GetComponent<CharacterController> ().enabled = false;
//		gameObject.AddComponent<CapsuleCollider> ();
//		gameObject.AddComponent<Rigidbody> ();
//		GetComponent<Rigidbody> ().drag = 5;

		Rigidbody rb = GetComponent<Rigidbody>();

		rb.freezeRotation = false;
		Vector3 hitDir = (hitPosition - transform.position).normalized;
		GetComponent<Rigidbody> ().AddForce (-hitDir * 20f, ForceMode.Impulse);
	}
}

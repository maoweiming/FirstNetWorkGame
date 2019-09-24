using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BulletHit : NetworkBehaviour
{

	void Start ()
	{
		if (isServer) {
			Destroy (gameObject, 3f);
//			GetComponent<Collider> ().enabled = false;
//			StartCoroutine (EnableCollider ());
			gameObject.layer = 12;
			StartCoroutine (ChangeLayer ());
		}
	}

	IEnumerator EnableCollider ()
	{
		yield return new WaitForSeconds (0.1f);
		GetComponent<Collider> ().enabled = true;
	}

	IEnumerator ChangeLayer ()
	{
		yield return new WaitForSeconds (0.01f);
		gameObject.layer = 11;
	}


	public GameObject basicHitPrefab;
	public GameObject bloodPrefab;

	private GameObject hitEffect;

	private Collision hitCollision;
	[SyncVar] private string hitTag;
	[SyncVar] private Vector3 hitPoint;
	[SyncVar] private Vector3 hitPointNormal;

	[ServerCallback]//[ServerCallback]
	void OnCollisionEnter (Collision collision)
	{
		hitCollision = collision;
		hitTag = collision.collider.tag;
		hitPoint = hitCollision.contacts [0].point;
		hitPointNormal = hitCollision.contacts [0].normal;

		PlayerHealth damageReceiver = collision.collider.GetComponent<PlayerHealth> ();
		if (damageReceiver != null) {
			damageReceiver.TakeDamage (10);
			damageReceiver.DamagePosition (collision.contacts [0].point);
		}

		//Destroy (hitEffect, 4f);
		NetworkServer.Destroy (gameObject);
		
	}

	public override void OnNetworkDestroy ()
	{
		playEffect ();
		Destroy (hitEffect.gameObject, 4f);
		base.OnNetworkDestroy ();
	}


	private Color hitTargetColor;

	void playEffect ()
	{
		if (hitTag == "Player")
			hitEffect = GameObject.Instantiate (bloodPrefab, transform.position, Quaternion.identity) as GameObject;
		else {
			hitEffect = GameObject.Instantiate (basicHitPrefab, transform.position, Quaternion.identity) as GameObject;

			hitTargetColor = hitCollision.collider.GetComponent<Renderer> ().material.GetColor ("_Color");
			hitEffect.GetComponentInChildren<ParticleSystem> ().startColor = hitTargetColor;
		}

		hitEffect.transform.LookAt (hitPoint + hitPointNormal);
	}

}

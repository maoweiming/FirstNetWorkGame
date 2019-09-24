using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerFire : NetworkBehaviour
{

	[ClientCallback]
	void Update ()
	{
		if (!isLocalPlayer)
			return;
		
		if (Input.GetMouseButtonDown (0)) {
			CmdDoFire (firePos.position, firePos.rotation);
//			CmdDoFire (serverSideFirePosition, serverSideFireRotation);
		}
	}

	//	void FixedUpdate ()
	//	{
	//		CmdSendToServer (firePos.position, firePos.rotation);
	//	}

	public Rigidbody bulletPrefab;
	public Transform firePos;

	//	[SyncVar]private Vector3 serverSideFirePosition;
	//	[SyncVar]private Quaternion serverSideFireRotation;
	//
	//	[Command]
	//	void CmdSendToServer (Vector3 position, Quaternion rotation)
	//	{
	//		serverSideFirePosition = position;
	//		serverSideFireRotation = rotation;
	//	}

	[Command]
	void CmdDoFire (Vector3 pos, Quaternion rot)
	{
		Rigidbody tempBullet = Instantiate (bulletPrefab, pos, rot) as Rigidbody;
		tempBullet.velocity = tempBullet.gameObject.transform.forward * 1500 * Time.deltaTime;

		NetworkServer.Spawn (tempBullet.gameObject);
	}

}

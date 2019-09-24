using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSyncTransform : NetworkBehaviour
{
	public float smoothSpeed = 6;

	[SyncVar]
	Vector3 serverSidePosition;
	[SyncVar]
	Quaternion serverSideRotation;

	void FixedUpdate ()
	{
		if (!isLocalPlayer)
			smoothTransform ();
		else
			CmdSendToServer (transform.position, transform.rotation);
	}

	void Update ()
	{

	}

	[Command]
	void CmdSendToServer (Vector3 position, Quaternion rotation)
	{
		serverSidePosition = position;
		serverSideRotation = rotation;
	}

	void smoothTransform ()
	{
		transform.position = Vector3.Lerp (transform.position, serverSidePosition, smoothSpeed * Time.fixedDeltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, serverSideRotation, smoothSpeed * Time.fixedDeltaTime);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMove : MonoBehaviour
{

	public Transform firstPos;
	public Transform playerPos;

	public float speed = 5f;

	Vector3 targetPosition;
	Quaternion targetRotation;

	void Start ()
	{
		targetPosition = firstPos.position;
		targetRotation = firstPos.rotation;
	}

	void Update ()
	{
		transform.position = Vector3.Lerp (transform.position, targetPosition, speed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * 0.5f * Time.deltaTime);
	}

	public void OnMoveToPlayer ()
	{
		targetPosition = playerPos.position;
		targetRotation = playerPos.rotation;
	}

	public void OnMoveToUp ()
	{
		targetPosition = firstPos.position;
		targetRotation = firstPos.rotation;	
	}

}

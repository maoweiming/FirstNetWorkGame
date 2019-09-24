using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{

	public Transform target;
	public float distanceOffset;
	public float dampTime;
	private Vector3 velocity = Vector3.zero;

	private Vector3 targetSmooth;

	private Quaternion preRotation;

	void Start ()
	{
		distanceOffset = Vector3.Distance (transform.position, target.position);
		targetSmooth = target.position;
		preRotation = transform.rotation;
	}

	void FixedUpdate ()
	{
		targetSmooth = Vector3.SmoothDamp (targetSmooth, target.position + transform.forward * -distanceOffset, ref velocity, dampTime * Time.deltaTime);
	}

	void LateUpdate ()
	{
		transform.position = targetSmooth;
		transform.rotation = preRotation;
	}
}

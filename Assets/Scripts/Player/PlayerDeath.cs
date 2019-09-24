using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
	public Transform t1Target;
	public Transform t2Target;
	public Vector3 t2;

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
//			canDo = true;
			StartCoroutine ("doAnim");
		}

//		if (canDo) {
//			if (Vector3.Distance (transform.position, t2Target.position) > 1f) {
//
//
//				print ("MOVE");
//
//				Vector3 center = (t1Target.position + t2Target.position) * 0.5f;
//
//				center -= new Vector3 (0, 1, 0);
//
//				Vector3 start = transform.position - center;
//				Vector3 end = t2Target.position - center;
//
//				transform.position = Vector3.Slerp (start, end, 4f * Time.deltaTime);
//				transform.position += center;
//
//				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (center - transform.position), 1f * Time.deltaTime);
//			} else
//				canDo = false;
//		}

	}

	void Start ()
	{
		transform.position = t1Target.position;
//		StartCoroutine ("doAnim");
	}

	IEnumerator doAnim ()
	{
		//		print ("START");
		transform.position = t1Target.position;
		while (Vector3.Distance (transform.position, t2Target.position) > 2f) {
	
	
			print ("MOVE");
	
			Vector3 center = (t1Target.position + t2Target.position) * 0.5f;
	
			center -= new Vector3 (0, 1, 0);
	
			Vector3 start = transform.position - center;
			Vector3 end = t2Target.position - center;

			transform.position = Vector3.Slerp (start, end, 1f * Time.deltaTime);
			transform.position += center;
	
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (center - transform.position), 1f * Time.deltaTime);
	
			yield return 0;
		}
	}

}


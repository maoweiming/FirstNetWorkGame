using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMovement : NetworkBehaviour
{
	//		CharacterController cc;
	Rigidbody rb;

	void Start ()
	{
//		cc = GetComponent<CharacterController> ();
		rb = GetComponent<Rigidbody> ();
	}

	private Vector3 moveDir;
	public float moveSpeed = 10;

	void Update ()
	{
		moveDir = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));

//		cc.Move (moveDir * moveSpeed * Time.deltaTime);

//		doRotate ();
	}

	public Camera characterCamera;
	public float rotateSpeed;

	void FixedUpdate ()
	{
		
//		rb.AddForce (moveDir * moveSpeed);

//		rb.velocity = (moveDir * moveSpeed / 100);
//		doRigidbodyMove ();

		doRigidbodyMove ();

		doRigidbodyRotate ();
	}

	void doRigidbodyMove ()
	{
//		float moveX = Input.GetAxis ("Horizontal");
//		Vector3 H = moveX * Vector3.right * moveSpeed * Time.deltaTime;
//		rb.MovePosition (rb.position + H);
//
//		float moveZ = Input.GetAxis ("Vertical");
//		Vector3 V = moveZ * Vector3.forward * moveSpeed * Time.deltaTime;
//		rb.MovePosition (rb.position + V);


		//transform.position = targetPos;

		rb.MovePosition (rb.position + moveDir * moveSpeed * Time.deltaTime);
	}

	void doRigidbodyRotate ()
	{
		Ray mouseRay = characterCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit mouseRayHit;
		Physics.Raycast (mouseRay, out mouseRayHit, 1 << LayerMask.NameToLayer ("Floor"));
		Vector3 lookPoint = new Vector3 (mouseRayHit.point.x, transform.position.y, mouseRayHit.point.z);
		Quaternion lookRotation = Quaternion.LookRotation (lookPoint - transform.position);
		Quaternion moveRotation = Quaternion.Slerp (rb.rotation, lookRotation, rotateSpeed * Time.deltaTime);
		rb.MoveRotation (moveRotation);
	}

	void doRotate ()
	{
		Ray mouseRay = characterCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit mouseRayHit;
		Physics.Raycast (mouseRay, out mouseRayHit, 1 << LayerMask.NameToLayer ("Floor"));
		Vector3 lookPoint = new Vector3 (mouseRayHit.point.x, transform.position.y, mouseRayHit.point.z);
//		transform.LookAt (lookPoint);
		Quaternion lookRotation = Quaternion.LookRotation (lookPoint - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
	}

}

using UnityEngine;
using System.Collections;

public class WindyMover : MonoBehaviour {

	private Rigidbody rb;
	public float dx, dy;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horiz = Input.GetAxis ("Horizontal");
		float vert = Input.GetAxis ("Vertical");
		rb.AddForce (new Vector3 (dx * horiz, 0, dy * vert));
	}
}

using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

	public Rigidbody rb;

	void Start () {

	}
	// Update is called once per frame
	void Update () {
		if (rb.transform.position.y < -100) {
			rb.angularVelocity = new Vector3(0f, 0f, 0f);
			rb.velocity = new Vector3(0f, 0f, 0f);
			rb.transform.position = new Vector3(0f, 1f, -2f);
			transform.rotation = Quaternion.Euler (0f, 0f, 0f);
		}
	}
}

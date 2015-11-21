using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

	public Rigidbody rb;
	Lives lives;

	void Start () {
		lives = (Lives)GameObject.FindGameObjectWithTag ("Lives").GetComponent<Lives>();
	}

	// Update is called once per frame
	void Update () {
		if (rb.transform.position.y < -100) {
			lives.Die ();
			rb.GetComponent<Collider>().isTrigger = false;
			rb.angularVelocity = new Vector3(0f, 0f, 0f);
			rb.velocity = new Vector3(0f, 0f, 0f);
			rb.transform.position = new Vector3(0f, 1f, -2f);
			transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			GameObject [] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
			for(int i = 0; i < ghosts.Length; i++) {
				ghosts[i].GetComponent <GhostController>().Reset();
			}
		}
	}
}

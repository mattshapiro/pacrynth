using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

	public Rigidbody rb;
	public GameObject ghostPrefab;
	Lives lives;

	void Start () {
		lives = (Lives)GameObject.FindGameObjectWithTag ("Lives").GetComponent<Lives>();
		Vector3 zero = new Vector3(0,0,0);
		GameObject child = (GameObject)Instantiate (ghostPrefab, zero, Quaternion.Euler (zero));
		child.transform.parent = this.transform;
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

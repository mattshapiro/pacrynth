using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

	public Rigidbody rb;
	public GameObject ghostPrefab;
	public float spawn_timer = 5.0f;
	public int maxGhosts = 5;

	Lives lives;
	private int numGhosts = 0;
	private float spawn_count = 0f;

	void MakeGhost () {
		GameObject child = (GameObject)Instantiate (ghostPrefab, this.transform.position, this.transform.rotation);
		child.transform.parent = this.transform;
		numGhosts++;
	}

	void Start () {
		lives = (Lives)GameObject.FindGameObjectWithTag ("Lives").GetComponent<Lives>();
		MakeGhost ();
	}

	// Update is called once per frame
	void Update () {
		// falling
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
		// ghost spawn timer
		if (numGhosts < maxGhosts) {
			if(spawn_count >= spawn_timer)
			{
				MakeGhost ();
				spawn_count = 0f;
			} else {
				spawn_count += Time.deltaTime;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

	public Rigidbody rb;
	public GameObject ghostPrefab;
	public float spawn_timer = 2.0f;
	public float ghost_bounce_radius = 1.0f;
	public int maxGhosts = 5;

	Lives lives;
	private int numGhosts = 0;
	private float spawn_count = 0f;
	private ArrayList ghosts;

	void MakeGhost () {
		GameObject child = (GameObject)Instantiate (ghostPrefab, this.transform.position, this.transform.rotation);
		child.transform.parent = this.transform;
		GhostController ghost = (GhostController)child.GetComponent<GhostController> ();
		ghosts.Add (ghost);
		numGhosts++;
	}

	void Restart () {
		numGhosts = 0;
		spawn_count = 0f;
		ghosts = new ArrayList ();
		MakeGhost ();
	}

	void Start () {
		lives = (Lives)GameObject.FindGameObjectWithTag ("Lives").GetComponent<Lives>();
		Restart ();
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
				Destroy(ghosts[i]);
				ghosts[i] = null;
			}
			Restart ();
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
		// ghost collisions
		for (int i = 0; i < numGhosts - 1; i++) {
			for(int j = i + 1; j < numGhosts; j++) {
				GhostController ghost1 = (GhostController)ghosts[i];
				GhostController ghost2 = (GhostController)ghosts[j];
				Vector3 distance = ghost1.transform.position - ghost2.transform.position;
				if(distance.magnitude < ghost_bounce_radius) {
					ghost1.Bounce (ghost2);
					ghost2.Bounce (ghost1);
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GhostCollider : MonoBehaviour {

	public Rigidbody player;
	public Rigidbody board;
	public Text output;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			output.text = "trigger";
			player.angularVelocity = new Vector3 (0f, 0f, 0f);
			player.velocity = new Vector3 (0f, 0f, 0f);
			board.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			player.velocity = new Vector3 (0f, -1f, 0f);
		}
	}
}

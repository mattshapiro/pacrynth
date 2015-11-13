using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GhostController : MonoBehaviour {

	public float speed = 3.0f;
	public Rigidbody player;
	public Rigidbody board;

	private PathNode currentNode, lastNode;
	private bool stopped;

	public void Reset() {
		stopped = false;
		currentNode = (PathNode)GameObject.FindGameObjectsWithTag ("PrimeNode")[0].GetComponent<PathNode>();
		lastNode = (PathNode)GameObject.FindGameObjectsWithTag ("Respawn") [0].GetComponent<PathNode> ();
		transform.position = lastNode.GetPosition ();
	}

	public void Stop() {
		stopped = true;
	}

	// Use this for initialization
	void Start () {
		Reset ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!stopped) {
			Vector3 delta = currentNode.GetPosition () - transform.position;
			if (delta.magnitude < currentNode.GetRadius ()) {
				int nextNode = 0; 
				do {
					nextNode = Random.Range (0, currentNode.children.Length);
				} while(currentNode.children[nextNode].Equals(lastNode));
				lastNode = currentNode;
				currentNode = currentNode.children [nextNode];
			} else {
				//transform.position += delta * Time.smoothDeltaTime * speed;
				transform.position = Vector3.MoveTowards (transform.position, currentNode.GetPosition (), Time.deltaTime * speed);
			}
		} else {
			Vector3 dest = player.transform.position;
			dest.y = transform.position.y;
			transform.position = Vector3.MoveTowards (transform.position, dest, Time.deltaTime * speed);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			Stop ();
			player.angularVelocity = new Vector3 (0f, 0f, 0f);
			player.velocity = new Vector3 (0f, 0f, 0f);
			board.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			other.isTrigger = true;
		}
	}
}

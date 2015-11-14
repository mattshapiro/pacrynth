using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GhostController : MonoBehaviour {

	public float speed = 3.0f;
	public float fright_time = 10;
	public Rigidbody player;
	public Rigidbody board;
	public Material fright_material, dead_material;

	private PathNode currentNode, lastNode;
	private bool stopped, fright_mode, dead_mode;
	private float fright_count;
	private Material live_material;

	private Renderer renderer;

	public void Reset() {
		stopped = false;
		fright_mode = false;
		currentNode = (PathNode)GameObject.FindGameObjectsWithTag ("PrimeNode")[0].GetComponent<PathNode>();
		lastNode = (PathNode)GameObject.FindGameObjectsWithTag ("Respawn") [0].GetComponent<PathNode> ();
		transform.position = lastNode.GetPosition ();
	}

	public void Stop() {
		stopped = true;
	}

	public void BeginFrightMode() {
		fright_count = 0;
		fright_mode = true;
		dead_mode = false;
		renderer.material = fright_material;
	}

	// Use this for initialization
	void Start () {
		renderer = GetComponent <Renderer> ();
		live_material = renderer.material;
		Reset ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!stopped) {
			if(fright_mode) {
				fright_count += Time.deltaTime;
				if(fright_count >= fright_time) {
					fright_mode = false;
					renderer.material = live_material;
				}
			}
			Vector3 delta = currentNode.GetPosition () - transform.position;
			if (delta.magnitude < currentNode.GetRadius ()) {
				if(dead_mode) {
					dead_mode = false;
					renderer.material = live_material;
				}
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
			if(fright_mode)
			{
				// collision with "blue ghost"
				dead_mode = true;
				fright_mode = false;
				renderer.material = dead_material;
				currentNode = (PathNode)GameObject.FindGameObjectsWithTag ("Respawn") [0].GetComponent<PathNode> ();
			} else {
				// falls through hole
				Stop ();
				player.angularVelocity = new Vector3 (0f, 0f, 0f);
				player.velocity = new Vector3 (0f, 0f, 0f);
				board.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
				other.isTrigger = true;
			}
		}
	}
}

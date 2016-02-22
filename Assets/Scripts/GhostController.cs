using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GhostController : MonoBehaviour {

	public float speed = 3.0f;
	public float fright_time = 10;
	public Material fright_material, dead_material;

	private PathNode currentNode, lastNode, primeNode;
	private bool stopped, fright_mode, dead_mode;
	private float fright_count;
	private Material live_material;

	private Rigidbody board;
	private Rigidbody player;
	private Renderer rend;

	private Vector3 previousPos;
	private CameraFollower cameraFollower;

	public void Reset() {
		stopped = false;
		fright_mode = false;
		currentNode = primeNode;
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Respawn");
		if (objects != null && objects.Length > 0) {
			lastNode = (PathNode)objects [0].GetComponent<PathNode> ();
			transform.position = lastNode.GetPosition ();
		}
		transform.position = lastNode.GetPosition ();
		previousPos = transform.position;
	}

	public Vector3 GetPreviousPosition() {
		return previousPos;
	}

	public void Stop() {
		player = (Rigidbody)GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody> ();
		board = (Rigidbody)GameObject.FindGameObjectWithTag ("Board").GetComponent<Rigidbody> ();
		stopped = true;
	}

	public void BeginFrightMode() {
		if (!fright_mode) {
			transform.localScale += new Vector3 (0, 0.1f, 0);
		}
		fright_count = 0;
		fright_mode = true;
		dead_mode = false;
		rend.material = fright_material;
	}

	public void Bounce (GhostController other) {
		float thisFrame = (other.transform.position - transform.position).magnitude ;
		float prevFrame = (other.GetPreviousPosition () - GetPreviousPosition () ).magnitude ;
		if ((thisFrame < prevFrame) && (currentNode != primeNode) && (lastNode != primeNode)) {
			PathNode temp = currentNode;
			currentNode = lastNode;
			lastNode = temp;
		}
	}

	// Use this for initialization
	void Start () {
		rend = GetComponent <Renderer> ();
		live_material = GetComponent<Renderer>().material;
		GameObject [] objects = GameObject.FindGameObjectsWithTag ("PrimeNode");
		primeNode =  (objects != null && objects.Length > 0) ? (PathNode)objects[0].GetComponent<PathNode> () : null;
		cameraFollower = (CameraFollower)GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollower> ();
		Reset ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		previousPos = transform.position;

		if (!stopped) {
			// Fright Mode
			if(fright_mode) {
				fright_count += Time.deltaTime;
				if(fright_count >= fright_time) {
					fright_mode = false;
					rend.material = live_material;
					transform.localScale -= new Vector3 (0, 0.1f, 0);
				} else if (fright_count >= fright_time - 3) {
					// blink
				}
			}

			// Pathing
			Vector3 delta = currentNode.GetPosition () - transform.position;
			if (delta.magnitude < currentNode.GetRadius ()) {
				if(dead_mode) {
					dead_mode = false;
					rend.material = live_material;
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
			if (fright_mode) {
				// collision with "blue ghost"
				dead_mode = true;
				fright_mode = false;
				transform.localScale -= new Vector3 (0, 0.1f, 0);
				currentNode = (PathNode)GameObject.FindGameObjectsWithTag ("Respawn") [0].GetComponent<PathNode> ();
			} else {
				// falls through hole
				Stop ();
				player.angularVelocity = new Vector3 (0f, 0f, 0f);
				player.velocity = new Vector3 (0f, 0f, 0f);
				board.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
				other.isTrigger = true;
				cameraFollower.setActive (false);
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour {

	public float speed = 3.0f;
	private PathNode currentNode, lastNode;

	// Use this for initialization
	void Start () {
		currentNode = (PathNode)GameObject.FindGameObjectsWithTag ("PrimeNode")[0].GetComponent<PathNode>();
		lastNode = (PathNode)GameObject.FindGameObjectsWithTag ("Respawn") [0].GetComponent<PathNode> ();
		transform.position = lastNode.GetPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 delta = currentNode.GetPosition () - transform.position;
		if (delta.magnitude < currentNode.GetRadius ()) {
			int nextNode = 0; 
			do {
				nextNode = Random.Range (0, currentNode.children.Length);
			}while(currentNode.children[nextNode].Equals(lastNode));
			lastNode = currentNode;
			currentNode = currentNode.children[nextNode];
		} else {
			//transform.position += delta * Time.smoothDeltaTime * speed;
			transform.position = Vector3.MoveTowards (transform.position, currentNode.GetPosition(), Time.deltaTime * speed);
		}
	}
}

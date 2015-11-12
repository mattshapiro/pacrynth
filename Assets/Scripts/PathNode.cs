using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {
	
	public PathNode[] children;
	public static float RADIUS = 0.1f;
	private Vector3 rgb;
	// Use this for initialization
	void Start () {
		rgb = new Vector3 (
			Random.Range (0, 256),
			Random.Range (0, 256),
			Random.Range (0, 256)
			);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetRadius() {
		return RADIUS;
	}

	public Vector3 GetPosition() {
		return transform.position;
	}

	void OnDrawGizmos() {
		//Gizmos.color = Color.white;
		Gizmos.DrawWireSphere (transform.position, RADIUS);
		Gizmos.color = new Color(rgb.x,rgb.y,rgb.z,0);
		Gizmos.color = Color.green;
		for (int i = 0; i < children.Length; i++) {
			Gizmos.color = Color.white;
			Gizmos.DrawLine (transform.position, children[i].transform.position);
		}
	}
}

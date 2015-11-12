using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {
	
	public PathNode[] children;
	public static float RADIUS = 0.1f;

	// Use this for initialization
	void Start () {
	
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
		byte r = (byte)Random.Range (0, 256);
		byte g = (byte)Random.Range (0, 256);
		byte b = (byte)Random.Range (0, 256);
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere (transform.position, RADIUS);
		Gizmos.color = new Color32(r,g,b,0);
		for (int i = 0; i < children.Length; i++) {
			Gizmos.color = Color.white;
			Gizmos.DrawLine (transform.position, children[i].transform.position);
		}
	}
}

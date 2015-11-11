using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {

	public GameObject obj;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - obj.transform.position;
	}

	void LateUpdate() {
		transform.localPosition = obj.transform.localPosition + offset;
	}
}

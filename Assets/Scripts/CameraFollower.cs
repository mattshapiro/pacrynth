using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {

	public GameObject obj;
	public float speed = 10.0f;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.localPosition - obj.transform.localPosition;
		offset.y = 10;
	}

	void LateUpdate() {
		if (transform.localPosition.y < 10) {
			transform.localPosition = obj.transform.localPosition + offset;
		} else {
			transform.localPosition = Vector3.Slerp(transform.localPosition, 
			                                              obj.transform.localPosition + offset,
			                                              Time.deltaTime * speed);
		}
	}
}

using UnityEngine;
using System.Collections;

public class menumover : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float former = transform.rotation.eulerAngles.y;
		float newgle = (former + (5.0f * Time.deltaTime)) % 360;
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x,
			newgle,//transform.rotation.eulerAngles.y,
			transform.rotation.eulerAngles.z);
	}
}

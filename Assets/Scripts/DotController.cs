using UnityEngine;
using System.Collections;

public class DotController : MonoBehaviour {

	bool triggered = false;
	int self_destruct_timer = 3;
	float self_destruct_count = 0;
	float close_enough = 0.1f;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			triggered = true;
		}
	}

	void FixedUpdate () {
		if (triggered) {
			self_destruct_count += Time.deltaTime;
			Vector3 vector = transform.TransformPoint(Vector3.up * 10);
			if(self_destruct_count < self_destruct_timer && (transform.position - vector).magnitude > close_enough) {
				transform.position = Vector3.MoveTowards(transform.position, vector, 10 * Time.deltaTime);
			} else {
				Destroy (gameObject);
			}
		}
	}
}

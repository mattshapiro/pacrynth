using UnityEngine;
using System.Collections;

public class DotController : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			Destroy (gameObject);
		}
	}
}

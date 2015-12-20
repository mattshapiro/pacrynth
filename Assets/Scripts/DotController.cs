using UnityEngine;
using System.Collections;

public class DotController : MonoBehaviour {

	Score score;
	GameObject cam;
	GameController gc;

	bool triggered = false;
	int self_destruct_timer = 3;
	float self_destruct_count = 0;
	float close_enough = 0.1f;

	void Start () { 
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		score = (Score)GameObject.FindGameObjectWithTag ("Score").GetComponent<Score>();
		gc = (GameController)GameObject.FindGameObjectWithTag ("Board").GetComponent<GameController> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			triggered = true;
		}
	}

	void FixedUpdate () {
		if (triggered) {
			if(self_destruct_count == 0) {
				score.AddScore(10);
			}
			self_destruct_count += Time.deltaTime;
			//Vector3 vector = transform.TransformPoint(Vector3.up * 10);
			if(self_destruct_count < self_destruct_timer && (cam.transform.position - transform.position).magnitude > close_enough) {
				transform.position = Vector3.MoveTowards(transform.position, cam.transform.position, 10 * Time.deltaTime);
			} else {
				gc.DotEaten();
				Destroy (gameObject);
			}
		}
	}
}

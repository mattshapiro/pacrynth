using UnityEngine;
using System.Collections;

public class PelletController : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			GameObject [] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
			for(int i = 0; i < ghosts.Length; i++ )
			{
				GhostController ghost = (GhostController)ghosts[i].GetComponent<GhostController>();
				ghost.BeginFrightMode ();
			}
			Destroy (gameObject);
		}
	}

}

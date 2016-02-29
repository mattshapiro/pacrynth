using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButtonBehaviourScript : MonoBehaviour {

	public void onClick() {
		SceneManager.LoadScene("Level1");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

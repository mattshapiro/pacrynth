using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButtonBehaviourScript : MonoBehaviour {
	public void onClick() {
		SceneManager.LoadScene("Level1");
	}
}

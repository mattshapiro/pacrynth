using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject mainCamera;
	public Text txtHighScore;

	private static string HS_KEY = "HS"; // high scores player prefs

	void Start () {
		Debug.Log ("start");
		if (txtHighScore != null) {
			if (PlayerPrefs.HasKey (HS_KEY)) {
				txtHighScore.text = "High Score: " + PlayerPrefs.GetInt (HS_KEY) + "pts";
			} else {
				txtHighScore.text = "";
			}
		}
	}

	public void onStart() {
		SceneManager.LoadScene("Level3", LoadSceneMode.Single);
	}

	// ug this could have gone better
	public void onContinue() {
		SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
	}
	
	// Update is called once per frame
	void Update () {
		float former = mainCamera.transform.rotation.eulerAngles.y;
		float newgle = (former + (5.0f * Time.deltaTime)) % 360;
		mainCamera.transform.rotation = Quaternion.Euler (mainCamera.transform.rotation.eulerAngles.x,
			newgle,//transform.rotation.eulerAngles.y,
			mainCamera.transform.rotation.eulerAngles.z);
	}
}

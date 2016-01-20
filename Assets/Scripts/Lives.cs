using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lives : Text {

	int lives;
	string LIVES_KEY = "lives";

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		string prefix = "";
		if (PlayerPrefs.HasKey (LIVES_KEY)) {
			// we are at a new level - set lives
			prefix = "has key ";
			lives = PlayerPrefs.GetInt (LIVES_KEY);
		} else {
			prefix = "no key ";
			lives = 3;
		}
		text = prefix + lives;
	}
	
	public void Die () {
		lives--;
		if (lives > 0) {
			PlayerPrefs.SetInt (LIVES_KEY, lives);
			text = "" + lives;
		} else {
			PlayerPrefs.DeleteAll ();
			text = "Tap the screen to restart.";
		}
	}

	public int GetScore() {
		return lives;
	}
}

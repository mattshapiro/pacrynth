using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : Text {

	int score, dot_max, dot_count;
	string SCORE_KEY = "score";

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if (PlayerPrefs.HasKey (SCORE_KEY)) {
			score = PlayerPrefs.GetInt (SCORE_KEY);
		} else {
			score = 0;
		}
		Display ();
	}
	
	public void AddScore(int add) {
		score += add;
		PlayerPrefs.SetInt (SCORE_KEY, score);
		Display ();
	}

	public void SetScore(int score) {
		this.score = score;
		PlayerPrefs.SetInt (SCORE_KEY, score);
		Display ();
	}

	public int GetScore() {
		return score;
	}

	void Display () {
		text = "" + score;
	}
}

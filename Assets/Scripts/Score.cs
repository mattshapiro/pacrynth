using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : Text {

	int dot_max, dot_count;
	static int score = 0;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		Display ();
	}
	
	public void AddScore(int add) {
		score += add;
		Display ();
	}

	public void SetScore(int s) {
		score = s;
		Display ();
	}

	public int GetScore() {
		return score;
	}

	void Display () {
		text = "" + score;
	}
}

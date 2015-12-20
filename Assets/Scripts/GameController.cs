using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public Rigidbody player;
	public GameObject ghostPrefab;
	public float spawn_timer = 2.0f;
	public float ghost_bounce_radius = 1.0f;
	public int maxGhosts = 5;

	Lives lives;
	private int numGhosts = 0;
	private float spawn_count = 0f;
	private ArrayList ghosts;
	private bool isDead = false;

	public float speed;
	
	private Rigidbody rb;
	private bool isMobile;
	private static float MAX_ANGLE = 60.0f;
	private int dot_count;

	private string LEVEL_KEY = "Level";
	private int MAX_LEVEL = 3;

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey (LEVEL_KEY)) {
			PlayerPrefs.SetInt (LEVEL_KEY, 1);
		}
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		rb = GetComponent<Rigidbody> ();
		isMobile = Application.isMobilePlatform;
		lives = (Lives)GameObject.FindGameObjectWithTag ("Lives").GetComponent<Lives>();
		Restart ();
		dot_count = GameObject.FindGameObjectsWithTag ("Dot").Length;
	}

	/******* TILT CONTROLLER ********/
	// Update is called once per frame
	void FixedUpdate () {
		if (!isDead) {
			if (isMobile) {
				UpdateMobile ();
			} else {
				UpdateDesktop ();
			}
		}
	}
	
	float limit(float value, float limiter)
	{
		if (value > limiter)
			value = limiter;
		else if (value < -limiter)
			value = -limiter;
		return value;
	}
	
	void UpdateMobile(){
		float x = -Input.acceleration.x * 180;
		float y = Input.acceleration.y * 180;
		x = limit (x, MAX_ANGLE);
		y = limit (y, MAX_ANGLE);
		Quaternion quato = Quaternion.Euler(y, 0, x);
		rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, quato, Time.deltaTime * 5.0f);
	}
	
	void UpdateDesktop(){
		float horiz = Input.GetAxis ("Horizontal");
		float vert = Input.GetAxis ("Vertical");
		Vector3 eulerAngleVelocity = new Vector3(vert * speed, 0f, horiz * speed * -5);
		eulerAngleVelocity.y = 0;
		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
		rb.MoveRotation(rb.rotation * deltaRotation);
	}

	/******* GAME CONTROLLER ********/

	void MakeGhost () {
		GameObject child = (GameObject)Instantiate (ghostPrefab, this.transform.position, this.transform.rotation);
		child.transform.parent = this.transform;
		GhostController ghost = (GhostController)child.GetComponent<GhostController> ();
		ghosts.Add (ghost);
		numGhosts++;
	}

	void Restart () {
		numGhosts = 0;
		spawn_count = 0f;
		ghosts = new ArrayList ();
		MakeGhost ();
		isDead = false;
	}

	void Dead () {
		isDead = true;
	}

	// Update is called once per frame
	void Update () {
		// falling
		if (player.transform.position.y < -100) {
			lives.Die ();
			player.GetComponent<Collider>().isTrigger = false;
			player.angularVelocity = new Vector3(0f, 0f, 0f);
			player.velocity = new Vector3(0f, 0f, 0f);
			player.transform.position = new Vector3(0f, 1f, -2f);
			transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			GameObject [] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
			for(int i = 0; i < ghosts.Length; i++) {
				Destroy(ghosts[i]);
				ghosts[i] = null;
			}
			if(lives.GetScore () <= 0) {
				Dead ();
			} else {
				Restart ();
			}
		}

		if (isDead && Input.touchCount > 0) {
			Application.LoadLevel (Application.loadedLevel);
		}

		// ghost spawn timer
		if (numGhosts < maxGhosts) {
			if(spawn_count >= spawn_timer)
			{
				MakeGhost ();
				spawn_count = 0f;
			} else {
				spawn_count += Time.deltaTime;
			}
		}
		// ghost collisions
		for (int i = 0; i < numGhosts - 1; i++) {
			for(int j = i + 1; j < numGhosts; j++) {
				GhostController ghost1 = (GhostController)ghosts[i];
				GhostController ghost2 = (GhostController)ghosts[j];
				Vector3 distance = ghost1.transform.position - ghost2.transform.position;
				if(distance.magnitude < ghost_bounce_radius) {
					ghost1.Bounce (ghost2);
					ghost2.Bounce (ghost1);
				}
			}
		}
	}

	public void DotEaten() {
		dot_count--;
		if (dot_count == 0) 
		{
			// next scene
			if(PlayerPrefs.HasKey(LEVEL_KEY)) {
				int level = PlayerPrefs.GetInt (LEVEL_KEY) + 1;
				if(level <= MAX_LEVEL) { 
					Application.LoadLevel("Level" + level);
				}
			}
		}
	}
}

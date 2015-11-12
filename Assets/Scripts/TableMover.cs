using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TableMover : MonoBehaviour {

	public float speed;
	public Text output;

	private Rigidbody rb;
	private bool isMobile;
	private static float MAX_ANGLE = 60.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		isMobile = Application.isMobilePlatform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isMobile) {
			UpdateMobile ();
		} else {
			UpdateDesktop ();
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
		Vector3 eulerAngleVelocity = new Vector3(vert * speed, 0f, horiz * speed * -1);
		eulerAngleVelocity.y = 0;
		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
		rb.MoveRotation(rb.rotation * deltaRotation);
	}
}

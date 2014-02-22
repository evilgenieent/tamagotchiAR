using UnityEngine;
using System.Collections;

public class BallUpdater : MonoBehaviour {

	public GameObject ball;
	public PetLogic cat;

	private float lastBall;

	// Use this for initialization
	void Start () {
		lastBall = Time.time;
	}

	void FixedUpdate() {
		ball.transform.position =
			new Vector3(
				0.0f,
				transform.position.y,
				0.0f
				);

		if (ball.transform.position.magnitude <= 10 && Time.time - lastBall > 10) {
			// TODO: prevent spam
			lastBall = Time.time;
			StartCoroutine(cat.OnPetting());
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class BallUpdater : MonoBehaviour {

	public GameObject ball;
	public PetLogic cat;

	PetLogic logic;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() {
		ball.transform.position =
			new Vector3(
				0.0f,
				transform.position.y,
				0.0f
				);

		if (ball.transform.position.magnitude <= 10) {
			// TODO: prevent spam
			StartCoroutine(cat.OnPetting());
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

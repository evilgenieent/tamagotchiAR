using UnityEngine;
using System.Collections;

public class BallUpdater : MonoBehaviour {

	public GameObject ball;

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
	}

	// Update is called once per frame
	void Update () {
	
	}
}

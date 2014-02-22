using UnityEngine;
using System.Collections;

public class PotUpdater : MonoBehaviour {

	public GameObject pot;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() {
		pot.transform.position =
			new Vector3(
				transform.position.x,
				0.0f,
				transform.position.z
				);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

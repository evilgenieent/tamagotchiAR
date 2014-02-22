using UnityEngine;
using System.Collections;

public class PotUpdater : MonoBehaviour {

	public GameObject pot;
	public PetLogic cat;

	private float lastPot;

	// Use this for initialization
	void Start () {
		lastPot = Time.time;
	}

	void FixedUpdate() {
		pot.transform.position =
			new Vector3(
				transform.position.x,
				0.0f,
				transform.position.z
				);
		
		if (pot.transform.position.magnitude <= 30 && Time.time - lastPot > 3) {
			// TODO: prevent spam
			lastPot = Time.time;
			StartCoroutine(cat.OnFood());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

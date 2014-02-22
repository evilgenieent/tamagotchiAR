using UnityEngine;
using System.Collections;

public class AnimationTest : MonoBehaviour {

	private bool animate=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!animation.isPlaying) {
			animation.Play("Walk");		
		}
	}
}

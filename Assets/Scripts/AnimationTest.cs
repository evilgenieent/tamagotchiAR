using UnityEngine;
using System.Collections;

public class AnimationTest : MonoBehaviour {

	private bool animate=false;
	private Animator animator;

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.animator) {
			animator.SetBool("Dead", true);
		}
	}
}

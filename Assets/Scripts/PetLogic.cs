using UnityEngine;
using System.Collections;

public class PetLogic : MonoBehaviour {

	private float hunger = 0;
	private float tiredness = 0;
	// whether the pet needs to go to the loo
	private float excrements = 0;
	private float health = 100;
	// whether the pet needs attention or not
	private float mood = 100;

	private const float HUNGER_THRESHOLD = 50;
	private const float TIREDNESS_THRESHOLD = 50;
	private const float EXCREMENTS_THRESHOLD = 50;
	private const float HEALTH_THRESHOLD = 50;
	private const float MOOD_THRESHOLD = 50;

	private float nextUpdate = 0;

	public float updateRate;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		checkState ();

		if (Time.time > nextUpdate) {
			UpdateState();
			nextUpdate = Time.time + updateRate;
		}
	}

	void checkState() {

		if (hunger > HUNGER_THRESHOLD) {
			sendHungry();
		}

		if (tiredness > TIREDNESS_THRESHOLD) {
			sendSleepy();
		}

		if (excrements > EXCREMENTS_THRESHOLD) {
			sendExcrement();
		}

		if (health < HEALTH_THRESHOLD) {
			sendDeath();
		}

		if (mood < MOOD_THRESHOLD) {
			sendAttention();
		}

	}

	void UpdateState() {

		hunger += Random.Range(0.2f, 0.5f);
		tiredness += Random.Range(0.2f, 0.5f);
		excrements += Random.Range(0.2f, 0.5f);
		mood -= Random.Range(0.2f, 0.5f);

		if (hunger < HUNGER_THRESHOLD || mood < MOOD_THRESHOLD) {
			health -= Random.Range(0.2f, 0.5f);
		} else {
			health += Random.Range(0.2f, 0.5f);
		}

		if (mood > MOOD_THRESHOLD) {
			health += Random.Range(0.2f, 0.5f);
		}

	}

	void sendHungry() {
		if (this.animator) {
			animator.SetBool("Hungry", true);
		}
	}

	void sendSleepy() {
		if (this.animator) {
			animator.SetBool("Sleepy", true);
		}
	}

	void sendExcrement() {
		if (this.animator) {
			animator.SetBool("Excrement", true);
		}
	}

	void sendDeath() {
		if (this.animator) {
			animator.SetBool("Dead", true);
		}
	}

	void sendAttention() {
		// TODO
	}

	public void onFood() {
		// TODO
	}
	
	public void onAttention() {
		// TODO
	}
}

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


	// all thresholds
	public const float HUNGER_THRESHOLD = 1;
	public const float TIREDNESS_THRESHOLD = 50;
	public const float EXCREMENTS_THRESHOLD = 50;
	public const float HEALTH_THRESHOLD = 50;
	public const float MOOD_THRESHOLD = 50;

	private const float HUNGER_THRESHOLD = 1;
	private const float TIREDNESS_THRESHOLD = 50;
	private const float EXCREMENTS_THRESHOLD = 50;
	private const float HEALTH_THRESHOLD = 50;
	private const float MOOD_THRESHOLD = 50;


	// incremention random factor (range)
	public const float INC_MIN = 0.2f;
	public const float INC_MAX = 0.5f;

	//
	public const float EAT_WAIT = 5;
	public const float PLAY_WAIT = 5;

	private float nextUpdate = 0;
	// how fast the script updates
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

		hunger += Random.Range(INC_MIN, INC_MAX);
		tiredness += Random.Range(INC_MIN, INC_MAX);
		excrements += Random.Range(INC_MIN, INC_MAX);
		mood -= Random.Range(INC_MIN, INC_MAX);

		if (hunger < HUNGER_THRESHOLD || mood < MOOD_THRESHOLD) {
			health -= Random.Range(INC_MIN, INC_MAX);
		} else {
			health += Random.Range(INC_MIN, INC_MAX);
		}

		if (mood > MOOD_THRESHOLD) {
			health += Random.Range(INC_MIN, INC_MAX);
		}

	}

	void SendHungry() {
		if (animator) {
			animator.SetBool("Hungry", true);
		}
	}

	void SendAttention() {
		if (animator) {
			animator.SetBool("Attention", true);
		}
	}

	void SendSleepy() {
		if (animator) {
			animator.SetBool("Sleepy", true);
		}
	}

	void SendExcrement() {
		if (animator) {
			animator.SetBool("Excrement", true);
		}
		if (animator) {
			animator.SetBool("Excrement", false);
		}
	}

	void SendDeath() {
		if (animator) {
			animator.SetBool("Dead", true);
		}
	}
	

	public void OnFood() {
		// gets called by user interaction script
		if (animator) {
			animator.SetBool("Eating", true);
		}
		//yield return new WaitForSeconds (EAT_WAIT);
		if (animator) {
			animator.SetBool("Eating", false);
			animator.SetBool("Hungry", false);
		}

	}
	
	public void OnPetting() {
		if (animator) {
			animator.SetBool("Petting", true);
		}
		//yield return new WaitForSeconds (PLAY_WAIT);
		if (animator) {
			animator.SetBool("Petting", false);
			animator.SetBool("Attention", false);
		}
	}
}

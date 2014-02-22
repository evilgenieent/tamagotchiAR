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
	private const float HUNGER_THRESHOLD = 10;
	private const float TIREDNESS_THRESHOLD = 50;
	private const float EXCREMENTS_THRESHOLD = 3;
	private const float HEALTH_THRESHOLD = 50;
	private const float MOOD_THRESHOLD = 50;

	// incremention random factor (range)
	public const float INC_MIN = 0.2f;
	public const float INC_MAX = 0.5f;

	//
	public const float EXCREMENT_WAIT = 1;
	public const float EAT_WAIT = 1;
	public const float PLAY_WAIT = 1;

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
			SendHungry();
		}

		if (tiredness > TIREDNESS_THRESHOLD) {
			SendSleepy();
		}

		if (excrements > EXCREMENTS_THRESHOLD) {
			StartCoroutine( SendExcrement());
		}

		if (health < HEALTH_THRESHOLD) {
			SendDeath();
		}

		if (mood < MOOD_THRESHOLD) {
			SendAttention();
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

		Debug.Log ("Excrements: " + excrements);
		Debug.Log ("Hunger: " + hunger);
		Debug.Log ("Tiredness: " + tiredness);
		Debug.Log ("Mood: " + mood);
		Debug.Log ("Health: " + health);
	}

	void SendHungry() {
		Debug.Log ("+++SendHungry+++");
		if (animator) {
			animator.SetBool("Hungry", true);
		}

	}

	void SendAttention() {
		Debug.Log ("+++SendAttention+++");
		if (animator) {
			animator.SetBool("Attention", true);
		}
	}

	void SendSleepy() {
		Debug.Log ("+++SendSleepy+++");
		if (animator) {
			animator.SetBool("Sleepy", true);
		}
	}

	IEnumerator SendExcrement() {
		Debug.Log ("+++SendExcrement+++");
		if (animator) {
			animator.SetBool("Excrement", true);
		}
		excrements = 0;
		yield return new WaitForSeconds (EXCREMENT_WAIT);
		if (animator) {
			animator.SetBool("Excrement", false);
		}

	}


	void SendDeath() {
		Debug.Log ("+++SendDeath+++");
		if (animator) {
			animator.SetBool("Dead", true);
		}
	}
	

	public IEnumerator OnFood() {
		Debug.Log ("+++OnFood+++");
		// gets called by user interaction script
		if (animator) {
			animator.SetBool("Eating", true);
		}
		yield return new WaitForSeconds (EAT_WAIT);
		if (animator) {
			animator.SetBool("Eating", false);
			animator.SetBool("Hungry", false);
		}
		hunger -= 50;
		if (hunger < 0)
			hunger = 0;

	}
	
	public IEnumerator OnPetting() {
		Debug.Log ("+++OnPetting+++");
		if (animator) {
			animator.SetBool("Petting", true);
		}
		yield return new WaitForSeconds (PLAY_WAIT);
		if (animator) {
			animator.SetBool("Petting", false);
			animator.SetBool("Attention", false);
		}
		mood -= 50;
		if (mood < 0)
			mood = 0;
	}
}

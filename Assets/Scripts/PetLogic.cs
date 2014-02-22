using UnityEngine;
using System.Collections;


public class PetLogic : MonoBehaviour {

	private float hunger = 0.0f;
	private float tiredness = 0.0f;
	// whether the pet needs to go to the loo
	private float excrements = 0.0f;
	private float health = 100.0f;
	// whether the pet needs attention or not
	private float mood = 100.0f;

	public GUIText txtHunger;
	// TODO implement tiredness
	//public GUIText txtTiredness;
	public GUIText txtHealth;
	public GUIText txtMood;

	// all thresholds
	private const float HUNGER_THRESHOLD = 50;
	private const float TIREDNESS_THRESHOLD = 50;
	private const float EXCREMENTS_THRESHOLD = 50;
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
	private GameObject excrement;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		excrement = GameObject.FindWithTag ("Excrement");
		Renderer[] rendererComponents = excrement.GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = excrement.GetComponentsInChildren<Collider>(true);
		
		// Disable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = false;
		}
		
		// Disable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = false;
		}
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

		//if (tiredness > TIREDNESS_THRESHOLD) {
		//	SendSleepy();
		//}

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

		hunger += Random.Range(INC_MIN*2, INC_MAX*2);
		tiredness += Random.Range(INC_MIN, INC_MAX);
		//excrements += Random.Range(INC_MIN, INC_MAX);
		mood -= Random.Range(INC_MIN, INC_MAX);



		if (hunger < HUNGER_THRESHOLD || mood < MOOD_THRESHOLD) {
			health -= Random.Range(INC_MIN, INC_MAX);
		} else {
			health += Random.Range(INC_MIN, INC_MAX);
		}

		if (mood > MOOD_THRESHOLD) {
			health += Random.Range(INC_MIN, INC_MAX);
		}

		// do not allow values above 100
		if (hunger > 100.0f)
			hunger = 100.0f;
		if (hunger < 0.0f)
			hunger = 0.0f;
		if (tiredness > 100.0f)
			tiredness = 100.0f;
		if (tiredness < 0.0f)
			tiredness = 0.0f;
		if (mood > 100.0f)
			mood = 100.0f;
		if (mood < 0.0f)
			mood = 0.0f;

		// do not allow values above 100
		if (health > 100.0f)
			health = 100.0f;
		if (health < 0.0f)
			health = 0.0f;

		txtHealth.text = "Health: " + Mathf.Floor(health) + "%";
		txtHunger.text = "Hunger: " + Mathf.Floor(hunger) + "%";
		txtMood.text = "Mood: " + Mathf.Floor(mood) + "%";
		// TODO implement tiredness
		//txtTiredness.text = "Tiredness: " + tiredness + "%";

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
		yield return new WaitForSeconds (EXCREMENT_WAIT);
		if (animator) {
			animator.SetBool("Excrement", true);
		}
		excrements = 0;
		yield return new WaitForSeconds (EXCREMENT_WAIT);
		if (animator) {
			animator.SetBool("Excrement", false);
		}
		excrement = GameObject.FindWithTag ("Excrement");
		Renderer[] rendererComponents = excrement.GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = excrement.GetComponentsInChildren<Collider>(true);
		
		// Disable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = true;
		}
		
		// Disable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = true;
		}
	}


	void SendDeath() {
		Debug.Log ("+++SendDeath+++");
		if (animator) {
			animator.SetBool("Dead", true);
		}
	}
	

	public IEnumerator OnFood() {
		if(hunger >= 50) {
			Debug.Log ("+++OnFood+++");
			// gets called by user interaction script
			if (animator) {
				animator.SetBool("Eating", true);
			}
			yield return new WaitForSeconds (EAT_WAIT);
			if (animator) {
				animator.SetBool("Eating", false);
				//yield return new WaitForSeconds (EAT_WAIT);
				animator.SetBool("Hungry", false);
			}
			hunger = 0;
			if (hunger < 0)
				hunger = 0;

			// cat wants to poo randomly
			excrements += Random.Range (60.0f, 60.0f);
		}
	}
	
	public IEnumerator OnPetting() {
		if(mood <= 50) {
			Debug.Log ("+++OnPetting+++");
			if (animator) {
				animator.SetBool("Petting", true);
			}
			yield return new WaitForSeconds (PLAY_WAIT);
			if (animator) {
				animator.SetBool("Petting", false);
				//yield return new WaitForSeconds (PLAY_WAIT);
				animator.SetBool("Attention", false);
			}
			mood += 50;
			if (mood > 100)
				mood = 100;
			tiredness += 20;
			if (tiredness > 100)
				tiredness = 100;
		}
	}
}

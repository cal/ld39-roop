using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanksAndZombies : MonoBehaviour {

	public class Plank {
		public int type;
		public GameObject gameObject;
		public Animator animator;
		public int health;

		public Plank(int t, GameObject g, Animator a, int h) {
			type = t;
			gameObject = g;
			animator = a;
			health = h;
		}
	}

	private List<Plank> livePlanks;
	private List<Plank> deadPlanks;

	public GameObject plank1;
	public GameObject plank2;
	public GameObject plank3;
	public GameObject zombies;

	private Plank p1;
	private Plank p2;
	private Plank p3;

	private float zombieKnockTickSize = 6.0f;
	private float zombieKnockTickTime;

	public GameObject gameOverBackground;
	public Text gameOverText;

	private PeopleMover thePlayer;
	private bool playerAtDoor;
	float plankRepairTimeTarget = 4.0f;
	float plankRepairTimeCurrent = 0.0f;

	// Use this for initialization
	void Start () {
		zombies.SetActive(false);

		deadPlanks = new List<Plank>();
		livePlanks = new List<Plank>();
		livePlanks.Add(new Plank(1, plank1, plank1.GetComponent<Animator>(), 4));
		livePlanks.Add(new Plank(2, plank2, plank2.GetComponent<Animator>(), 4));
		livePlanks.Add(new Plank(3, plank3, plank3.GetComponent<Animator>(), 4));

		zombieKnockTickTime = zombieKnockTickSize;

		gameOverBackground.SetActive(false);

		thePlayer = FindObjectOfType<PeopleMover>();
	}
	
	// Update is called once per frame
	void Update () {
		zombieKnockTickTime -= Time.deltaTime;
		if (zombieKnockTickTime <= 0.0f) {
			ZombieKnock();
		}

		if ((deadPlanks.Count > 0) && playerAtDoor && (Input.GetAxis("Vertical") != 0.0f)) {
			thePlayer.repairingDoor = true;
			plankRepairTimeCurrent += Time.deltaTime;
			if (plankRepairTimeCurrent > plankRepairTimeTarget) {
				plankRepairTimeCurrent = 0.0f;
				RepairAPlank();
			}
		}
		else {
			plankRepairTimeCurrent = 0.0f;
		}
	}

	void ZombieKnock() {
		zombieKnockTickTime = zombieKnockTickSize;

		if (livePlanks.Count > 0) {
			int bangOnPlank = Random.Range(0, livePlanks.Count);
			livePlanks[bangOnPlank].health--;
			if (livePlanks[bangOnPlank].health <= 0) {
				PlayPlankDeathAnimation(livePlanks[bangOnPlank].animator);
				deadPlanks.Add(livePlanks[bangOnPlank]);
				livePlanks.Remove(livePlanks[bangOnPlank]);
			}
			else {
				PlayKnockAnimation(livePlanks[bangOnPlank].animator);
			}
		}
		else {
			plank1.SetActive(false);
			plank2.SetActive(false);
			plank3.SetActive(false);
			zombies.SetActive(true);

			Time.timeScale = 0.0f;
			gameOverText.text = "Game Over\n\nZombies came in through the door";
			gameOverBackground.SetActive(true);
		}
	}

	void PlayKnockAnimation(Animator anim) {
		anim.Play("Plank", 0, 0.5f);
		StartCoroutine(StopPlayingKnockAnimation(anim));
	}
	IEnumerator StopPlayingKnockAnimation(Animator anim) {
		yield return new WaitForSeconds(1);
		anim.Play("Plank", 0, 0.0f);
	}

	void PlayPlankDeathAnimation(Animator anim) {
		anim.Play("Plank", 0, 1.5f);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "Player") {
			playerAtDoor = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.name == "Player") {
			playerAtDoor = false;
		}
	}

	void RepairAPlank() {
		deadPlanks[0].health = 4;
		deadPlanks[0].animator.Play("Plank", 0, 0.0f);
		livePlanks.Add(deadPlanks[0]);
		deadPlanks.Remove(deadPlanks[0]);
	}
}

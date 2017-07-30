using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
**	References:
**	- https://unity3d.com/learn/tutorials/topics/2d-game-creation/2d-character-controllers
**	- https://www.youtube.com/watch?v=KBSHz-ee8Sk
**	- https://unity3d.com/learn/tutorials/projects/2d-ufo-tutorial/counting-collectables-and-displaying-score?playlist=25844
**	- https://docs.unity3d.com/ScriptReference/Time-time.html
*/

public class PeopleMover : MonoBehaviour {

	// stuff we need to know about
	private Rigidbody2D rigi;

	// left and right movement;
	public float maxSpeed = 10.0f;
	private bool facingRight = true;

	// up and down movement
	public bool onLadder = false;
	public float climbSpeed = 10.0f;
	private float climbVelocity;
	private float gravityWas;

	// this has nothing to do with moving about
	// now we're just putting the game logic on the player
	// wat. anyway, this is the power level stuff
	public Text powerText;
	public Slider powerSlider;
	private int powerLevel = 100;
	private float powerTickSize = 2.0f;
	private float powerTickTime;
	private int powerDrain = 1;

	// i mean why stop now
	// here's a clock
	public Text clockText;
	private int clockMinutes;
	private int clockSeconds;
	private float clockStart;

	// and we need to get crankin'
	public bool onCrank = false;
	public Rigidbody2D crank;
	public float crankSpeed = 10.0f;
	public GameObject crankIndicator;
	private float crankVelocity;
	private float crankTickSize = 1.0f;
	private float crankTickTime;
	private int crankPower = 1;
	public bool crankingAway = false;

	// and we need to know about the mice
	public GameObject friedMouse1;
	public bool onMouse1 = false;
	public GameObject friedMouse2;
	public bool onMouse2 = false;
	public GameObject friedMouse3;
	public bool onMouse3 = false;
	public GameObject friedMouse4;
	public bool onMouse4 = false;
	public GameObject stuckMouse;
	public bool onMouse5 = false;
	private int friedMice = 0;
	private int friedMiceDrain = 3;

	// and this
	public bool repairingDoor = false;

	// and the game has to end
	public GameObject gameOverBackground;
	public Text gameOverText;

	// Use this for initialization
	void Start () {
		rigi = GetComponent<Rigidbody2D>();

		gravityWas = rigi.gravityScale;

		PowerUpdate();
		ClockUpdate();
		CrankUpdate();

		gameOverBackground.SetActive(false);

		clockStart = Time.time;
	}
	
	// FixedUpdate is called once per physics frame
	void FixedUpdate() {
		float move = Input.GetAxis("Horizontal");

		rigi.velocity = new Vector2(move * maxSpeed, rigi.velocity.y);

		if((move > 0 && !facingRight) || (move < 0 && facingRight)) {
			Flip();
		}

		if (onLadder) {
			rigi.gravityScale = 0.0f;

			climbVelocity = climbSpeed * Input.GetAxis("Vertical");

			rigi.velocity = new Vector2(rigi.velocity.x, climbVelocity);
		}
		if (!onLadder) {
			rigi.gravityScale = gravityWas;
		}

		if (onCrank) {
			// if the mouse is stuck, we can't crank
			if (!stuckMouse.activeSelf) {
				crankVelocity = crankSpeed * Input.GetAxis("Vertical");
				if (crankVelocity > 0) {
					crankVelocity = crankVelocity * -1;
					crankingAway = true;
				}
				else {
					crankingAway = false;
				}
				crank.angularVelocity = crankVelocity;
			}
		}
		else {
			crankingAway = false;
		}

		if (onMouse1 && friedMouse1.activeSelf && Input.GetAxis("Vertical") != 0.0f) {
			friedMouse1.SetActive(false);
		}
		if (onMouse2 && friedMouse2.activeSelf && Input.GetAxis("Vertical") != 0.0f) {
			friedMouse2.SetActive(false);
		}
		if (onMouse3 && friedMouse3.activeSelf && Input.GetAxis("Vertical") != 0.0f) {
			friedMouse3.SetActive(false);
		}
		if (onMouse4 && friedMouse4.activeSelf && Input.GetAxis("Vertical") != 0.0f) {
			friedMouse4.SetActive(false);
		}
		if (onMouse5 && stuckMouse.activeSelf && Input.GetAxis("Vertical") != 0.0f) {
			stuckMouse.SetActive(false);
		}

	}

	// Update is called once per frame
	void Update() {
		powerTickTime -= Time.deltaTime;
		if (powerTickTime <= 0.0f) {
			PowerUpdate();
		}

		ClockUpdate();

		crankTickTime -= Time.deltaTime;
		if (crankTickTime <= 0.0f) {
			CrankUpdate();
		}
	}

	void Flip()	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void PowerUpdate() {
		powerTickTime = powerTickSize;

		friedMice = 0;
		if (friedMouse1.activeSelf) { friedMice++; }
		if (friedMouse2.activeSelf) { friedMice++; }
		if (friedMouse3.activeSelf) { friedMice++; }
		if (friedMouse4.activeSelf) { friedMice++; }

		powerLevel -= powerDrain + (friedMice * friedMiceDrain);

		if (powerLevel <= 0) {
			powerLevel = 0;

			Time.timeScale = 0.0f;
			gameOverText.text = "Game Over\n\nRan out of power";
			gameOverBackground.SetActive(true);
		}

		powerText.text = powerLevel.ToString() + "%";
		powerSlider.value = powerLevel;
	}

	void ClockUpdate() {
		clockMinutes = Mathf.FloorToInt((Time.time - clockStart) / 60.0f);
		clockSeconds = Mathf.FloorToInt((Time.time - clockStart) % 60.0f);
		clockText.text = clockMinutes.ToString("00") + ":" + clockSeconds.ToString("00");
	}

	void CrankUpdate() {
		if (crank.angularVelocity < 0.0f) {
			powerLevel += crankPower;

			if (powerLevel > 100) {
				powerLevel = 100;
			}

			powerText.text = powerLevel.ToString() + "%";

			crankIndicator.SetActive(true);
		}
		else {
			crankIndicator.SetActive(false);
		}

		// and reset
		crankTickTime = crankTickSize;
	}

}

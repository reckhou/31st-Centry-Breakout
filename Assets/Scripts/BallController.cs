using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public Vector2 InitialForce;
	public bool IsRolling = false;

	public AudioSource AudioBump;
	public AudioSource AudioExplode;
	public AudioSource AudioScore;
	public AudioSource AudioCombo;

	public int HitBrickCnt;
	public int Combo;
	private float LastHitBrickTime;

	// Use this for initialization
	void Start () {

	}

	public void Roll() {
		if (IsRolling) {
			return;
		}
		this.GetComponent<Rigidbody2D>().AddForce(InitialForce);
		IsRolling = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (!IsRolling) {
			Vector2 pos = PadController.Instance.transform.localPosition; 
			pos.y += PadController.Instance.GetComponent<SpriteRenderer>().bounds.size.y/2
					+ GetComponent<SpriteRenderer>().bounds.size.y/2;
			transform.localPosition = pos;
			return;
		}
	}

	void FixedUpdate() {
		if (GetComponent<Rigidbody2D>().velocity.magnitude > GameController.Instance.MaxBallSpeed) {
			GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized 
				* GameController.Instance.MaxBallSpeed;
		} else if (GetComponent<Rigidbody2D>().velocity.magnitude < GameController.Instance.MinBallSpeed) {
			GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized 
				* GameController.Instance.MinBallSpeed;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "BottomBorder") {
			AudioExplode.Play();
			GameController.Instance.LoseLife();
			Reset();
			return;
		}

		if (coll.gameObject.tag != "Brick" && IsRolling) {
			AudioBump.Play();
			if (coll.gameObject.tag == "Pad") {
				float deltaX = transform.localPosition.x - coll.transform.localPosition.x;
				float padLength = coll.transform.GetComponent<BoxCollider2D>().size.x *
					coll.transform.localScale.x;
				float maxForceX = 0.3f;
				float addForceX = deltaX / padLength * maxForceX;
				gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(addForceX, 0.1f));
			}
		} else if (coll.gameObject.tag == "Brick") {
			HitBrickCnt++;

			if (Time.time - LastHitBrickTime < GameController.Instance.ComboInterval) {
				Combo++;
			} else {
				Combo = 0;
			}

			if (Combo > GameController.Instance.ScoreBonusAfterCombo) {
				AudioCombo.Play();
			} else {
				AudioScore.Play();
			}
			GameController.Instance.GainScore(Combo);
			if (Combo >= GameController.Instance.GainLifeAfterCombo && 
			    Combo % GameController.Instance.GainLifeAfterCombo == 0) {
				GameController.Instance.GainLife();
			}

			LastHitBrickTime = Time.time;
		}

		if (coll.gameObject.tag == "Brick" || coll.gameObject.tag == "TopBorder") {
			GetComponent<Rigidbody2D>().velocity *= (1.0f + GameController.Instance.BallAccleration);
		}

	}

	public void Reset() {
		IsRolling = false;
		Combo = 0;
	}

	public void Perish() {
		Destroy(gameObject);
	}
}

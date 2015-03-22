using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public Vector2 InitialForce;
	public bool IsRolling = false;

	float bounceRate = 1.0f;

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
//		if (bounceRate < 1.0f) {
//			GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 1.0f;
//			bounceRate = 1.0f;
//		}

		if (coll.gameObject.tag == "BottomBorder") {
//			Perish();
		} else if (coll.gameObject.tag == "Brick" || coll.gameObject.tag == "Border") {
			GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 1.1f;
		} else {
			GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 1.0f;
		}

		print (GetComponent<Rigidbody2D>().velocity.magnitude);
	}

	public void Perish() {
		Destroy(gameObject);
	}
}

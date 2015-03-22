using UnityEngine;
using System.Collections;

public class PadController : MonoBehaviour {
	public float Speed;

	public GameObject LeftBorder;
	public GameObject RightBorder;

	public float Friction;

	private static PadController instance;
	public static PadController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<PadController>();
			}
			
			return instance;
    	}
  	}

	enum Direction {
		Left = 0,
		Right
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (GameController.Instance.BallList.Count > 0) {
					GameController.Instance.BallList[0].GetComponent<BallController>().Roll();
				}
			}

			Vector2 pos = transform.localPosition;
			if (Input.GetKey(KeyCode.LeftArrow) && !IsTouchBorder(Direction.Left)) {
				pos.x -= Speed;
			} else if (Input.GetKey(KeyCode.RightArrow) && !IsTouchBorder(Direction.Right)) {
				pos.x += Speed;
			}

			transform.localPosition = pos;
		}
	}

	bool IsTouchBorder(Direction direction) {
		float selfWidth = GetComponent<SpriteRenderer>().bounds.size.x;
		if (direction == Direction.Left &&
		    transform.localPosition.x < LeftBorder.transform.localPosition.x +
		    	LeftBorder.GetComponent<BoxCollider2D>().bounds.size.x/2 + selfWidth/2) {
			return true;
		}

		if (direction == Direction.Right &&
		    transform.localPosition.x > RightBorder.transform.localPosition.x -
		    	LeftBorder.GetComponent<BoxCollider2D>().bounds.size.x/2 - selfWidth/2) {
			return true;
		}

		return false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// Add a friction force when pad is moving.
		if (coll.gameObject.tag == "Ball") {
			Vector2 force = Vector2.zero;
			if (Input.GetKey(KeyCode.LeftArrow)) {
				force.x = - Friction;
			} else if (Input.GetKey(KeyCode.RightArrow)) {
				force.x = Friction;
			}
			force.y = Friction;
			coll.transform.GetComponent<Rigidbody2D>().AddForce(force);
		}
		
	}
}

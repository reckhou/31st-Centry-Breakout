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
		float XValue = Input.GetAxis("Horizontal");
		if (Input.anyKey || XValue != 0) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) {
				if (GameController.Instance.BallList.Count > 0) {
					GameController.Instance.BallList[0].GetComponent<BallController>().Roll();
				}
			}

			Vector2 pos = transform.localPosition;
			float actualSpeed = Speed;

			if (XValue < 0) {
				if (!IsTouchBorder(Direction.Left)){
					if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) {
						actualSpeed *= 2f;
					}
				} else {
					return;
				}
			} else if (XValue > 0) {
				if (!IsTouchBorder(Direction.Right)){
					if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) {
						actualSpeed *= 2f;
					}
				} else {
					return;
				}
			}

			float translation = XValue * actualSpeed;
			translation *= Time.deltaTime;
			transform.Translate(translation, 0, 0);
		}
	}

	bool IsTouchBorder(Direction direction) {
		float selfWidth = GetComponent<SpriteRenderer>().bounds.size.x;
		if (direction == Direction.Left &&
		    transform.position.x < LeftBorder.transform.position.x +
		    	LeftBorder.GetComponent<BoxCollider2D>().bounds.size.x/2 + selfWidth/2) {
			return true;
		}

		if (direction == Direction.Right &&
		    transform.position.x > RightBorder.transform.position.x -
		    	LeftBorder.GetComponent<BoxCollider2D>().bounds.size.x/2 - selfWidth/2) {
			return true;
		}

		return false;
	}

	public void Perish() {
		Destroy(gameObject);
	}
}

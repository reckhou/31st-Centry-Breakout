using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public Vector2 InitialForce;
	public bool IsRolling = false;

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

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "BottomBorder") {
			Perish();
		}
		
	}

	public void Perish() {
		Destroy(gameObject);
	}
}

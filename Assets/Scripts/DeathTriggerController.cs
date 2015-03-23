using UnityEngine;
using System.Collections;

public class DeathTriggerController : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Brick") {
			GameController.Instance.GameOver();
			gameObject.SetActive(false);
		}
	}
}

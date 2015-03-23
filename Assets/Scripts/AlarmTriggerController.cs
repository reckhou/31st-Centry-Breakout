using UnityEngine;
using System.Collections;

public class AlarmTriggerController : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Brick") {
			GameController.Instance.PlayAlarm();
		}
	}
}

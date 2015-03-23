using UnityEngine;
using System.Collections;

public class InstructionController : MonoBehaviour {
	public float FlashInterval;
	private float lastFlash;
	public GameObject StartText;
	void Start() {
		lastFlash = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			Application.LoadLevel("main");
		}

		if (Time.time - lastFlash > FlashInterval) {
			bool isActive = StartText.activeSelf;
			StartText.SetActive(!isActive);
			lastFlash = Time.time;
		}
	}
}

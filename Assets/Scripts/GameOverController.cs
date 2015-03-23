using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {
	public float FlashInterval;
	private float lastFlash;
	public GameObject StartText;
	public GameObject ScoreText;
	void Start() {
		lastFlash = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("main");
		}
		
		if (Time.time - lastFlash > FlashInterval) {
			bool isActive = StartText.activeSelf;
			StartText.SetActive(!isActive);
			lastFlash = Time.time;
		}
	}

	public void SetScore(int score) {
		ScoreText.GetComponent<TextMesh>().text = "Score: " + score.ToString();
	}
}

using UnityEngine;
using System.Collections;

public class ScoreAreaController : MonoBehaviour {
	public GameObject ScoreText;
	
	private static ScoreAreaController instance;
	public static ScoreAreaController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<ScoreAreaController>();
			}
			
			return instance;
		}
	}
	
	public void UpdateScore(int score, int combo) {
		if (combo <= GameController.Instance.ScoreBonusAfterCombo) {
			ScoreText.GetComponent<TextMesh>().text = score.ToString();
		} else {
			ScoreText.GetComponent<TextMesh>().text = "Combo: " + combo.ToString() + " " + score.ToString();
		}
	}
}

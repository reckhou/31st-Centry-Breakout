using UnityEngine;
using System.Collections;

public class LifeAreaController : MonoBehaviour {
	public GameObject LifeText;

	private static LifeAreaController instance;
	public static LifeAreaController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<LifeAreaController>();
			}
			
			return instance;
		}
	}

	public void UpdateLifeCount(int lifeCount) {
		LifeText.GetComponent<TextMesh>().text = "x" + lifeCount.ToString();
	}
}

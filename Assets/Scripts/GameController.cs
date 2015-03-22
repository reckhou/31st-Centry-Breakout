using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public List<GameObject> BallList;
	public GameObject BattleLayer;
	public GameObject BrickPrefab;

	private static GameController instance;
	public static GameController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<GameController>();
			}
			
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		SpawnBricks();
	}

	void SpawnBricks() {
		Vector2 spawnPos = new Vector2(-6.0f, 0);
		for (int i = 0; i < 10; i++ ) {
			GameObject tmp = Instantiate(BrickPrefab);
			tmp.transform.parent = BattleLayer.transform;
			tmp.transform.localPosition = spawnPos;
			spawnPos.x += tmp.GetComponent<SpriteRenderer>().bounds.size.x;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}

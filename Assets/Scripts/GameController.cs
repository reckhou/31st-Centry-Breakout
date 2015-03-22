using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public List<GameObject> BallList;
	public GameObject BattleLayer;
	public GameObject BrickPrefab;
	public GameObject BrickZone;
	public Vector2 StartSpawnPos;
	public float SpawnInterval;
	public int MaxBrickSpawnCnt;
	public int MinBrickSpawnCnt;
	public float MaxBallSpeed;
	public float MinBallSpeed;
	private float LastSpawn;

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
		Vector2 brickSize = BrickPrefab.GetComponent<SpriteRenderer>().bounds.size;
		Vector2 brickZonePos = BrickZone.transform.localPosition;
		brickZonePos.y -= brickSize.y;
		BrickZone.transform.localPosition = brickZonePos;
		StartSpawnPos.y += brickSize.y;

		// calculate whole spawn zone length, then make every line center align.
		int spawnCnt = Tools.Random(MinBrickSpawnCnt, MaxBrickSpawnCnt);
		Vector2 spawnPos = StartSpawnPos;
		float spawnZoneLength = brickSize.x * MaxBrickSpawnCnt;
		spawnPos.x = spawnPos.x + spawnZoneLength/2 - brickSize.x * (spawnCnt/2.0f);

		for (int i = 0; i < spawnCnt; i++ ) {
			GameObject tmp = Instantiate(BrickPrefab);
			tmp.transform.parent = BrickZone.transform;
			tmp.transform.localPosition = spawnPos;
			spawnPos.x += BrickPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - LastSpawn > SpawnInterval) {
			SpawnBricks();
			LastSpawn = Time.time;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public int LifeCount;
	public int Score;

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
	public float BallAccleration;
	public float ComboInterval;
	public int ScoreBonusAfterCombo;
	public int GainLifeAfterCombo;

	private float LastSpawn;

	public AudioSource GainLifeAudio;
	public AudioSource GameOverAudio;
	public AudioSource AlarmAudio;
	private bool gameOver;

	public GameObject GameUI;
	public GameObject GameOverUI;

	private int SpawnCnt;
	public int SpawnSpeedUpRounds;

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
		// init UI
		LifeAreaController.Instance.UpdateLifeCount(LifeCount);
		gameOver = false;
	}

	void SpawnBricks() {
		SpawnCnt++;
		Vector2 brickSize = BrickPrefab.GetComponent<SpriteRenderer>().bounds.size;
		Vector2 brickZonePos = BrickZone.transform.localPosition;
		brickZonePos.y -= brickSize.y;
		BrickZone.transform.localPosition = brickZonePos;
		StartSpawnPos.y += brickSize.y;

		// calculate whole spawn zone length, then make every line center align.
		int spawnCnt = Tools.Random(MinBrickSpawnCnt, MaxBrickSpawnCnt);
		Vector2 spawnPos = StartSpawnPos;
		float spawnZoneLength = brickSize.x * spawnCnt;
		spawnPos.x -= spawnZoneLength/2;

		for (int i = 0; i < spawnCnt; i++ ) {
			GameObject tmp = Instantiate(BrickPrefab);
			tmp.transform.parent = BrickZone.transform;
			tmp.transform.localPosition = spawnPos;
			spawnPos.x += BrickPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		}
	}

	// Update is called once per frame
	void Update () {
		if (gameOver) {
			return;
		}

		if (Time.time - LastSpawn > SpawnInterval) {
			SpawnBricks();
			LastSpawn = Time.time;
			if (SpawnCnt % SpawnSpeedUpRounds == 0) {
				SpawnInterval *= 0.95f;
			}
		}
	}

	public void LoseLife() {
		LifeCount--;
		LifeAreaController.Instance.UpdateLifeCount(LifeCount);
		if (LifeCount < 0) {
			GameOver();
		}

		if (LifeCount < 1) {
			PlayAlarm();
		}
	}

	public void GainLife() {
		LifeCount++;
		LifeAreaController.Instance.UpdateLifeCount(LifeCount);
		GainLifeAudio.Play();
	}

	public void GameOver() {
		foreach (GameObject obj in BallList) {
			obj.GetComponent<BallController>().Perish();
		}
		PadController.Instance.Perish();
		GameOverAudio.Play();
		GameUI.SetActive(false);
		GameOverUI.SetActive(true);
		GameOverUI.GetComponent<GameOverController>().SetScore(Score);
		BrickZone.SetActive(false);
		gameOver = true;
	}

	public void GainScore(int combo) {
		if (combo <= ScoreBonusAfterCombo) {
			Score += 1000;
		} else {
			Score += combo * 1000;
		}
		ScoreAreaController.Instance.UpdateScore(Score, combo);
	}

	public void PlayAlarm() {
		if (!AlarmAudio.isPlaying) {
			AlarmAudio.Play();
		}
	}
}

using UnityEngine;
using System.Collections;
using System;

public class Tools : MonoBehaviour {

	static System.Random ran;

	public static int Random(int min, int max) {
		if (max <= min) {
			return max;
		}

		if (ran == null) {
			long tick = DateTime.Now.Ticks;
			ran = new System.Random((int)(tick & 0xffffffffL) | (int) (tick >> 32));
		}

		int result = ran.Next(min, max);
		return result;
	}

	public static float Random(float min, float max) {
		int result = Random((int)(min * 100.0f), (int)(max * 100.0f));
		return (float)result / 100.0f;
	}

	public static bool Random() {
		int result = Random(0, 2);
		if (result == 0) {
			return false;
		} else {
			return true;
		}
	}
}

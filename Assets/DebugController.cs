﻿using UnityEngine;
using System.Collections;

public class DebugController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TextMesh> ().text = Input.GetAxis ("Horizontal").ToString ();
	}
}

using UnityEngine;
using System.Collections;

public class AIWalker : MonoBehaviour {

	CharacterController CC;

	void Start () {
		CC = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {

	}
}

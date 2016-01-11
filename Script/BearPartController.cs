using UnityEngine;
using System.Collections;

public class BearPartController : MonoBehaviour {

	void Awake () {
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onGameRestart += Start;
		
	}

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class MonsterAudioController : MonoBehaviour {
	private GameController gameController;
	public AudioSource growling;
	public AudioSource starting_yell;

	void Awake () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		gameController.onGamePlayStart += () => {
			Debug.Log ("'I WANT MY FRIENDS BACK !!'");
			if (starting_yell != null){
				starting_yell.Play ();
			}
			Debug.Log ("Invoking Growl");
			Invoke ("StartGrowl", 2);
		};
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void StartGrowl () {
		Debug.Log ("Growling");
		growling.Play ();
	}
}

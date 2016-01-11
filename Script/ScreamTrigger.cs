using UnityEngine;
using System.Collections;

public class ScreamTrigger : MonoBehaviour {
	GameController gameController;
	
	void Awake () {
		gameController = GameObject.FindGameObjectWithTag("GameController")
			.GetComponent<GameController>();
		
		gameController.onSceneStart += () => 
			gameObject.GetComponent<AudioSource>().mute = true;
		
		gameController.onSceneEnd += () => 
			gameObject.GetComponent<AudioSource>().mute = false;
	}

	// Use this for initialization
	void Start () {
        Invoke("Scream", 9f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Scream() {
        this.gameObject.GetComponent<AudioSource>().Play();
        
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SceneController : MonoBehaviour {
	public MovieTexture[] scenes;

	float changeSpeed = 2f;
	System.Action changeAlpha;
	GameController gameController;
	SoundClip soundManager;
	RawImage sceneVideo;
	AudioSource sceneAudio;
	bool isPlaying;

	void Awake () {
		gameController = GameObject.FindGameObjectWithTag("GameController")
			.GetComponent<GameController>();
		soundManager = GameObject.Find ("SoundManager")
			.GetComponent<SoundClip>();

		gameController.onGameWin += () => {
			StartVideo(3);
			Invoke ("PlayWinEnd", scenes[3].duration);
		};

		gameController.onGameLose += () => {
			StartVideo(5);
		};
	}

	// Use this for initialization
	void Start () {
		sceneVideo = GetComponent<RawImage>();
		sceneAudio = GetComponent<AudioSource>();
		Color color = Color.white;
		color.a = 0f;
		sceneVideo.color = color;
		sceneVideo.enabled = false;
		isPlaying = false;
		changeAlpha = null;

		Invoke("PlayIntroScene", 0f);
	}

	void PlayIntroScene () {
		StartVideo(0);
		soundManager.RegisterPlayWhere(scenes[0].duration);
	}
	
	// Update is called once per frame
	void Update () {
		if (changeAlpha != null)
			changeAlpha();
	}

	public void StartVideo (int idx) {
		if (isPlaying == false) {
			sceneVideo.texture = scenes[idx];
			changeAlpha = IncreaseAlpha;
			isPlaying = true;
			var mv = (MovieTexture)sceneVideo.texture;
			sceneAudio.clip = mv.audioClip;
			mv.Play();
			sceneAudio.Play();

			Invoke("StopVideo", mv.duration - 1f / changeSpeed);
			gameController.SceneStart();
		}
	}

	void IncreaseAlpha () {
		Color color = sceneVideo.color;
		color.a += Time.deltaTime * changeSpeed;

		if (color.a >= 0.9f && !sceneVideo.enabled) {
			color.a = 1f;
			changeAlpha = null;
			sceneVideo.enabled = true;
		}
		sceneVideo.color = color;
	}

	void StopVideo() {
		changeAlpha = DecreaseAlpha;
	}

	void DecreaseAlpha () {
		Color color = sceneVideo.color;
		color.a -= Time.deltaTime * changeSpeed;

		if (color.a <= 0.1f && sceneVideo.enabled) {
			color.a = 0f;
			changeAlpha = null;
			sceneVideo.enabled = false;
			var mv = (MovieTexture)sceneVideo.texture;
			mv.Stop();
			sceneAudio.Stop();
			sceneVideo.texture = null;
			sceneAudio.clip = null;
			isPlaying = false;

			gameController.SceneEnd();
		}
		sceneVideo.color = color;
	}

	public float DelayWhenLose () {
		float time;
		time = scenes[5].duration;
		return time;
	}

	public float DelayWhenWin () {
		float time;
		time = scenes[3].duration + scenes[4].duration;
		return time;
	}

	void PlayWinEnd () {
		StartVideo(4);
	}
}

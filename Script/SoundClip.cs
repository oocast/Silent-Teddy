using UnityEngine;
using System.Collections;

public class SoundClip : MonoBehaviour {

	public AudioSource[] thunder;
    public AudioSource moveForward;
    public AudioSource collecting;
    public AudioSource kidScream;
	public AudioSource whats_that;
	public AudioSource first_bear;
	public AudioSource second_bear;
	public AudioSource fix_bear;
	public AudioSource heavy_breathing;
	public AudioSource come_out_and_play;
	public AudioSource bears_are_students;
	public AudioSource too_quiet;
	public AudioSource where_is_everyone;

	private VillainAI villain;
	bool being_chased = false;

    int i = 0;
	// Use this for initialization
	void Start () {
        Random.seed = System.DateTime.Now.Second;
		being_chased = false;
		villain = GameObject.Find ("Villain").GetComponent<VillainAI> ();
		villain.whileChasing += () => {
			if (!being_chased) {
				being_chased = true;
				heavy_breathing.Play ();
			}
		};
		villain.whilePatrolling += () => {
			if (being_chased) {
				being_chased = false;
				heavy_breathing.Stop ();
			}
		};
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().onGameRestart += () => {
			if (heavy_breathing.isPlaying) {
				heavy_breathing.Stop ();
			}
		};
        //invoke play kidScream
    }
	
	// Update is called once per frame
	void Update () {

    }

	public void PlayThunder () {
        if (!thunder[0].isPlaying && !thunder[1].isPlaying)
        {
            i = Random.Range(0, 2);
            thunder[i].Play();
        }
	}

	public void RegisterPlayWhere(float delay = 0f) {
		Invoke ("PlayWhere", delay);
	}

	void PlayWhere() {
		where_is_everyone.Play();
	}
}

using UnityEngine;
using System.Collections;

public class LightningController : MonoBehaviour {
	public float flash_interval = 3;
	public float flash_interval_min = 15f;
	public float flash_interval_max = 20f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		flash_interval = Random.Range(flash_interval_min,flash_interval_max);
	}
}

using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
	public float interval = 0.3f;
	public Light flicker_light;
	public float max_intensity = 0.6f;
	bool flashNow = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(flashNow == false){
			flashNow = true;
			Invoke("Flash", (float)Random.Range(0.0f, interval));
		}
	}

	void Flash (){
		StartCoroutine ("FlashScene");
	}
	
	IEnumerator FlashScene (){
		float alpha = 1;
		/*bool double_flash = false;
		float rand = Random.Range (-1.0f, 1.0f);
		if (rand > 0.5f) {
			double_flash = true;
		}*/
		
		while (alpha > 0) {
			alpha -=1f;
			flicker_light.intensity = Mathf.Abs(alpha - 1) * max_intensity;
			yield return new WaitForSeconds (0.1f);
		}
		while (alpha < 1) {
			alpha +=1f;
			flicker_light.intensity = Mathf.Abs(alpha - 1) * max_intensity;
			yield return new WaitForSeconds (0.1f);
		}
		while (alpha > 0) {
			alpha -=1f;
			flicker_light.intensity = Mathf.Abs(alpha - 1) * max_intensity;
			yield return new WaitForSeconds (0.1f);
		}
		while (alpha < 1) {
			alpha +=1f;
			flicker_light.intensity = Mathf.Abs(alpha - 1) * max_intensity;
			yield return new WaitForSeconds (0.1f);
		}
		flashNow = false;
	}
}

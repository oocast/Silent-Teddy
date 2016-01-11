using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour {

    public static bool FindPlayer = false;  //for changing color from yellow to red; false -> yellow, true -> red
	// Use this for initialization
	void Start () {
		FindPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (FindPlayer == true)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            GameObject.Find("FaceLight").GetComponent<Light>().color = Color.red;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            GameObject.Find("FaceLight").GetComponent<Light>().color = Color.yellow;
        }
   }
}

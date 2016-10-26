using UnityEngine;
using System.Collections;

public class levelUp : MonoBehaviour {

	FaderManager fader;
	public string faderName;

	void Awake(){
		fader = GameObject.Find (faderName).GetComponent<FaderManager>();
	}

	void OnTriggerEnter(Collider other) {
		if(fader==null)
			fader = GameObject.Find (faderName).GetComponent<FaderManager>();
		fader.level++;
	}
}

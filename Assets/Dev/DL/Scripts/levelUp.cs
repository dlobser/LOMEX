using UnityEngine;
using System.Collections;

public class levelUp : MonoBehaviour {

	FaderManager fader;
	public string faderName;
	bool buildable = true;

	void Awake(){
//		if (GameObject.Find (faderName)!= null)
//			fader = GameObject.Find (faderName).GetComponent<FaderManager> ();
//		else
//			buildable = false;
	}

	void OnTriggerEnter(Collider other) {
		if (GameObject.Find (faderName)!=null) {
			if (fader == null)
				fader = GameObject.Find (faderName).GetComponent<FaderManager> ();
			fader.level++;
		}
	}
}

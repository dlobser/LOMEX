using UnityEngine;
using System.Collections;

public class ResetLevel : MonoBehaviour {

	FaderManager fader;
	float init;
	// Use this for initialization
	void Start () {
		fader = GetComponent<FaderManager> ();
		init = fader.level;
	}
	
	public void reset(){
		fader.level = init;
	}
}

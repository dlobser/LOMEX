using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CopyPosition : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position != target.transform.position)
			this.transform.position = target.transform.position;
	}
}

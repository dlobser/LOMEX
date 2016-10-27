using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CopyPosition : MonoBehaviour {

	public GameObject[] target;
	public List<GameObject> toBePlaced = new List<GameObject>();
	int toBePlacedCount;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			toBePlaced.Add (child.gameObject);
		}
		toBePlacedCount = toBePlaced.Count;
	}
	
	// Update is called once per frame
//	void Update () {
//		if (this.transform.position != target.transform.position)
//			this.transform.position = target.transform.position;
//	}

	public void UpdateTargetTransform(int targetIndex, int whichToUpdate){
		if (whichToUpdate == 0)
		{
			toBePlaced[ targetIndex % toBePlacedCount ].transform.position = target [targetIndex].transform.position;
		} else if (whichToUpdate == 1)
		{
			toBePlaced[ targetIndex % toBePlacedCount ].transform.rotation = target [targetIndex].transform.rotation;
		} else
		{
			toBePlaced[ targetIndex % toBePlacedCount ].transform.position = target [targetIndex].transform.position;
			toBePlaced[ targetIndex % toBePlacedCount ].transform.rotation = target [targetIndex].transform.rotation;
		}
	}
}

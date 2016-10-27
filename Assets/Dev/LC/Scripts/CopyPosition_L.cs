using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CopyPosition_L : MonoBehaviour {

	public GameObject[] target;
	public GameObject[] toBePlaced;
	int toBePlacedCount;

	// Use this for initialization
	void Start () {
		toBePlacedCount = toBePlaced.Length;
	}

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

	public void HideTarget(int targetIndex){
		if (toBePlaced [targetIndex].activeInHierarchy) {
			target [targetIndex].SetActive (false);
			toBePlaced [targetIndex].SetActive (false);
		}
			
	}

	public void ShowTarget(int targetIndex){
		if (!toBePlaced [targetIndex].activeInHierarchy) {
			target [targetIndex].SetActive (true);
			toBePlaced [targetIndex].SetActive (true);
		}
			
	}
}

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
			toBePlaced[ targetIndex ].transform.position = target [targetIndex].transform.position;

			if (targetIndex == 2 || targetIndex == 5 || targetIndex == 6)
			{
				toBePlaced [targetIndex].transform.position = toBePlaced [targetIndex-1].transform.position;
			}
		}
		else if (whichToUpdate == 1)
		{
			toBePlaced[ targetIndex ].transform.rotation = target [targetIndex].transform.rotation;

			if (targetIndex == 2 || targetIndex == 5)
			{
				toBePlaced [targetIndex].transform.rotation = toBePlaced [targetIndex-1].transform.rotation;
			}
		}
		else
		{
			toBePlaced[ targetIndex ].transform.position = target [targetIndex].transform.position;
			toBePlaced[ targetIndex ].transform.rotation = target [targetIndex].transform.rotation;

			if (targetIndex == 2 || targetIndex == 5)
			{
				toBePlaced [targetIndex].transform.position = toBePlaced [targetIndex-1].transform.position;
				toBePlaced [targetIndex].transform.rotation = toBePlaced [targetIndex-1].transform.rotation;
			}
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

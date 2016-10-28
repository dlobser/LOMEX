using UnityEngine;
using System.Collections;

public class TriggerAnimation : MonoBehaviour {

	public string[] triggerName;
	public Animator[] anim;

	public void Trigger(){
		if (anim != null) {
			for (int i = 0; i < triggerName.Length; i++) {
				if(anim[i]!=null)
					anim[i].SetTrigger (triggerName[i]);
			}

		}
	}
}

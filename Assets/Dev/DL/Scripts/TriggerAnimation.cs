using UnityEngine;
using System.Collections;

public class TriggerAnimation : MonoBehaviour {

	public string triggerName;
	public Animator anim;

	public void Trigger(){
		if(anim!=null)
			anim.SetTrigger (triggerName);
	}
}

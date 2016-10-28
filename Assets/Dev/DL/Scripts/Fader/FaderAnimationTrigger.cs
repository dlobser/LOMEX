using UnityEngine;
using System.Collections;

public class FaderAnimationTrigger : Fader {

	public TriggerAnimation[] triggerAnimation;
	int which = 0;

	public override void Fade(){
		float currentLevel = ((Mathf.Clamp (level, min, max)-min) / levels) * triggerAnimation.Length;
		int which = (int)Mathf.Clamp (Mathf.Floor (currentLevel), 0, triggerAnimation.Length - 1);
		triggerAnimation [which].Trigger ();
	}
}

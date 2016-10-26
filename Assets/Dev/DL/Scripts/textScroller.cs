using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace exp3{
	public class textScroller : MonoBehaviour {

		public int charLength;
		public string text;
		private char[] letters;
		private string tempText;
		public float speed;
		private float count = 0;
		Text textCTRL;
		private int counter = 0;

		void Start () {
			text += " ";
			textCTRL = GetComponent<Text> ();
			letters = text.ToCharArray ();
			print (letters[0]);

		}
		
		// Update is called once per frame
		void Update () {

			count += Time.deltaTime;
			if (count>speed) {
				count=0;
				counter++;
				if (counter > letters.Length-1)
					counter = 0;
				tempText = "";

				for (int i = 0,q=counter; i < charLength; i++) {
					tempText += letters [q].ToString ();
					q++;
					if(q>letters.Length-1)
						q=0;
				}
				textCTRL.text = tempText;
			}
		}
	}
}

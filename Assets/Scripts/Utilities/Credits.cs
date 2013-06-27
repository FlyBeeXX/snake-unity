using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	
	
	private Rect mainRect = new Rect (Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600);
	
	
	public string[] _credits;// = {" Developer: Gabriel Nadler", "Art: Dominik Würsch"};

	// Use this for initialization
	void Start () {
		_credits = new string[2];
		_credits[0] = "Developer: Gabriel Nadler";
		_credits[1] = "Art: Dominik Würsch";
		Debug.Log (_credits[0]);
		Debug.Log (_credits[1]);
		foreach (string credit in _credits)
			Debug.Log(credit);
		
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUIStyle style = new GUIStyle();//GUI.skin.box);
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 30;
		style.normal.textColor = Color.white;
		style.border = new RectOffset(0,0,0,0);
//		GUI.BeginGroup(this.mainRect);
		int counter = 0;
		foreach (string credit in _credits) {
			GUI.Box (new Rect (Screen.width/2-400,200+50*counter,800,50),credit,style);
			counter++;
		}
//		GUI.EndGroup();
	}
}

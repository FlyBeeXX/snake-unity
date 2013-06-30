using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	
	
	private Rect mainRect = new Rect (Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600);
	public float verticalChange = 0.1f;
	public float moveWait = 0.3f;
	
	public GUIStyle textStyle;
	
	
	private bool started = false;
	

	
	private float offset = 0;
	
	public string[] _credits = {" Developer: Gabriel Nadler"};

	// Use this for initialization
	void Start () {
	}
	
	IEnumerator moveUp()
	{
		this.started = true;
		while(true)
		{
			offset += verticalChange;
			yield return new WaitForSeconds(moveWait);
		}
		
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.BeginGroup(this.mainRect);
		int counter = 01;
		if (!this.started)
			StartCoroutine(moveUp());
		foreach (string credit in _credits) {
			GUI.Box (new Rect (this.mainRect.width/2-200,this.mainRect.height-50*counter-offset,400,50),credit,this.textStyle);
			counter++;
		}
		GUI.EndGroup();
	}
}

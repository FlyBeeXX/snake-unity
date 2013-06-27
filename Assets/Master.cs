using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Master : StateMachineBehaviourEx {
	public static Master Instance;
	private Dictionary<string,object> config; 
	
	
	public enum MasterStates {
		Starting,
		Menu,
		Playing,
		Paused
	}
	
	void Start() {
		
		currentState = MasterStates.Starting;
	}

	static void DoNothing(){}
	
	
	public Action OnGUIHandler = DoNothing;
	
	
#region Starting
	public int countdown = 3;
	
	
	private IEnumerator Countdown() {
		while (countdown > 0){
			Debug.Log (Time.time);
			countdown--;
			yield return new WaitForSeconds(1);
		}
		
	}
		void Starting_EnterState () {
			if (Application.loadedLevelName != GameScenes.StartingScreen.ToString())
				Application.LoadLevel(GameScenes.StartingScreen.ToString());
//			this.OnGUIHandler = this.Starting_OnGUI;
			Debug.Log ("Entering Starting State");
			StartCoroutine(this.Countdown());
		}
		void Starting_OnGUI() {
			float width = 400f;
			float height = 20f;
			GUI.Box (new Rect(Screen.width/2-width/2, Screen.height/2-height/2, width,height), "Hey there this is the loading screen");
			width = 60f;
			height = 60;
			GUI.Box (new Rect(Screen.width/2-width/2, Screen.height/2+200-height/2, width,height), this.countdown.ToString());
			if (this.countdown == 0)
				this.currentState = MasterStates.Menu;
		}
			
		void Starting_ExitState () {
			Debug.Log ("Exiting Starting State");
		}
#endregion
	
#region Menu
	
		public void theGameJustStarted(){
			this.currentState = MasterStates.Playing;
		}
		void Menu_EnterState () {
			Application.LoadLevel(GameScenes.MainMenu.ToString());
		}
		void Menu_Update() {

		}
		void Menu_ExitState () {
			Debug.Log ("Exiting Menu State");
		}
#endregion
	
#region Playing
	void Playing_EnterState () {
		
		Debug.Log ("Entering Playing State");
	}
	void Playing_Update() {

	}
	void Playing_ExitState () {
		Debug.Log ("Exiting Playing State");
	}
#endregion
	
	
	
	
}

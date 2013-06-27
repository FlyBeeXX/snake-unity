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
		this.mainmenu = this.gameObject.GetComponent<MainMenu>();
		this._menupaused = this.gameObject.GetComponent<MenuPaused>();
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
			float height = 30f;
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
		private MainMenu mainmenu = null;
		void Menu_EnterState () {

			this.mainmenu.enabled = true;
		}
		void Menu_Update() {

		}
		void Menu_ExitState () {
			this.mainmenu.enabled = false;
			Debug.Log ("Exiting Menu State");
		}
	public void theGameJustStarted(){
		this.currentState = MasterStates.Playing;
	}
#endregion
	
#region Playing
	void Playing_EnterState () {
		this.mainmenu.enabled = false;
		this._menupaused.enabled = false;
		
		Debug.Log ("Entering Playing State");
	}
	void Playing_Update() {
		
		if (Input.GetKeyUp(KeyCode.Escape))
			this.currentState = MasterStates.Paused;

	}
	void Playing_ExitState () {
		
		Debug.Log ("Saving game State haha");
	}
#endregion
	
#region Paused
	private MenuPaused _menupaused = null;
	void Paused_EnterState () {

		Time.timeScale = 0; // Pause the game
		this._menupaused.enabled = true;
//		Application.LoadLevel(GameScenes.MainMenu.ToString());
		
		Debug.Log ("Entering Paused State");
	}
	void Paused_Update() {

	}
	void Paused_ExitState () {
		this._menupaused.enabled = false;
		Debug.Log ("Exiting Paused State");
	}
#endregion
	
	
	
	
}
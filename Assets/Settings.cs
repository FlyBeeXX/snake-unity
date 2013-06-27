using UnityEngine;
using System.Collections;
using System;
public class Settings : StateMachineBehaviourEx {
	static void DoNothing(){}
	
	[HideInInspector]
	public Action lastMenu = DoNothing;
	public enum SettingsStates {
		Exit
	}

	// Use this for initialization
	void Start () {
		this.currentState = SettingsStates.Exit;
	}
	
	
	#region Exit
        private Rect exitRect = new Rect(0,Screen.height*4/5,200,30);
        public string exitString = "Back to Main Menu";
    
        public GUIStyle exitStyle;
        private Color exitSaveColor;
	
		private MainMenu _mainmenu = null;
        void Exit_EnterState () {
			if (!_mainmenu)
				this._mainmenu = this.gameObject.GetComponent<MainMenu>();
            exitSaveColor = exitStyle.normal.textColor;
            exitStyle.normal.textColor = Color.red;
        }
        void Exit_Update() {
			bool enter = Input.GetKeyUp (KeyCode.Return) || Input.GetKeyUp (KeyCode.KeypadEnter);
			if (enter)
			{
				this.lastMenu();
//				Application.LoadLevel(GameScenes.MainMenu.ToString());
			}
        }
        void Exit_ExitState () {
            exitStyle.normal.textColor = this.exitSaveColor;
        }
    #endregion
	
	void updateRectangles()
	{
		exitRect.x = Screen.width/2-exitRect.width/2;
	}
		
	
	void OnGUI()
	{
		this.updateRectangles();
		GUIStyle style = new GUIStyle(GUI.skin.box);
		style.fontStyle = FontStyle.Bold;
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 30;
		GUI.Box(new Rect(Screen.width/5,Screen.height/2,Screen.width*3/5,50), "OVER 9000 SETTINGS!!", style);
		GUI.Box(exitRect, exitString, exitStyle);
		
		
	}
}

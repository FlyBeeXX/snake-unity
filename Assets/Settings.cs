using UnityEngine;
using System.Collections;

public class Settings : StateMachineBehaviourEx {
	
	public enum SettingsStates {
		Exit
	}

	// Use this for initialization
	void Start () {
		this.currentState = SettingsStates.Exit;
	}
	
	
	#region Exit
        public Rect exitRect = new Rect(0,160,200,30);
        public string exitString = "Back to Main Menu";
    
        public GUIStyle exitStyle;
        private Color exitSaveColor;
        void Exit_EnterState () {
            exitSaveColor = exitStyle.normal.textColor;
            exitStyle.normal.textColor = Color.red;
            Debug.Log ("Entering Exit State");
        }
        void Exit_Update() {
//                bool up = Input.GetKeyUp(KeyCode.UpArrow);
//                if(up)
//                    currentState = MainMenuStates.Settings;
//                bool down = Input.GetKeyUp(KeyCode.DownArrow);
//                if(down)
//                    currentState = MainMenuStates.NewGame;
				bool enter = Input.GetKeyUp (KeyCode.Return) || Input.GetKeyUp (KeyCode.KeypadEnter);
				if (enter)
					Application.LoadLevel("MainMenu");
        }
        void Exit_ExitState () {
            exitStyle.normal.textColor = this.exitSaveColor;
            Debug.Log ("Exiting Exit State");
        }
    #endregion
	
	void updateRectangles()
	{
		exitRect.x = Screen.width/2-exitRect.width/2;
	}
		
	
	void OnGUI()
	{
		this.updateRectangles();
		GUI.Box(new Rect(10,10,200,50), "Test New Scene");
		GUI.Box(exitRect, exitString, exitStyle);
		
		
	}
}

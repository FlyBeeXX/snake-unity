using UnityEngine;
using System.Collections;
using System;
public class Settings : StateMachineBehaviourEx {
	static void DoNothing(){}
	
	public enum SettingsStates {
		Exit,
		Hidden,
		ChangeBack
	}
	
	public Enum lastState;
	public StateMachineBehaviourEx lastMachine;

	// Use this for initialization
	void Start () {
		this.useGUI = false;
		this.currentState = SettingsStates.Hidden;
	}
	
	
	#region Exit
    private Rect exitRect = new Rect(0,Screen.height*4/5,200,30);
    public string exitString = "Back to Main Menu";

    public GUIStyle exitStyle;
    private Color exitSaveColor;

	private MainMenu _mainmenu = null;


    void Exit_EnterState () {
        exitSaveColor = exitStyle.normal.textColor;
        exitStyle.normal.textColor = Color.red;
		this.lastState = (Enum)stateMachine.lastState;
		this.lastMachine = stateMachine.lastStateMachineBehaviour;
    }
    void Exit_Update() {
		bool enter = Input.GetKeyUp (KeyCode.Return) || Input.GetKeyUp (KeyCode.KeypadEnter);
		if (enter)
			this.currentState = SettingsStates.ChangeBack;
    }
    void Exit_ExitState () {
        exitStyle.normal.textColor = this.exitSaveColor;
		
    }
	void Exit_OnGUI()
	{
		this.updateRectangles();
		GUIStyle style = new GUIStyle(GUI.skin.box);
		style.fontStyle = FontStyle.Bold;
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 30;
		GUI.Box(new Rect(Screen.width/5,Screen.height/2,Screen.width*3/5,50), "OVER 9000 SETTINGS!!", style);
		GUI.Box(exitRect, exitString, exitStyle);
		
		
	}
    #endregion
	void updateRectangles()
	{
		exitRect.x = Screen.width/2-exitRect.width/2;
	}
	
#region Hidden
	// Hidden State
#endregion
	
#region ChangeBack
	void ChangeBack_EnterState () {
		stateMachine.SetState(this.lastState, this.lastMachine);
	}
	void ChangeBack_Update() {

	}
	void ChangeBack_ExitState () {
	}
#endregion
	

		
	

}

using System;
using UnityEngine;
using System.Collections.Generic;
public partial class MainMenu :  StateMachineBehaviourEx {
#region New Game
        
    public Rect newGameRect = new Rect(0,40,200,30);
    
    public string newGameString = "New Game";
    public GUIStyle newGameStyle;
	
    private Color newGameSaveColor;
    // Use this for State initialization, IEnumerator or void
    void NewGame_EnterState () {
        
		this.saveState((MainMenuStates)currentState);
        this.newGameSaveColor = newGameStyle.normal.textColor;
        this.newGameStyle.normal.textColor = Color.red;
    }
    
    
    
    void NewGame_Update() {
        if(this.pressedUp())
            currentState = MainMenuStates.Exit;
            
        if(this.pressedDown())
            currentState = MainMenuStates.LoadGame;
		if (this.pressedEnter())
		{
			Application.LoadLevel (GameScenes.Snake.ToString());
			master.theGameJustStarted();
		}

            
    }
    void NewGame_ExitState () {
        this.newGameStyle.normal.textColor = this.newGameSaveColor;
        
    }
	void NewGame_OnGUI() {
		drawMenu();
	}
	
    
    
#endregion
	
}
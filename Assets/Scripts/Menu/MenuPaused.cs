using System;
using UnityEngine;
using System.Collections.Generic;


public class MenuPaused :  StateMachineBehaviourEx {

    private Dictionary<string,object> config; 
	
	private string lastState = "lastMainPausedState";
	
	private Master master;

    
    public enum MainPausedStates {
        Continue,
        LoadGame,
        Settings,
        Exit
    }
	


    // Use this for initialization
    void Start () {
		this.config = Configuration.config;
		this.master = GameObject.FindGameObjectWithTag("GameController").GetComponent<Master>();
		object startState;
		
		
		if (!config.TryGetValue(lastState, out startState))
		{
			startState = MainPausedStates.Continue;
			this.saveState((MainPausedStates)startState);
		}
        this.useGUI = true;
        currentState = (MainPausedStates)startState;
    }
	
	private void saveState(MainPausedStates state)
	{
		if (config.ContainsKey(lastState))
			config.Remove(lastState);
		config.Add(lastState, state);
	}
	
	
	#region Helpers
	
	private bool pressedEnter()
	{
		return Input.GetKeyUp (KeyCode.Return) || Input.GetKeyUp (KeyCode.KeypadEnter);
	}
	

	private bool pressedDown()
	{
		return Input.GetKeyUp(KeyCode.DownArrow);
	}
	private bool pressedUp()
	{
		return Input.GetKeyUp(KeyCode.UpArrow);
	}
	#endregion
    
    #region New Game
        
    public Rect continueRect = new Rect(0,40,200,30);
    
    public string continueString = "Continue";
    public GUIStyle continueStyle;
	
    private Color continueSaveColor;
    // Use this for State initialization, IEnumerator or void
    void Continue_EnterState () {
        
		this.saveState((MainPausedStates)currentState);
        this.continueSaveColor = continueStyle.normal.textColor;
        this.continueStyle.normal.textColor = Color.red;
    }
    
    
    
    void Continue_Update() {
        if(this.pressedUp())
            currentState = MainPausedStates.Exit;
            
        if(this.pressedDown())
            currentState = MainPausedStates.LoadGame;
		if (this.pressedEnter())
		{
			master.theGameJustStarted();
		}

            
    }
    void Continue_ExitState () {
        this.continueStyle.normal.textColor = this.continueSaveColor;
        
    }
	
    
    
    #endregion

    #region LoadGame
        public Rect loadGameRect = new Rect(0,80,200,30);
        public string loadGameString = "Load Game";
    
        public GUIStyle loadGameStyle;
        private Color loadGameSaveColor;
        void LoadGame_EnterState () {
			this.saveState((MainPausedStates)currentState);
            loadGameSaveColor = loadGameStyle.normal.textColor;
            loadGameStyle.normal.textColor = Color.green;
            
        }
        void LoadGame_Update() {
//          Debug.Log ("LoadGame Update");

            if(this.pressedUp())
                currentState = MainPausedStates.Continue;
            if(this.pressedDown())
                currentState = MainPausedStates.Settings;

                
        }
        void LoadGame_ExitState () {
            loadGameStyle.normal.textColor = this.loadGameSaveColor;
            
        }

    #endregion
    
    #region Settings
        public Rect settingsRect = new Rect(0,120,200,30);
        public string settingsString = "Settings";
//		private string 
    	private Settings _settings = null;
        public GUIStyle settingsStyle;
        private Color settingsSaveColor;
        void Settings_EnterState () {
			if (!this._settings)
				this._settings = this.gameObject.GetComponent<Settings>();
			this.saveState((MainPausedStates)currentState);
            settingsSaveColor = settingsStyle.normal.textColor;
            settingsStyle.normal.textColor = Color.blue;
            
        }
        void Settings_Update() {
                if(this.pressedUp())
                    currentState = MainPausedStates.LoadGame;
                if(this.pressedDown())
                    currentState = MainPausedStates.Exit;
				if (this.pressedEnter()) {
					this._settings.enabled = true;
					Configuration.lastMenu = ()=> {
						this.enabled = true;
						this._settings.enabled = false;
					};
					this.enabled = false;
				}
        }
        void Settings_ExitState () {
			this._settings.enabled = false;
            settingsStyle.normal.textColor = this.settingsSaveColor;
            
        }
    #endregion
    #region Exit
        public Rect exitRect = new Rect(0,160,200,30);
        public string exitString = "Exit Game";
    
        public GUIStyle exitStyle;
        private Color exitSaveColor;
        void Exit_EnterState () {
			this.saveState((MainPausedStates)currentState);
            exitSaveColor = exitStyle.normal.textColor;
            exitStyle.normal.textColor = Color.blue;
            
        }
        void Exit_Update() {
                if(this.pressedUp())
                    currentState = MainPausedStates.Settings;
                if(this.pressedDown())
                    currentState = MainPausedStates.Continue;
				if (this.pressedEnter())
					Application.Quit();
        }
        void Exit_ExitState () {
            exitStyle.normal.textColor = this.exitSaveColor;
            
        }
    #endregion
    void updateRects ()
    {
        this.continueRect.x = Screen.width/2 - this.continueRect.width/2;
        this.loadGameRect.x = Screen.width/2 - this.loadGameRect.width/2;
        this.settingsRect.x = Screen.width/2 - this.settingsRect.width/2;
        this.exitRect.x = Screen.width/2 - this.exitRect.width/2;
    }
    
    
    
    void OnGUI()
    {
        this.updateRects();
        GUI.Box (this.continueRect, this.continueString, this.continueStyle    );
        GUI.Box (this.loadGameRect, this.loadGameString, this.loadGameStyle    );        
        GUI.Box (this.settingsRect, this.settingsString, this.settingsStyle    );
        GUI.Box (this.exitRect, this.exitString, this.exitStyle    );
    }
    
}

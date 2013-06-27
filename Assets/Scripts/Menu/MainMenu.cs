using System;
using UnityEngine;
using System.Collections.Generic;


public class MainMenu :  StateMachineBehaviourEx {

    private Dictionary<string,object> config; 
	
	private string lastState = "lastMainMenuState";
	
	private Master master;

    
    public enum MainMenuStates {
        NewGame,
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
			startState = MainMenuStates.NewGame;
			this.saveState((MainMenuStates)startState);
		}
        this.useGUI = true;
        currentState = (MainMenuStates)startState;
    }
	
	private void saveState(MainMenuStates state)
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
	
    
    
    #endregion

    #region LoadGame
        public Rect loadGameRect = new Rect(0,80,200,30);
        public string loadGameString = "Load Game";
    
        public GUIStyle loadGameStyle;
        private Color loadGameSaveColor;
        void LoadGame_EnterState () {
			this.saveState((MainMenuStates)currentState);
            loadGameSaveColor = loadGameStyle.normal.textColor;
            loadGameStyle.normal.textColor = Color.green;
            
        }
        void LoadGame_Update() {
//          Debug.Log ("LoadGame Update");

            if(this.pressedUp())
                currentState = MainMenuStates.NewGame;
            if(this.pressedDown())
                currentState = MainMenuStates.Settings;

                
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
			this.saveState((MainMenuStates)currentState);
            settingsSaveColor = settingsStyle.normal.textColor;
            settingsStyle.normal.textColor = Color.blue;
            
        }
        void Settings_Update() {
                if(this.pressedUp())
                    currentState = MainMenuStates.LoadGame;
                if(this.pressedDown())
                    currentState = MainMenuStates.Exit;
				if (this.pressedEnter()) {
					this._settings.enabled = true;
					this._settings.lastMenu = ()=> {
						this.enabled = true;
						this._settings.enabled = false;
					};
					this.enabled = false;
//					Application.LoadLevel(GameScenes.Settings.ToString());
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
			this.saveState((MainMenuStates)currentState);
            exitSaveColor = exitStyle.normal.textColor;
            exitStyle.normal.textColor = Color.blue;
            
        }
        void Exit_Update() {
                if(this.pressedUp())
                    currentState = MainMenuStates.Settings;
                if(this.pressedDown())
                    currentState = MainMenuStates.NewGame;
				if (this.pressedEnter())
					master.Quit();
        }
        void Exit_ExitState () {
            exitStyle.normal.textColor = this.exitSaveColor;
            
        }
    #endregion
    void updateRects ()
    {
        this.newGameRect.x = Screen.width/2 - this.newGameRect.width/2;
        this.loadGameRect.x = Screen.width/2 - this.loadGameRect.width/2;
        this.settingsRect.x = Screen.width/2 - this.settingsRect.width/2;
        this.exitRect.x = Screen.width/2 - this.exitRect.width/2;
    }
    
    
    
    void OnGUI()
    {
        this.updateRects();
        GUI.Box (this.newGameRect, this.newGameString, this.newGameStyle    );
        GUI.Box (this.loadGameRect, this.loadGameString, this.loadGameStyle    );        
        GUI.Box (this.settingsRect, this.settingsString, this.settingsStyle    );
        GUI.Box (this.exitRect, this.exitString, this.exitStyle    );
    }
    
}

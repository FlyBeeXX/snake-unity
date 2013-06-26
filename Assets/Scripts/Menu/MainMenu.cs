using System;
using UnityEngine;
using System.Collections.Generic;


public class MainMenu :  StateMachineBehaviourEx {

    private Dictionary<string,object> config; 
	
	private string lastState = "lastState";
    
    public enum MainMenuStates {
        NewGame,
        LoadGame,
        Settings,
        Exit
    }

    // Use this for initialization
    void Start () {
		Debug.Log("Start");
		this.config = GameObject.FindGameObjectWithTag("GameController").GetComponent<Configuration>().config;
		object startState;
		
		
		if (!config.TryGetValue(lastState, out startState))
		{
			Debug.Log ("No value present");
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
	
    
    #region New Game
        
    public Rect newGameRect = new Rect(0,40,200,30);
    
    public string newGameString = "New Game";
    public GUIStyle newGameStyle;
	
    private Color newGameSaveColor;
    // Use this for State initialization, IEnumerator or void
    void NewGame_EnterState () {
        Debug.Log ("Entering new Game State");
		this.saveState((MainMenuStates)currentState);
        this.newGameSaveColor = newGameStyle.normal.textColor;
        this.newGameStyle.normal.textColor = Color.red;
    }
    
    
    
    void NewGame_Update() {
//      Debug.Log ("NewGame Update");
            bool up = Input.GetKeyUp(KeyCode.UpArrow);
            if(up)
                currentState = MainMenuStates.Exit;
                
            bool down = Input.GetKeyUp(KeyCode.DownArrow);
            if(down)
                currentState = MainMenuStates.LoadGame;

            
    }
    void NewGame_ExitState () {
        newGameStyle.normal.textColor = this.newGameSaveColor;
        Debug.Log ("Exiting new Game State");
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
            Debug.Log ("Entering LoadGame State");
        }
        void LoadGame_Update() {
//          Debug.Log ("LoadGame Update");

            bool up = Input.GetKeyUp(KeyCode.UpArrow);
            if(up)
                currentState = MainMenuStates.NewGame;
            bool down = Input.GetKeyUp(KeyCode.DownArrow);
            if(down)
                currentState = MainMenuStates.Settings;

                
        }
        void LoadGame_ExitState () {
            loadGameStyle.normal.textColor = this.loadGameSaveColor;
            Debug.Log ("Exiting LoadGame State");
        }

    #endregion
    
    #region Settings
        public Rect settingsRect = new Rect(0,120,200,30);
        public string settingsString = "Settings";
    
        public GUIStyle settingsStyle;
        private Color settingsSaveColor;
        void Settings_EnterState () {
		
			this.saveState((MainMenuStates)currentState);
            settingsSaveColor = settingsStyle.normal.textColor;
            settingsStyle.normal.textColor = Color.blue;
            Debug.Log ("Entering Settings State");
        }
        void Settings_Update() {
                bool up = Input.GetKeyUp(KeyCode.UpArrow);
                if(up)
                    currentState = MainMenuStates.LoadGame;
                bool down = Input.GetKeyUp(KeyCode.DownArrow);
                if(down)
                    currentState = MainMenuStates.Exit;
				bool enter = Input.GetKeyUp (KeyCode.Return) || Input.GetKeyUp (KeyCode.KeypadEnter);
				if (enter)
					Application.LoadLevel("Settings");
        }
        void Settings_ExitState () {
            settingsStyle.normal.textColor = this.settingsSaveColor;
            Debug.Log ("Exiting Settings State");
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
            Debug.Log ("Entering Exit State");
        }
        void Exit_Update() {
                bool up = Input.GetKeyUp(KeyCode.UpArrow);
                if(up)
                    currentState = MainMenuStates.Settings;
                bool down = Input.GetKeyUp(KeyCode.DownArrow);
                if(down)
                    currentState = MainMenuStates.NewGame;
        }
        void Exit_ExitState () {
            exitStyle.normal.textColor = this.exitSaveColor;
            Debug.Log ("Exiting Exit State");
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

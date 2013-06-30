using UnityEngine;
using System.Collections;

public class MenuPaused : BaseMenuWithSettings
{

   public enum MenuPausedStates {
	    NewGame,
	    LoadGame,
	    Settings,
	    Exit,
		Hidden
	}
	
	public void Show() {
		this.currentState = MenuPausedStates.NewGame;
	}
	public void Hide() {
		this.currentState = MenuPausedStates.Hidden;
	}
	
	
	void Start() {
		base.init();	
		
		
		base.Settings_UpHandler = () => currentState = MenuPausedStates.LoadGame;
		base.Settings_DownHandler = () => currentState = MenuPausedStates.Exit;
	}

	

#region New Game
        
    public Rect newGameRect = new Rect(0,40,200,30);
    
    public string newGameString = "Continue";
    public GUIStyle newGameStyle;
	
    private Color newGameSaveColor;
    // Use this for State initialization, IEnumerator or void
    void NewGame_EnterState () {
       
        this.newGameSaveColor = newGameStyle.normal.textColor;
        this.newGameStyle.normal.textColor = Color.red;
    }
    
    
    
    void NewGame_Update() {
        if(this.pressedUp())
            currentState = MenuPausedStates.Exit;
            
        if(this.pressedDown())
            currentState = MenuPausedStates.LoadGame;
		if (this.pressedEnter())
		{
			master.theGameJustStarted();
		}

            
    }
    void NewGame_ExitState () {
        this.newGameStyle.normal.textColor = this.newGameSaveColor;
        
    }
	void NewGame_OnGUI() {
		this.drawMenu();
	}
	
    
    
#endregion	

	
#region LoadGame
    public Rect loadGameRect = new Rect(0,80,200,30);
    public string loadGameString = "Load Game";

    public GUIStyle loadGameStyle;
    private Color loadGameSaveColor;
    void LoadGame_EnterState () {
        loadGameSaveColor = loadGameStyle.normal.textColor;
        loadGameStyle.normal.textColor = Color.green;
        
    }
    void LoadGame_Update() {

        if(this.pressedUp())
            currentState = MenuPausedStates.NewGame;
        if(this.pressedDown())
            this.SetState(MenuPausedStates.Settings, this);

            
    }
    void LoadGame_ExitState () {
        loadGameStyle.normal.textColor = this.loadGameSaveColor;
        
    }
	
	void LoadGame_OnGUI() {
			drawMenu();
	}

#endregion

#region Settings
	protected override void Settings_OnGUI ()
	{
		this.drawMenu();
	}
    #endregion
    #region Exit
        public Rect exitRect = new Rect(0,160,200,30);
        public string exitString = "Exit Game";
    
        public GUIStyle exitStyle;
        private Color exitSaveColor;
        void Exit_EnterState () {
            exitSaveColor = exitStyle.normal.textColor;
            exitStyle.normal.textColor = Color.blue;
            
        }
        void Exit_Update() {
                if(this.pressedUp())
                    currentState = MenuPausedStates.Settings;
                if(this.pressedDown())
                    currentState = MenuPausedStates.NewGame;
				if (this.pressedEnter())
					master.Quit();
        }
        void Exit_ExitState () {
            exitStyle.normal.textColor = this.exitSaveColor;
            
        }
	void Exit_OnGUI() {
		drawMenu();
	}
    #endregion
    void updateRects ()
    {
        this.newGameRect.x = Screen.width/2 - this.newGameRect.width/2;
        this.loadGameRect.x = Screen.width/2 - this.loadGameRect.width/2;
        this.settingsRect.x = Screen.width/2 - this.settingsRect.width/2;
        this.exitRect.x = Screen.width/2 - this.exitRect.width/2;
    }
	
	void drawMenu(){
		this.updateRects();
        base.drawSettings();
        GUI.Box (this.newGameRect, this.newGameString, this.newGameStyle    );
        GUI.Box (this.loadGameRect, this.loadGameString, this.loadGameStyle    );
        GUI.Box (this.exitRect, this.exitString, this.exitStyle    );
	}
    
}


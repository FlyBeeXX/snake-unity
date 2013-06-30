using System;
using UnityEngine;
using System.Collections.Generic;


public abstract class BaseMenuWithSettings :  StateMachineBehaviourEx {



	protected Master master;


    public enum BaseMenuStates {
        Settings,
        Hidden
    }



    // Use this for initialization
    protected void init () {
		this.master = GameObject.FindGameObjectWithTag("GameController").GetComponent<Master>();
        this._settings = this.gameObject.GetComponent<Settings>();

        this.useGUI = true;
        currentState = BaseMenuStates.Hidden;
    }



#region Input Detectors

	protected bool pressedEnter()
	{
		return Input.GetKeyUp (KeyCode.Return) || Input.GetKeyUp (KeyCode.KeypadEnter);
	}
	protected bool pressedDown()
	{
		return Input.GetKeyUp(KeyCode.DownArrow);
	}
    protected bool pressedUp()
    {
        return Input.GetKeyUp(KeyCode.UpArrow);
    }
    protected bool pressedLeft()
    {
        return Input.GetKeyUp(KeyCode.LeftArrow);
    }
	protected bool pressedRight()
	{
		return Input.GetKeyUp(KeyCode.RightArrow);
	}
#endregion

#region Settings
    public Rect settingsRect = new Rect(0,120,200,30);
    public string settingsString = "Settings";
	private Settings _settings = null;
    public GUIStyle settingsStyle;
    private Color settingsSaveColor;
    protected void Settings_EnterState () {
        settingsSaveColor = settingsStyle.normal.textColor;
        settingsStyle.normal.textColor = Color.blue;

    }
    protected void Settings_Update() {
            if(this.pressedUp())
                this.Settings_UpHandler();
            if(this.pressedDown())
               this.Settings_DownHandler();
            if (this.pressedEnter())
                this.Settings_EnterHandler();
            if (this.pressedRight())
                this.Settings_RightHandler();
			if (this.pressedLeft())
                this.Settings_LeftHandler();
    }
    protected void Settings_ExitState () {
		this._settings.enabled = false;
        settingsStyle.normal.textColor = this.settingsSaveColor;

    }
    private void Settings_EnterHandler(){
        this.SetState(Settings.SettingsStates.Exit, _settings);
    }
	protected abstract void Settings_OnGUI();
	protected Action Settings_UpHandler = DoNothing;
	protected Action Settings_DownHandler = DoNothing;
	protected Action Settings_LeftHandler = DoNothing;
	protected Action Settings_RightHandler = DoNothing;
	
	protected static void DoNothing(){}

    protected void drawSettings(){
        GUI.Box (this.settingsRect, this.settingsString, this.settingsStyle    );
    }
#endregion
#region Hidden
//	void Hidden_EnterState () {
//
//	}
//	void Hidden_Update() {
//
//	}
//	void Hidden_ExitState () {
//	}
#endregion




}

using UnityEngine;
using System.Collections;

public class SnakeMover2 : StateMachineBehaviourEx
{
	public int speed = 100;
	public float rotationspeed = 50;

	private Transform _transform;
	private Camera _mainCamera;
	private Master _master;
	private Configuration _config;
	
	public bool debug = true;
	
	public float movingTolerance = 0.06f;

	public enum SnakeMover2States {
		Paused,
		MovingLeft,
		MovingRight,
		MovingUp,
		MovingDown,
		RightToUp,
		RightToDown,
		UpToRight,
		UpToLeft,
		LeftToUp,
		LeftToDown,
		DownToLeft,
		DownToRight
	}

	public void StartMoving() {
		this.currentState = SnakeMover2States.MovingRight;
	}

	// Use this for initialization
	void Start ()
	{

		this.currentState = SnakeMover2States.Paused;
		this._config = GameObject.FindGameObjectWithTag("GameController").GetComponent<Configuration>();
		this._master = GameObject.FindGameObjectWithTag("GameController").GetComponent<Master>();
		this._transform = gameObject.transform;
		this._mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		this._master.snakeLoaded();
		this._transform.position = new Vector3(-2*_config.GridSize,_transform.position.y, _config.GridSize);
	}
#region Input
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

	private bool AtTurningPosition() {
		float x = Mathf.Abs(_transform.position.x)%this._config.GridSize;
		float z = Mathf.Abs(_transform.position.z)%this._config.GridSize;
		
		float lower = this._config.GridSize-movingTolerance;
		float upper = movingTolerance;
		return  (x > lower  || x < upper)
			&& (z > lower || z < upper);
	}
	private SnakeMover2States nextState;
#region MovingRight



	void MovingRight_EnterState () {
		if (debug)
			Debug.Log("Entering MovingRight State");
		_transform.forward = Vector3.right;
		Debug.Log("Right: " +_transform.position);
		nextState = SnakeMover2States.MovingRight;
	}
	void MovingRight_Update() {
		this.move();
		
		if (pressedUp())
		{
			nextState = SnakeMover2States.RightToUp;
		}
		if (pressedDown())
			nextState = SnakeMover2States.RightToDown;


	}
	
	void MovingRight_FixedUpdate() {
		if (nextState != SnakeMover2States.MovingRight && this.AtTurningPosition())
			this.currentState = nextState;
	}
	void MovingRight_ExitState () {
		if (debug)
			Debug.Log("Exiting MovingRightState");
		_transform.position = new Vector3(Mathf.Round(_transform.position.x), _transform.position.y,Mathf.Round(_transform.position.z));
	}
#endregion
#region RightToUp
	void RightToUp_EnterState () {
		if (debug)
			Debug.Log("RightToUp");

		_transform.forward = Vector3.forward;
	}
	void RightToUp_Update() {
		this.nextState =  SnakeMover2States.MovingUp;
	}
	void RightToUp_ExitState () {
	}
	void RightToUp_FixedUpdate() {
		if (nextState != SnakeMover2States.RightToUp && this.AtTurningPosition())
			this.currentState = nextState;
	}
#endregion
#region RightToDown
	void RightToDown_EnterState () {
		if (debug)
			Debug.Log("RightToDown");

		_transform.forward = Vector3.back;
	}
	void RightToDown_Update() {
		this.nextState =  SnakeMover2States.MovingDown;
	}
	void RightToDown_ExitState () {
	}
	void RightToDown_FixedUpdate() {
		if (nextState != SnakeMover2States.RightToDown && this.AtTurningPosition())
			this.currentState = nextState;
	}
#endregion
#region MovingLeft
	void MovingLeft_EnterState () {
		Debug.Log("Left: " +_transform.position);
		if (debug)
			Debug.Log ("Entering MovingLeft State");
	}
	void MovingLeft_Update() {
		this.move();
		if (pressedUp())
			nextState = SnakeMover2States.LeftToUp;
		if (pressedDown())
			nextState = SnakeMover2States.LeftToDown;

	}
	void MovingLeft_ExitState () {
		if (debug)
			Debug.Log ("Exiting MovingLeft State");
		_transform.position = new Vector3(Mathf.Round(_transform.position.x), _transform.position.y,Mathf.Round(_transform.position.z));
	}
	void MovingLeft_FixedUpdate() {
		if (nextState != SnakeMover2States.MovingLeft && this.AtTurningPosition())
			this.currentState = nextState;
	}
#endregion
#region LeftToUp
	void LeftToUp_EnterState () {
		if (debug)
			Debug.Log("LeftToUp");

		_transform.forward = Vector3.forward;
//		Debug.Log("New Foward: " +_transform.forward);
	}
	void LeftToUp_Update() {
		this.nextState =  SnakeMover2States.MovingUp;
	}
	void LeftToUp_ExitState () {
	}
	void LeftToUp_FixedUpdate() {
		if (nextState != SnakeMover2States.LeftToUp && this.AtTurningPosition())
			this.currentState = nextState;
	}
#endregion
#region LeftToDown
	void LeftToDown_EnterState () {
		if (debug)
			Debug.Log("LeftToDown");

		_transform.forward = Vector3.back;
	}
	void LeftToDown_Update() {
		this.nextState =  SnakeMover2States.MovingDown;
	}
	void LeftToDown_ExitState () {
	}
	void LeftToDown_FixedUpdate() {
		if (nextState != SnakeMover2States.LeftToDown && this.AtTurningPosition())
			this.currentState = nextState;
	}
#endregion

#region MovingUp
	void MovingUp_EnterState () {
		Debug.Log("Up: " +_transform.position);
		if (debug)
			Debug.Log("Entering MovingUp State");
		
		
	}
	void MovingUp_Update() {
		this.move();
		
		if (pressedLeft())
			this.nextState = SnakeMover2States.UpToLeft;
		if (pressedRight())
			this.nextState = SnakeMover2States.UpToRight;


	}
	void MovingUp_ExitState () {
		if (debug)
			Debug.Log("Exiting MovingUp State");
		_transform.position = new Vector3(Mathf.Round(_transform.position.x), _transform.position.y,Mathf.Round(_transform.position.z));
	}
	void MovingUp_FixedUpdate() {
		if (nextState != SnakeMover2States.MovingUp && this.AtTurningPosition())
			this.currentState = nextState;
	}
#endregion
#region UpToLeft
void UpToLeft_EnterState () {
//		StartCoroutine(WaitSomeTime());
		
		if (debug)
			Debug.Log("UpToLeft");
		
		
			_transform.forward = Vector3.left;
	
}
void UpToLeft_Update() {
		
		this.nextState =  SnakeMover2States.MovingLeft;
	
}
void UpToLeft_ExitState () {
}
	void UpToLeft_FixedUpdate() {
		if (nextState != SnakeMover2States.UpToLeft && this.AtTurningPosition())
			this.currentState = nextState;
	}
#endregion
#region UpToRight
	void UpToRight_EnterState () {
		if (debug)
			Debug.Log("UpToRight");

		_transform.forward = Vector3.right;
	}
	void UpToRight_Update() {
		this.nextState =  SnakeMover2States.MovingRight;
	}
	void UpToRight_ExitState () {
	}
	void UpToRight_FixedUpdate() {
		if (nextState != SnakeMover2States.UpToRight && this.AtTurningPosition())
			this.currentState = nextState;
	}
#endregion
#region MovingDown
	void MovingDown_EnterState () {
		Debug.Log("Down: " +_transform.position);
		if (debug)
			Debug.Log ("Entering MovingDown State");
//		nextState = SnakeMover2States.MovingDown;
	}
	void MovingDown_Update() {
		this.move();
		if (pressedLeft())
			this.nextState = SnakeMover2States.DownToLeft;
		if (pressedRight())
			this.nextState = SnakeMover2States.DownToRight;

	}
	void MovingDown_ExitState () {
		if (debug)
			Debug.Log ("Exiting MovingDown State");
		_transform.position = new Vector3(Mathf.Round(_transform.position.x), _transform.position.y,Mathf.Round(_transform.position.z));
	}	
	void MovingDown_FixedUpdate() {
		if (nextState != SnakeMover2States.MovingDown && this.AtTurningPosition())
			this.currentState = nextState;
	}
#endregion
#region DownToLeft
void DownToLeft_EnterState () {
		if (debug)
			Debug.Log("DownToLeft");

	_transform.forward = Vector3.left;
//		Debug.Log("New Foward: " +_transform.forward);
}
void DownToLeft_Update() {
	this.nextState =  SnakeMover2States.MovingLeft;
}
void DownToLeft_ExitState () {
}
	void DownToLeft_FixedUpdate() {
		if (nextState != SnakeMover2States.DownToLeft && this.AtTurningPosition())
			this.currentState = nextState;
	}
#endregion
#region DownToRight
	void DownToRight_EnterState () {
		if (debug)
			Debug.Log("DownToRight");

		_transform.forward = Vector3.right;
	}
	void DownToRight_Update() {
		nextState = SnakeMover2States.MovingRight;
	}
	void DownToRight_ExitState () {
	}
	void DownToRight_FixedUpdate() {
		if (nextState != SnakeMover2States.DownToRight && this.AtTurningPosition())
			this.currentState = nextState;
	}

#endregion


	void move()
	{
		Vector3 newpos = _transform.position+ _transform.forward*Time.deltaTime*this.speed;
		_transform.position = newpos;
	}

}


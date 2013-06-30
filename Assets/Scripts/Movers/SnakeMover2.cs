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


#region MovingRight



	void MovingRight_EnterState () {
		_transform.forward = Vector3.right;
//		Debug.Log("Foward: "+_transform.forward);
	}
	void MovingRight_Update() {
		this.move();
		if (pressedUp())
			currentState = SnakeMover2States.RightToUp;
		if (pressedDown())
			currentState = SnakeMover2States.RightToDown;
	}
	void MovingRight_ExitState () {
	}
#endregion
#region RightToUp
	void RightToUp_EnterState () {

		_transform.forward = Vector3.forward;
	}
	void RightToUp_Update() {
		this.currentState =  SnakeMover2States.MovingUp;
	}
	void RightToUp_ExitState () {
	}
#endregion
#region RightToDown
	void RightToDown_EnterState () {

		_transform.forward = Vector3.back;
	}
	void RightToDown_Update() {
		this.currentState =  SnakeMover2States.MovingDown;
	}
	void RightToDown_ExitState () {
	}
#endregion
#region MovingLeft
	void MovingLeft_EnterState () {

		Debug.Log ("Entering MovingLeft State");
	}
	void MovingLeft_Update() {
		this.move();
		if (pressedUp())
			currentState = SnakeMover2States.LeftToUp;
		if (pressedDown())
			currentState = SnakeMover2States.LeftToDown;

	}
	void MovingLeft_ExitState () {
		Debug.Log ("Exiting MovingLeft State");
	}
#endregion
#region LeftToUp
	void LeftToUp_EnterState () {

		_transform.forward = Vector3.forward;
//		Debug.Log("New Foward: " +_transform.forward);
	}
	void LeftToUp_Update() {
		this.currentState =  SnakeMover2States.MovingUp;
	}
	void LeftToUp_ExitState () {
	}
#endregion
#region LeftToDown
	void LeftToDown_EnterState () {

		_transform.forward = Vector3.back;
	}
	void LeftToDown_Update() {
		this.currentState =  SnakeMover2States.MovingDown;
	}
	void LeftToDown_ExitState () {
	}
#endregion
#region MovingUp
	void MovingUp_EnterState () {
		Debug.Log("MovingUp");
	}
	void MovingUp_Update() {
		this.move();
		if (pressedLeft())
			this.currentState = SnakeMover2States.UpToLeft;
		if (pressedRight())
			this.currentState = SnakeMover2States.UpToRight;


	}
	void MovingUp_ExitState () {
	}
#endregion
#region UpToLeft
void UpToLeft_EnterState () {

	_transform.forward = Vector3.left;
//		Debug.Log("New Foward: " +_transform.forward);
}
void UpToLeft_Update() {
	this.currentState =  SnakeMover2States.MovingLeft;
}
void UpToLeft_ExitState () {
}
#endregion
#region UpToRight
	void UpToRight_EnterState () {

		_transform.forward = Vector3.right;
	}
	void UpToRight_Update() {
		this.currentState =  SnakeMover2States.MovingRight;
	}
	void UpToRight_ExitState () {
	}
#endregion
#region MovingDown
	void MovingDown_EnterState () {

		Debug.Log ("Entering MovingDown State");
	}
	void MovingDown_Update() {
		this.move();
		if (pressedLeft())
			this.currentState = SnakeMover2States.DownToLeft;
		if (pressedRight())
			this.currentState = SnakeMover2States.DownToRight;

	}
	void MovingDown_ExitState () {
		Debug.Log ("Exiting MovingDown State");
	}
#endregion
#region DownToLeft
void DownToLeft_EnterState () {

	_transform.forward = Vector3.left;
//		Debug.Log("New Foward: " +_transform.forward);
}
void DownToLeft_Update() {
	this.currentState =  SnakeMover2States.MovingLeft;
}
void DownToLeft_ExitState () {
}
#endregion
#region DownToRight
	void DownToRight_EnterState () {

		_transform.forward = Vector3.right;
	}
	void DownToRight_Update() {
		this.currentState =  SnakeMover2States.MovingRight;
	}
	void DownToRight_ExitState () {
	}
#endregion


	void move()
	{
		Vector3 newpos = _transform.position+ _transform.forward*Time.deltaTime*this.speed;
		_transform.position = newpos;
	}

}


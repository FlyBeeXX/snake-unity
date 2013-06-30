using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SnakeMover : StateMachineBehaviourEx {
	public int speed = 100;
	public float rotationspeed = 50;
	
	private Transform _transform;
	private Camera _mainCamera;
	private Master _master;
	
	public enum SnakeMoverStates {
		Moving,
		Stopped
	}
	
	private Configuration _config;
	
	public Direction direction = Direction.Right;
	// Use this for initialization
	void Start () {
		this.currentState = SnakeMoverStates.Moving;
		this._config = GameObject.FindGameObjectWithTag("GameController").GetComponent<Configuration>();
		this._master = GameObject.FindGameObjectWithTag("GameController").GetComponent<Master>();
		this._transform = gameObject.transform;
		this._mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	private bool canChangeDirection(Vector3 screenpos)
	{
		bool val = Mathf.Abs(screenpos.x)%1 < 0.01f || Mathf.Abs(screenpos.x)%1 > 0.99f 
			&& Mathf.Abs(screenpos.z)%1 < 0.01f || Mathf.Abs(screenpos.z)%1 > 0.99f;
//		Debug.Log("X % 1 : "+(Mathf.Abs(screenpos.x)%1)+" Y: " +(Mathf.Abs(screenpos.z)%1)+ " ===> " + val);
		return val;
//		return (  Math.Abs(previousTarget.x-screenpos.x) > this._config.GridSize-Mathf.Epsilon
//				|| Math.Abs(previousTarget.x-screenpos.x) < Mathf.Epsilon) 
//			&& (	Math.Abs(previousTarget.y-screenpos.y) > this._config.GridSize -Mathf.Epsilon
//				|| Math.Abs(previousTarget.y-screenpos.y) < Mathf.Epsilon);
	}
	
	private bool needNewTarget(Vector3 position)
	{
	bool val = Mathf.Abs(Mathf.Abs(position.x)-Mathf.Abs(currentTarget.x)) < 0.01f ||Mathf.Abs(Mathf.Abs(position.x)-Mathf.Abs(currentTarget.x)) > 0.99f 
			&& Mathf.Abs(Mathf.Abs(position.z)-Mathf.Abs(currentTarget.z)) < 0.01f || Mathf.Abs(Mathf.Abs(position.z)-Mathf.Abs(currentTarget.z)) > 0.99f;
//		Debug.Log ("Need new target: " + val);
		return val;
	}
	
	
	private Vector3 currentTarget = Vector3.zero;
	private Vector3 previousTarget = Vector3.zero;
	
	
	private List<Vector3> waypoints = new List<Vector3>();
	private float start = 0;
	private float steplength = 2;
	private Vector3 nextStep()
	{
		
		Vector3 screenpos = _transform.position;//_mainCamera.WorldToScreenPoint(_transform.position);
		if (previousTarget == Vector3.zero)
			previousTarget = _transform.position;
		if (currentTarget == Vector3.zero)
			currentTarget = previousTarget;
		
		if (needNewTarget(_transform.position)) {
			
			Vector3 newTarget = currentTarget;
			previousTarget = currentTarget;
			switch (this.direction) {
			case Direction.Down:
				newTarget.z -= this._config.GridSize;
				screenpos.z = (int)screenpos.z;
				break;
			case Direction.Left:
				newTarget.x -= this._config.GridSize;
				screenpos.x = (int)screenpos.x%1;
				break;
			case Direction.Up:
				newTarget.z += this._config.GridSize;
				screenpos.z = (int)screenpos.z%1;
				break;
			case Direction.Right:
				newTarget.x += this._config.GridSize;
				screenpos.x = (int)screenpos.x%1;
				break;
				
			default:
			break;
			}
			start = Time.time;
			currentTarget = newTarget;
			Debug.Log ("currentTarget: " + currentTarget+"previousTarget:" +previousTarget+" pos: " + _transform.position);
			//		Debug.Log (previousTarget+ " - " +screenpos+" - canchange:  "+this.canChangeDirection(screenpos));
		}
		return currentTarget;//.ScreenToWorldPoint(previousTarget);
	}
	
	private void changeDirection()
	{
		if (!this._master.isPaused()) {
			if (Input.GetKeyUp(KeyCode.DownArrow))
				this.direction = Direction.Down;
			if (Input.GetKeyUp(KeyCode.LeftArrow))
				this.direction = Direction.Left;
			if (Input.GetKeyUp(KeyCode.UpArrow))
				this.direction = Direction.Up;
			if (Input.GetKeyUp(KeyCode.RightArrow))
				this.direction = Direction.Right;
		}
	}
	
	
	private Vector3 newPos(Vector3 current, Vector3 target)
	{
		float distanceCovered = (Time.time - this.start+0.1f)*speed;
		float distanceRation = distanceCovered/_config.GridSize;
		Debug.Log ("distanceRatio: " + distanceRation);
		
		
		return Vector3.Lerp(current, target, distanceRation);
	}
	
	// Update is called once per frame
	
	void Moving_EnterState() {
		// Add last point and next 2 points to the waypoints
		this.waypoints.Add(new Vector3(0,1,0));
		this.waypoints.Add(new Vector3(1,1,0));
		this.waypoints.Add(new Vector3(2,1,0));
		
		
	}
	void Moving_Update () {
		this.changeDirection();


		_transform.position = this.newPos(_transform.position,this.nextStep());
	}
	private bool atWall=false;
	void Moving_OnGUI() {
		string test = "Moving";
		if (this.atWall)
			test = "At the Wall";
		GUI.Box (new Rect(0,0,200,40), test);
	}
	void Moving_OnTriggerEnter(Collider collider) 
	{
		if (collider.name == "Wall")
			this.atWall = true;
	}
		
	void Moving_OnTriggerExit(Collider collider) 
	{	
		if (collider.name == "Wall")
			this.atWall = false;
	}
	
	public enum Direction {
		Left,
		Right,
		Up,
		Down
	}
}

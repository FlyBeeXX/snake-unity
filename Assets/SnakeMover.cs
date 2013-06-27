using UnityEngine;
using System.Collections;
using System;

public class SnakeMover : MonoBehaviour {
	public float speed = 1f;
	
	private Transform _transform;
	private Camera _mainCamera;
	private Master _master;
	
	private Configuration _config;
	
	public Direction direction = Direction.Right;
	// Use this for initialization
	void Start () {
		this._config = GameObject.FindGameObjectWithTag("GameController").GetComponent<Configuration>();
		this._master = GameObject.FindGameObjectWithTag("GameController").GetComponent<Master>();
		this._transform = gameObject.transform;
		this._mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	private bool canChangeDirection(Vector3 screenpos)
	{
		return Vector3.Distance(screenpos, previousTarget) < 3;
//		return (  Math.Abs(previousTarget.x-screenpos.x) > this._config.GridSize-Mathf.Epsilon
//				|| Math.Abs(previousTarget.x-screenpos.x) < Mathf.Epsilon) 
//			&& (	Math.Abs(previousTarget.y-screenpos.y) > this._config.GridSize -Mathf.Epsilon
//				|| Math.Abs(previousTarget.y-screenpos.y) < Mathf.Epsilon);
	}
	
	private Vector3 previousTarget = Vector3.zero;
	private Vector3 nextStep()
	{
		
		Vector3 screenpos = _mainCamera.WorldToScreenPoint(_transform.position);
		Debug.Log (previousTarget+ " - " +screenpos);
		if (previousTarget == Vector3.zero || this.canChangeDirection(screenpos)) {
			switch (this.direction) {
			case Direction.Down:
				screenpos.y -= this._config.GridSize;
				break;
			case Direction.Left:
				screenpos.x -= this._config.GridSize;
				break;
			case Direction.Up:
				screenpos.y += this._config.GridSize;
				break;
			case Direction.Right:
				screenpos.x += this._config.GridSize;
				break;
				
			default:
			break;
			}
			previousTarget = screenpos;
		}
		return _mainCamera.ScreenToWorldPoint(previousTarget);
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
	
	// Update is called once per frame
	void Update () {
			this.changeDirection();


		_transform.position = Vector3.Lerp(_transform.position,this.nextStep(), speed*Time.deltaTime);
	}
	
	
	public enum Direction {
		Left,
		Right,
		Up,
		Down
	}
}

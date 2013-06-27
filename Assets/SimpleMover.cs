using UnityEngine;
using System.Collections;

public class SimpleMover : MonoBehaviour {
	
	private Transform _transform;
	private Camera _mainCamera;
	
	public int speed = 50;
	
	bool reverse = false;
	

	// Use this for initialization
	void Start () {
		this._transform = gameObject.transform;
		this._mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenpos = _mainCamera.WorldToScreenPoint(_transform.position);
//		if (screenpos.x > Screen.width-_transform
		if (screenpos.x >= Screen.width-100 || screenpos.x <= 100)
			reverse = !reverse;
		
		screenpos.x += speed*(reverse ? -1 : 1);
		_transform.position = Vector3.Lerp(_transform.position,_mainCamera.ScreenToWorldPoint(screenpos), Time.deltaTime);
	}
}

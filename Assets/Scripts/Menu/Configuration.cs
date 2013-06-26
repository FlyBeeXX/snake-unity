using UnityEngine;
using System;
using System.Collections.Generic;

public class Configuration : MonoBehaviour {
	
	public static Configuration Instance;
	public Dictionary<string, object> config = new Dictionary<string, object>();

	// Use this for initialization
    void Awake()
  	{
        if(Configuration.Instance)
            DestroyImmediate(this.gameObject);
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
	}
	
	

}

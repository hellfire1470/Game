using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadExecution : MonoBehaviour {



	private static List<Action> _actions = new List<Action>();

	public static void OnMainThread(Action action){
		lock (_actions) {
			_actions.Add (action);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lock (_actions) {
			foreach (Action action in _actions) {
				action ();
			}
			_actions.Clear ();
		}
	}
}

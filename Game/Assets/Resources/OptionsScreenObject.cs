using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreenObject : MonoBehaviour {

	private Button _buttonBack;

	// Use this for initialization
	void Start () {
		_buttonBack = GetComponentInChildren<Button> ();
		_buttonBack.onClick.AddListener (delegate {
			GameObject.Find ("MenuContainer").GetComponent<Animator>().SetTrigger("LoginScreen");
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

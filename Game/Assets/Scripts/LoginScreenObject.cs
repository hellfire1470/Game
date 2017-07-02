using System.Collections;
using System.Collections.Generic;
using FileManager;
using Network;
using Network.Packages;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreenObject : MonoBehaviour {

	private Button _buttonLogin;
	private Button _buttonOptions;

	private Toggle _toggleKeepUsername;

	private InputField _inputUser;
	private InputField _inputPass;

	// Use this for initialization
	void Start () {
		_inputUser = GetComponentsInChildren<InputField> () [0];
		_inputPass = GetComponentsInChildren<InputField> () [1];

		_toggleKeepUsername = GetComponentInChildren<Toggle> ();
		_toggleKeepUsername.onValueChanged.AddListener (delegate {
			
			if(!_toggleKeepUsername.isOn){
				Global.ConfigurationFile.RemoveKey("loginUsername");
			}

			Global.ConfigurationFile.SetValue("keepUsername", _toggleKeepUsername.isOn ? "1" : "0");
			Global.ConfigurationFile.Apply();
		});

		if (Global.ConfigurationFile.GetValue ("keepUsername") == "1") {
			_toggleKeepUsername.isOn = true;
			_toggleKeepUsername.GraphicUpdateComplete ();
			_inputUser.text = Global.ConfigurationFile.GetValue ("loginUsername");
		} else {
			_toggleKeepUsername.isOn = false;
		}

		_buttonLogin = GetComponentsInChildren<Button> ()[0];
		_buttonLogin.onClick.AddListener (delegate {
			if(_inputUser.text == "" || _inputPass.text == "") return;
			if(_toggleKeepUsername.isOn){
				Global.ConfigurationFile.SetValue("loginUsername", _inputUser.text);
				Global.ConfigurationFile.Apply();
			}
			Global.Client.SendBytes(NetworkHelper.Serialize(new LoginRequest(){Username = _inputUser.text, Password = _inputPass.text}));
		});

		_buttonOptions = GetComponentsInChildren<Button> ()[1];
		_buttonOptions.onClick.AddListener (delegate {
			GameObject.Find ("MenuContainer").GetComponent<Animator>().SetTrigger("Options");
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

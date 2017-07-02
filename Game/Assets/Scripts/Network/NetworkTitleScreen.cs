using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;
using Network.Packages;

public class NetworkTitleScreen: MonoBehaviour {

	void Start(){
		Global.Client.DataReceived += DataReceived;
	}

	void Disable(){
		Global.Client.DataReceived -= DataReceived;
	}

	public void DataReceived(object sender, NetworkReceiveEventArgs e){
		Response response;
		switch (NetworkHelper.GetPackageType (e.Data)) {
		case PackageType.Login:
			response = NetworkHelper.Deserialize<LoginResponse> (e.Data);
			if (response.Success) {
				MainThreadExecution.OnMainThread (delegate {
					GameObject.Find ("MenuContainer").GetComponent<Animator> ().SetTrigger ("CharacterSelection");
				});
			} else {
				MainThreadExecution.OnMainThread (delegate {
					GameObject.Find ("LoginScreen").GetComponent<Animator> ().SetTrigger ("LoginFail");
				});
			}
			break;
		}
	}
}

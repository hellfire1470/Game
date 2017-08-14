using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;
using Network.Packages;

public class NetworkTitleScreen : MonoBehaviour
{

    void Start()
    {
        Global.Client.DataReceived += DataReceived;
    }

    void Disable()
    {
        Global.Client.DataReceived -= DataReceived;
    }

    public void DataReceived(object sender, NetworkReceiveEventArgs e)
    {
        Response response;
        switch (NetworkHelper.GetPackageType(e.Data))
        {
            case PackageType.Login:
                response = NetworkHelper.Deserialize<LoginResponse>(e.Data);
                if (response.Success)
                {
                    MainThreadExecution.OnMainThread(delegate
                    {
                        Camera.main.GetComponent<CameraAutoNavigation>().SetTarget(GameObject.Find("CharacterSelectionCameraPoint"));
                    });
                }
                else
                {
                    MainThreadExecution.OnMainThread(delegate
                    {
                        GameObject.Find("LoginScreen").GetComponent<Animator>().SetTrigger("LoginFail");
                        FindObjectOfType<LoginScreenObject>().ShowError((response as LoginResponse).Error.ToString());
                    });
                }
                break;

            case PackageType.CreateCharacter:
                response = NetworkHelper.Deserialize<CreateCharacterResponse>(e.Data);
                if(response.Success)
                {
                    Debug.Log("Character Created");
                }
                else 
                {
                    MainThreadExecution.OnMainThread(delegate
                    {
                        switch (((CreateCharacterResponse)response).Error)
                        {
                            case GameData.ErrorResult.NameExists:
                                FindObjectOfType<CharacterCreationScreenObject>().ShowError("Name already exists");
                                break;
                            case GameData.ErrorResult.InvalidName:
                                FindObjectOfType<CharacterCreationScreenObject>().ShowError("Invalid Name");
                                break;
                            case GameData.ErrorResult.CharacterLimit:
                                FindObjectOfType<CharacterCreationScreenObject>().ShowError("Character Limit Reached");
                                break;
                            case GameData.ErrorResult.UnknownError:
                                FindObjectOfType<CharacterCreationScreenObject>().ShowError("Unknown Error");
                                break;
                        }
                    });
                }
                break;
        }
    }
}

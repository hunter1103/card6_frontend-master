using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.SceneScripts;

public class PlayFabAuthenticator : MonoBehaviour
{
    public static PlayFabAuthenticator Instance;
    private string _playFabPlayerIdCache;
    public InputField usernameLoginText;
    public InputField passwordLoginText;
    public InputField usernameSignupText;
    public InputField emailSignupText;
    public InputField passwordSignupText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoginClick()
    {    
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest()
        {                        
            TitleId = PlayFabSettings.TitleId,
            Username = usernameLoginText.text,
            Password = passwordLoginText.text            
        }, RequestPhotonToken, OnPlayFabLoginError);        
    }

    public void SignupClick()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Username = usernameSignupText.text,
            Password = passwordSignupText.text,
            Email = emailSignupText.text
        }, RequestPhotonTokenRegister, OnPlayFabSignupError);       
    }

    private void AuthenticateWithPlayFab()
    {
        LogMessage("PlayFab authenticating using Custom ID...");
    }

    private void RequestPhotonToken(LoginResult obj)
    {
        LogMessage("PlayFab authenticated. Requesting photon token...");
        _playFabPlayerIdCache = obj.PlayFabId;

        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppID
        }, AuthenticateWithPhoton, OnPlayFabLoginError);
    }

    private void RequestPhotonTokenRegister(RegisterPlayFabUserResult obj)
    {
        LogMessage("PlayFab authenticated. Requesting photon token...");
        _playFabPlayerIdCache = obj.PlayFabId;

        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppID
        }, AuthenticateWithPhoton, OnPlayFabSignupError);
    }

    private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
    {
        LogMessage("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");
        var customAuth = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };
        customAuth.AddAuthParameter("username", _playFabPlayerIdCache);
        customAuth.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);
        PhotonNetwork.AuthValues = customAuth;
        LoginScene.Instance.LoginClick();        
    }

    private void OnPlayFabLoginError(PlayFabError obj)
    {
        LogMessage(obj.GenerateErrorReport());

        if (usernameLoginText.text != "" && passwordLoginText.text != "")
        {
            usernameLoginText.text = obj.ErrorMessage;
        }
        else
        {
            var sb = new System.Text.StringBuilder();

            foreach (var kv in obj.ErrorDetails)
            {
                sb.Append(string.Join(", ", kv.Value.ToArray()));
                sb.Append(" | ");
            }

            usernameLoginText.text = "" + sb;
        }            
    }

    private void OnPlayFabSignupError(PlayFabError obj)
    {
        LogMessage(obj.GenerateErrorReport());
        var sb = new System.Text.StringBuilder();

        foreach (var kv in obj.ErrorDetails)
        {
            sb.Append(kv.Key);
            sb.Append(": ");
            sb.Append(string.Join(", ", kv.Value.ToArray()));
            sb.Append(" | ");
        }

        usernameSignupText.text = "" + sb;        
    }

    public void LogMessage(string message)
    {
        Debug.Log("PlayFab + Photon Example: " + message);
    }
}
using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum AuthState
{
    NotAuthenticated,
    Authenticating,
    Authenticated,
    Error,
    TimeOut,
}

public class LoginCanvas : MonoBehaviour
{
    [Title("References")]
    [SerializeField] [Required] private Button _loginButton;
    
    private AuthState _authState = AuthState.NotAuthenticated;
    
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        
        if (PlayerPrefs.HasKey("AccessToken"))
        {
            Debug.Log("PlayerPrefs");
            string accessTokenPlayerPrefs = PlayerPrefs.GetString("AccessToken");
        
            await SignInWithUnityAsync(accessTokenPlayerPrefs);
        }

        _loginButton.onClick.AddListener(OnLoginClicked);
        
    }

    private void OnLoginClicked()
    {
        SignIn();
    }

    private async Task SignIn()
    {
        if (_authState == AuthState.Authenticated) return;

        if (_authState == AuthState.Authenticating)
        {
            PlayerAccountService.Instance.SignedIn -= SignedIn;
            PlayerAccountService.Instance.SignInFailed -= SignInFailed;
        }

        await SignInUnityAsync();
    }
    
    private async Task SignInUnityAsync()
    {
        _authState = AuthState.Authenticating;

        PlayerAccountService.Instance.SignedIn += SignedIn;

        PlayerAccountService.Instance.SignInFailed += SignInFailed;

        try
        {
            await PlayerAccountService.Instance.StartSignInAsync();
        }
        catch (PlayerAccountsException ex) {
            Debug.LogException(ex);
            _authState = AuthState.Error;
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            _authState = AuthState.Error;
        }
        
    }
    
    private void SignInFailed(RequestFailedException exception)
    {
        _authState = AuthState.Error;
        _loginButton.interactable = true;

        Debug.LogWarning($"SignIn failed: {exception.Message}");
        Debug.LogException(exception);
        
        PlayerAccountService.Instance.SignedIn -= SignedIn;
        PlayerAccountService.Instance.SignInFailed -= SignInFailed;
    }

    private async void SignedIn()
    {
        try
        {
            var accessToken = PlayerAccountService.Instance.AccessToken;
            await SignInWithUnityAsync(accessToken);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            _authState = AuthState.Error;
            _loginButton.interactable = true;
        }
        
        PlayerAccountService.Instance.SignedIn -= SignedIn;
        PlayerAccountService.Instance.SignInFailed -= SignInFailed;
    }
    
    private async Task SignInWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("SignIn successfull.");

            PlayerPrefs.SetString("AccessToken", accessToken);

            _authState = AuthState.Authenticated;
            _loginButton.interactable = false;
            await SceneManager.LoadSceneAsync(1);
            
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
}

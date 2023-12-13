using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;




public class SignIn_UpManager : MonoBehaviour
{
    public enum Sign
    {
        SignUP, SignIn
    }
    public Sign sign;

    public InputField IDinput, PasswordInput, ScdPsInput, Emailinput;
    [SerializeField]
    private TextMeshProUGUI errorTxt;
    //[SerializeField]
    //private EventBoxManager eventBox;
    [SerializeField]
    private NetworkManager ntM;
    
    public void SignInBtn()
    {
        var request = new LoginWithPlayFabRequest { Username = IDinput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);

    }
    public void SignUpBtn()
    {
        if(PasswordInput.text != ScdPsInput.text) { errorTxt.text = " 비밀번호가 같지 않습니다."; return; }
        else if(IDinput.text.Length < 6) { errorTxt.text = " 아이디 6자리 이상 적어주세요"; return; }
        //
        errorTxt.text = " ";
        var request = new RegisterPlayFabUserRequest { Username = IDinput.text, Password = PasswordInput.text, Email = Emailinput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        ntM.Connect();
        gameObject.SetActive(false);

    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("로그인 실패...");
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("가입 성공");
        // Instantiate(eventBox);
        // Destroy(gameObject);
        GetComponent<EventBoxManager>().CreateBox();
        Destroy(gameObject);
    }
    private void RegisterFailure(PlayFabError error)
    {
        errorTxt.text = "이메일 양식을 확인 부탁합니다.";
    }
}
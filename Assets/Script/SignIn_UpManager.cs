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
        if(PasswordInput.text != ScdPsInput.text) { errorTxt.text = " ��й�ȣ�� ���� �ʽ��ϴ�."; return; }
        else if(IDinput.text.Length < 6) { errorTxt.text = " ���̵� 6�ڸ� �̻� �����ּ���"; return; }
        //
        errorTxt.text = " ";
        var request = new RegisterPlayFabUserRequest { Username = IDinput.text, Password = PasswordInput.text, Email = Emailinput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("�α��� ����");
        ntM.Connect();
        gameObject.SetActive(false);

    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("�α��� ����...");
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("���� ����");
        // Instantiate(eventBox);
        // Destroy(gameObject);
        GetComponent<EventBoxManager>().CreateBox();
        Destroy(gameObject);
    }
    private void RegisterFailure(PlayFabError error)
    {
        errorTxt.text = "�̸��� ����� Ȯ�� ��Ź�մϴ�.";
    }
}
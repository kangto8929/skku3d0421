using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.SceneManagement;

[Serializable]
public class UI_InputFields
{
    public TextMeshProUGUI ResultText;  // 결과 텍스트
    public TMP_InputField IDInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordComfirmInputField;
    public Button ConfirmButton;   // 로그인 or 회원가입 버튼
}

public class UI_LoginScene : MonoBehaviour
{
    public GameObject Title;

    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject ResisterPanel;

    [Header("로그인")]
    public UI_InputFields LoginInputFields;

    [Header("회원가입")]
    public UI_InputFields RegisterInputFields;

    private const string PREFIX = "ID_";
    private const string SALT = "10043428";//사이트 비밀번호

    //DoTween 할 거
    public float duration = 0.5f;
    public float strength = 7f;


    // 게임 시작하면 로그인 켜주고 회원가입은 꺼주고..
    private void Start()
    {
        Title.SetActive(true);

        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);

        LoginInputFields.ResultText.text = string.Empty;
        RegisterInputFields.ResultText.text = string.Empty;

        LoginCheck();
    }

    // 회원가입하기 버튼 클릭
    public void OnClickGoToResisterButton()
    {
        LoginPanel.SetActive(false);
        ResisterPanel.SetActive(true);
    }

    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);
    }


    // 회원가입
    public void Resister()
    {
        // 1. 아이디 입력을 확인한다.
        string id = RegisterInputFields.IDInputField.text;
        string password = RegisterInputFields.PasswordInputField.text;
        string passwordCheck = RegisterInputFields.PasswordComfirmInputField.text;

        if (string.IsNullOrEmpty(id))
        {
            RegisterInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            RegisterInputFields.ResultText.text = "아이디를 입력해주세요.";
            return;
        }

        // 2. 1차 비밀번호 입력을 확인한다.
        //string password = RegisterInputFields.PasswordInputField.text;
        else if (string.IsNullOrEmpty(password))
        {
            RegisterInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            RegisterInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }

        // 3. 2차 비밀번호 입력을 확인하고, 1차 비밀번호 입력과 같은지 확인한다.
        else if (string.IsNullOrEmpty(passwordCheck))
        {
            RegisterInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            RegisterInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }

        else if(passwordCheck != password)
        {
            RegisterInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            RegisterInputFields.ResultText.text = "비밀번호가 일치하지 않습니다.";
            return;
        }


        // 4. PlayerPrefs를 이용해서 아이디와 비밀번호를 저장한다.
        PlayerPrefs.SetString(PREFIX + id, Encryption(password + SALT));//다른 값을 저장
        //PlayerPrefs.SetString("PassWord", password);

        // 5. 로그인 창으로 돌아간다. (이때 아이디는 자동 입력되어 있다.)

        OnClickGoToLoginButton();
        string loginId = LoginInputFields.IDInputField.text;
        LoginInputFields.IDInputField.text = id;

        Debug.Log("아이디 입력 및 비밀번호 일치 확인");
        Debug.Log("아이디 저장" + id);
        Debug.Log("비밀번호 저장" + password);

    }

    public string Encryption(string text)
    {
        // 해시 암호화 알고리즘 인스턴스를 생성한다.
        SHA256 sha256 = SHA256.Create();

        // 운영체제 혹은 프로그래밍 언어별로 string 표현하는 방식이 다 다르므로
        // UTF8 버전 바이트로 배열로 바꿔야한다.
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        byte[] hash = sha256.ComputeHash(bytes);

        string resultText = string.Empty;
        foreach (byte b in hash)
        {
            // byte를 다시 string으로 바꿔서 이어붙이기
            resultText += b.ToString("X2");
        }

        return resultText;
    }


    private void LogIn()
    {
        //1.아이디 입력을 확인한다

        string id = LoginInputFields.IDInputField.text;
        string password = LoginInputFields.PasswordInputField.text;

        if (string.IsNullOrEmpty(id))
        {
            LoginInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            LoginInputFields.ResultText.text = "아이디를 입력해주세요.";
            return;
        }
        //2. 비밀번호 입력을 확인한다
        else if (string.IsNullOrEmpty(password))
        {
            LoginInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            LoginInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }

        //3. PlayerPrefs.Get을 이용해서 아이디와 비밀번호가 맞는지 확인한다.
        if(!PlayerPrefs.HasKey(PREFIX + id))
        {
            LoginInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            LoginInputFields.ResultText.text = "아이디와 비밀번호를 확인해주세요.";
            return;
        }

        string hashedPassword = PlayerPrefs.GetString(key: PREFIX + id);
        if (hashedPassword != Encryption(text: password + SALT))
        {
            LoginInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            LoginInputFields.ResultText.text = "아이디와 비밀번호를 확인해주세요.";
            return;
        }

        //4. 맞다면 로그인
        Debug.Log("로그인 성공!");

        Title.SetActive(false);
        LoginPanel.SetActive(false);
        ResisterPanel.SetActive(false);
        SceneManager.LoadScene(1);

    }


    public void LoginCheck()
    {
        string id = LoginInputFields.IDInputField.text;
        string password = LoginInputFields.PasswordInputField.text;

        LoginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password);
    }

}
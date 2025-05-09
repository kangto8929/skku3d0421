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
    public TextMeshProUGUI ResultText;  // ��� �ؽ�Ʈ
    public TMP_InputField IDInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordComfirmInputField;
    public Button ConfirmButton;   // �α��� or ȸ������ ��ư
}

public class UI_LoginScene : MonoBehaviour
{
    public GameObject Title;

    [Header("�г�")]
    public GameObject LoginPanel;
    public GameObject ResisterPanel;

    [Header("�α���")]
    public UI_InputFields LoginInputFields;

    [Header("ȸ������")]
    public UI_InputFields RegisterInputFields;

    private const string PREFIX = "ID_";
    private const string SALT = "10043428";//����Ʈ ��й�ȣ

    //DoTween �� ��
    public float duration = 0.5f;
    public float strength = 7f;


    // ���� �����ϸ� �α��� ���ְ� ȸ�������� ���ְ�..
    private void Start()
    {
        Title.SetActive(true);

        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);

        LoginInputFields.ResultText.text = string.Empty;
        RegisterInputFields.ResultText.text = string.Empty;

        LoginCheck();
    }

    // ȸ�������ϱ� ��ư Ŭ��
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


    // ȸ������
    public void Resister()
    {
        // 1. ���̵� �Է��� Ȯ���Ѵ�.
        string id = RegisterInputFields.IDInputField.text;
        string password = RegisterInputFields.PasswordInputField.text;
        string passwordCheck = RegisterInputFields.PasswordComfirmInputField.text;

        if (string.IsNullOrEmpty(id))
        {
            RegisterInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            RegisterInputFields.ResultText.text = "���̵� �Է����ּ���.";
            return;
        }

        // 2. 1�� ��й�ȣ �Է��� Ȯ���Ѵ�.
        //string password = RegisterInputFields.PasswordInputField.text;
        else if (string.IsNullOrEmpty(password))
        {
            RegisterInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            RegisterInputFields.ResultText.text = "��й�ȣ�� �Է����ּ���.";
            return;
        }

        // 3. 2�� ��й�ȣ �Է��� Ȯ���ϰ�, 1�� ��й�ȣ �Է°� ������ Ȯ���Ѵ�.
        else if (string.IsNullOrEmpty(passwordCheck))
        {
            RegisterInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            RegisterInputFields.ResultText.text = "��й�ȣ�� �Է����ּ���.";
            return;
        }

        else if(passwordCheck != password)
        {
            RegisterInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            RegisterInputFields.ResultText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            return;
        }


        // 4. PlayerPrefs�� �̿��ؼ� ���̵�� ��й�ȣ�� �����Ѵ�.
        PlayerPrefs.SetString(PREFIX + id, Encryption(password + SALT));//�ٸ� ���� ����
        //PlayerPrefs.SetString("PassWord", password);

        // 5. �α��� â���� ���ư���. (�̶� ���̵�� �ڵ� �ԷµǾ� �ִ�.)

        OnClickGoToLoginButton();
        string loginId = LoginInputFields.IDInputField.text;
        LoginInputFields.IDInputField.text = id;

        Debug.Log("���̵� �Է� �� ��й�ȣ ��ġ Ȯ��");
        Debug.Log("���̵� ����" + id);
        Debug.Log("��й�ȣ ����" + password);

    }

    public string Encryption(string text)
    {
        // �ؽ� ��ȣȭ �˰��� �ν��Ͻ��� �����Ѵ�.
        SHA256 sha256 = SHA256.Create();

        // �ü�� Ȥ�� ���α׷��� ���� string ǥ���ϴ� ����� �� �ٸ��Ƿ�
        // UTF8 ���� ����Ʈ�� �迭�� �ٲ���Ѵ�.
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        byte[] hash = sha256.ComputeHash(bytes);

        string resultText = string.Empty;
        foreach (byte b in hash)
        {
            // byte�� �ٽ� string���� �ٲ㼭 �̾���̱�
            resultText += b.ToString("X2");
        }

        return resultText;
    }


    private void LogIn()
    {
        //1.���̵� �Է��� Ȯ���Ѵ�

        string id = LoginInputFields.IDInputField.text;
        string password = LoginInputFields.PasswordInputField.text;

        if (string.IsNullOrEmpty(id))
        {
            LoginInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            LoginInputFields.ResultText.text = "���̵� �Է����ּ���.";
            return;
        }
        //2. ��й�ȣ �Է��� Ȯ���Ѵ�
        else if (string.IsNullOrEmpty(password))
        {
            LoginInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            LoginInputFields.ResultText.text = "��й�ȣ�� �Է����ּ���.";
            return;
        }

        //3. PlayerPrefs.Get�� �̿��ؼ� ���̵�� ��й�ȣ�� �´��� Ȯ���Ѵ�.
        if(!PlayerPrefs.HasKey(PREFIX + id))
        {
            LoginInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            LoginInputFields.ResultText.text = "���̵�� ��й�ȣ�� Ȯ�����ּ���.";
            return;
        }

        string hashedPassword = PlayerPrefs.GetString(key: PREFIX + id);
        if (hashedPassword != Encryption(text: password + SALT))
        {
            LoginInputFields.ResultText.rectTransform.DOShakePosition(duration, strength);
            LoginInputFields.ResultText.text = "���̵�� ��й�ȣ�� Ȯ�����ּ���.";
            return;
        }

        //4. �´ٸ� �α���
        Debug.Log("�α��� ����!");

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
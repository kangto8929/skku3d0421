using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    //��ǥ: ���� ���� '�񵿱� ���'���� �ε��ϰ� �ʹ�.
    // ���� �ε� ������� �ð������� ǥ���ϰ� �ʹ�.
    // �� %���α׷��� �ٿ� %�� �ؽ�Ʈ

    //�Ӽ�
    // - ���� �� ��ȣ(�ε���)
    public int NextSceneIndex = 2;


    // - ���α׷��� �����̴� ��
    public Slider ProgressSlider;

    // - ���α׷��� �ؽ�Ʈ
    public TextMeshProUGUI ProgressText;

    public TextMeshProUGUI TitleText;

    public void Start()
    {
        StartCoroutine(LoadNextScene_Coroutine());

        // Ÿ��Ʋ ����: ������ �� ���̰� (alpha = 0)
        Color titleColor = TitleText.color;
        titleColor.a = 0f;
        TitleText.color = titleColor;

        // ó�� �� �� �ڿ������� ��Ÿ����, �� �������� �ݺ�
        // ������ ����
        Sequence seq = DOTween.Sequence();

        seq.Append(TitleText.DOFade(1f, 3f).SetEase(Ease.InOutSine)) // ���������� �� 2.5��
            //.AppendInterval(1f)
           .Append(TitleText.DOFade(0f, 1.5f).SetEase(Ease.InOutSine)) // ���������� �� 1.5��
           .AppendInterval(0.5f)
           .SetLoops(-1); // ���� �ݺ�

    }

    private IEnumerator LoadNextScene_Coroutine()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(NextSceneIndex);
        ao.allowSceneActivation = false;

        //�ε��� �Ǵ� ���� ����ؼ� �ݺ���
        while (ao.isDone == false)
        {
            //�񵿱�� ������ �ڵ带 �ۼ�
            Debug.Log(ao.progress);
            ProgressSlider.value = ao.progress;

            //ProgressText.text = $"{ao.progress * 100f}%";

            //������ ����ؼ� ���� �����ͳ� ��ȹ �����͸� �޾ƿ��� �ȴ�.
            if(ao.progress >= 0.1f)
            {
                ProgressText.text = "������? Ȯ��. ����? Ȯ��. ����? ...Ȯ��!";
            }

            else if (ao.progress >= 0.3f)
            {
                ProgressText.text = "������ 90%�� �ڽŰ�, �������� ��¦�̾�";
            }

            else if (ao.progress >= 0.5f)
            {
                ProgressText.text = "���߷� ���� �����簡 �ذ��� �����߸���!";
            }

            else if (ao.progress >= 0.7f)
            {
                ProgressText.text = "��� ���׷��Ÿ��� �Ҹ��� �鸰�ٸ�... �ذ��� Ȯ�� 99%";
            }


            if (ao.progress >= 0.9f)
            {
                
                ProgressText.text = "�غ���! �ذ���� ���� ������ �ž�.";
                yield return new WaitForSeconds(4f);
                ao.allowSceneActivation = true;
            }

            //yield return new WaitForSeconds(3f);
            //yield return null;//1������ ���
        }
    }
}

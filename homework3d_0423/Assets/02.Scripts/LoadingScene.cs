using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    //목표: 다음 씬을 '비동기 방식'으로 로드하고 싶다.
    // 또한 로딩 진행률을 시각적으로 표현하고 싶다.
    // ㄴ %프로그래스 바와 %별 텍스트

    //속성
    // - 다음 씬 번호(인덱스)
    public int NextSceneIndex = 2;


    // - 프로그래스 슬라이더 바
    public Slider ProgressSlider;

    // - 프로그래스 텍스트
    public TextMeshProUGUI ProgressText;

    public TextMeshProUGUI TitleText;

    public void Start()
    {
        StartCoroutine(LoadNextScene_Coroutine());

        // 타이틀 색상: 시작은 안 보이게 (alpha = 0)
        Color titleColor = TitleText.color;
        titleColor.a = 0f;
        TitleText.color = titleColor;

        // 처음 한 번 자연스럽게 나타나고, 그 다음부터 반복
        // 시퀀스 생성
        Sequence seq = DOTween.Sequence();

        seq.Append(TitleText.DOFade(1f, 3f).SetEase(Ease.InOutSine)) // 선명해지는 데 2.5초
            //.AppendInterval(1f)
           .Append(TitleText.DOFade(0f, 1.5f).SetEase(Ease.InOutSine)) // 투명해지는 데 1.5초
           .AppendInterval(0.5f)
           .SetLoops(-1); // 무한 반복

    }

    private IEnumerator LoadNextScene_Coroutine()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(NextSceneIndex);
        ao.allowSceneActivation = false;

        //로딩이 되는 도안 계속해서 반복문
        while (ao.isDone == false)
        {
            //비동기로 실행할 코드를 작성
            Debug.Log(ao.progress);
            ProgressSlider.value = ao.progress;

            //ProgressText.text = $"{ao.progress * 100f}%";

            //서버와 통신해서 유저 데이터나 기획 데이터를 받아오면 된다.
            if(ao.progress >= 0.1f)
            {
                ProgressText.text = "지팡이? 확인. 모자? 확인. 마법? ...확인!";
            }

            else if (ao.progress >= 0.3f)
            {
                ProgressText.text = "마법의 90%는 자신감, 나머지는 반짝이야";
            }

            else if (ao.progress >= 0.5f)
            {
                ProgressText.text = "명중률 좋은 마법사가 해골을 쓰러뜨린다!";
            }

            else if (ao.progress >= 0.7f)
            {
                ProgressText.text = "어디서 덜그럭거리는 소리가 들린다면... 해골일 확률 99%";
            }


            if (ao.progress >= 0.9f)
            {
                
                ProgressText.text = "준비해! 해골들이 흔들기 시작할 거야.";
                yield return new WaitForSeconds(4f);
                ao.allowSceneActivation = true;
            }

            //yield return new WaitForSeconds(3f);
            //yield return null;//1프레임 대기
        }
    }
}

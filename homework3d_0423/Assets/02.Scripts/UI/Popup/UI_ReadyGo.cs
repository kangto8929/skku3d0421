using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class UI_ReadyGo : MonoBehaviour
{
    public GameObject ReadyGoPanel;

    public TextMeshProUGUI ReadyText;


    private void Start()
    {

        ReadyGoPanel.SetActive(true);
        GameManager.Instance.TimePause();
        StartCoroutine(ShowReadyGo());
    }

    IEnumerator ShowReadyGo()
    {
        ReadyText.text = "Ready";

        //점 하나씩 추가
        for(int i=0; i < 3; i++)
        {
            yield return new WaitForSecondsRealtime(1f);
            ReadyText.text += ".";
        }

        yield return new WaitForSecondsRealtime(1f);

        ReadyText.text = "Go!";

        yield return new WaitForSecondsRealtime(1f);
        ReadyGoPanel.SetActive(false);
        GameManager.Instance.Continue();
    }
}

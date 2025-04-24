using UnityEngine;
using UnityEngine.UI;

public class Steminer : MonoBehaviour
{
    public static Steminer Instance;

    public Slider SteminerSlider;
    public float UpSteminerSpeed = 0.2f;
    public float DownSteminerSpeed = 0.4f;
    public float DownJumpSteminerSpeed = 0.8f;

    private void Start()
    {
        Instance = this;
        SteminerSlider.value = 1;
    }

    //���׹̳� ����
    public void IncreaseSteminer()
    {
        SteminerSlider.value = SteminerSlider.value + UpSteminerSpeed * Time.deltaTime;
    }

    //���׹̳� ����
    public void DecreaseSteminer()
    {
        SteminerSlider.value = SteminerSlider.value - DownSteminerSpeed * Time.deltaTime;

    }

    public void DecreaseJumpSteminer()
    {
        SteminerSlider.value = SteminerSlider.value - DownSteminerSpeed * Time.deltaTime;

    }


}

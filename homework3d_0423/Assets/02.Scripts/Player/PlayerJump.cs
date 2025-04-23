using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Steminer.Instance.DecreaseSteminer();
            Debug.Log("왼쪽 시프트 키 눌림, 스테미너 감소!");
        }

        else if(Input.GetKey(KeyCode.RightShift))
        {
            Steminer.Instance.DecreaseSteminer();
            Debug.Log("오른쪽 시프트 키 눌림, 스테미너 감소!");
        }

        else
        {
            Steminer.Instance.IncreaseSteminer();
            Debug.Log("스테미너 회복!");
        }
    }
}

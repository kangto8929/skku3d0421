using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Steminer.Instance.DecreaseSteminer();
            Debug.Log("���� ����Ʈ Ű ����, ���׹̳� ����!");
        }

        else if(Input.GetKey(KeyCode.RightShift))
        {
            Steminer.Instance.DecreaseSteminer();
            Debug.Log("������ ����Ʈ Ű ����, ���׹̳� ����!");
        }

        else
        {
            Steminer.Instance.IncreaseSteminer();
            Debug.Log("���׹̳� ȸ��!");
        }
    }
}

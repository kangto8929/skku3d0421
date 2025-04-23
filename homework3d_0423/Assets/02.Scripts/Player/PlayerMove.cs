using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목표: wasd를 누르면 캐릭터을 카메라 방향에 맞게 이동시키고 싶다.
    // 필요 속성:
    // - 이동속도
    public float MoveSpeed = 7f;
    
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    
    // 구현 순서:
    // 
    void Update()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 2. 입력으로부터 방향을 설정한다.
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized; 
        
        // 2-1. 메인 카메라를 기준으로 방향을 변환한다.
        dir = Camera.main.transform.TransformDirection(dir);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //플레이어 달림
            Steminer.Instance.DecreaseSteminer();
           // Debug.Log("왼쪽 시프트 키 눌림, 스테미너 감소!");
        }

        else if (Input.GetKey(KeyCode.RightShift))
        {
            //플레이어 달림
            Steminer.Instance.DecreaseSteminer();
            //Debug.Log("오른쪽 시프트 키 눌림, 스테미너 감소!");
        }

        else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && !Input.GetKey(KeyCode.E))
        {
            //달리지 않을 경우 +  벽을 타지 않을 경우
            Steminer.Instance.IncreaseSteminer();
          //  Debug.Log("스테미너 회복!");
        }

      
        
        
        // 4. 방향에 따라 플레이어를 이동한다.
        //transform.position += dir * MoveSpeed * Time.deltaTime;
        _characterController.Move(dir * MoveSpeed * Time.deltaTime);
    }
}

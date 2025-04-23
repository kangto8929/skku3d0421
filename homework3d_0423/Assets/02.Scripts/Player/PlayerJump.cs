using JetBrains.Annotations;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float JumpForce = 7f;//점프 힘
    public float GroundCheckDistance = 0.2f;//땅 체크 거리
    public LayerMask GorundLayer;//땅으로 판단할 레이어

    private Rigidbody _rigidbody;
    private int _jumpCount = 0;//점프 횟수
    private int _maxJumps = 2;// 최대 점프 횟수
    private bool isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundCheck();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded || _jumpCount< _maxJumps)
            {
                Jump();
                _jumpCount++;
                Debug.Log($"{_jumpCount}단 점프!");
            }
        }
    }

    void GroundCheck()
    {
        //아래 방햐으로 레이저 쏴서 닿았는지 확인
        Ray ray = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(ray, GroundCheckDistance, GorundLayer);

        if (isGrounded)
        {
            _jumpCount = 0; // 땅에 닿으면 점프 횟수 초기화
        }

      //  Debug.Log("땅에 붙어있음");
    }

    void Jump()
    {
       
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z); // 수직 속도 초기화
        _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        Debug.Log("점푸");
    }
}

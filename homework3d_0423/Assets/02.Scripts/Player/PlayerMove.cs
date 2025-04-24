using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 7f;
    public float RunSpeed = 12f;
    public float CurrnetSpeed;

    public float DashDuration = 0.5f;
    public float _DashSpeed = 20f;
    public bool IsDashing = false;
    private float _dashTimer = 0f;

    private CharacterController _characterController;

    // 점프 관련
    public float JumpPower = 5f;
    private float _yVelocity = 0f;
    private const float GRAVITY = -9.8f;
    public float GroundCheckDistance = 1.0f;
    public LayerMask GroundLayer;
    public int JumpCount = 0;
    public bool IsGrounded;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 입력
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v).normalized;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0f;

        // 대시
        if (Input.GetKeyDown(KeyCode.E) && !IsDashing)
        {
            IsDashing = true;
            _dashTimer = DashDuration;
            Steminer.Instance.DashDecreaseSteminer();
        }

        if (IsDashing)
        {
            CurrnetSpeed = _DashSpeed;
            _dashTimer -= Time.deltaTime;
            dir = Camera.main.transform.forward;
            dir.y = 0f;
            if (_dashTimer <= 0f)
                IsDashing = false;
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Steminer.Instance.DecreaseSteminer();
            CurrnetSpeed = RunSpeed;
        }
        else
        {
            Steminer.Instance.IncreaseSteminer();
            CurrnetSpeed = MoveSpeed;
        }

        // 점프
        GroundCheck();

        if (Input.GetButtonDown("Jump") && JumpCount < 2)
        {
            _yVelocity = JumpPower;
            JumpCount++;
            Debug.Log($"{JumpCount}단 점프!");
        }

        _yVelocity += GRAVITY * Time.deltaTime;
        dir.y = _yVelocity;

        _characterController.Move(dir * CurrnetSpeed * Time.deltaTime);
    }

    void GroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        IsGrounded = Physics.Raycast(ray, GroundCheckDistance, GroundLayer);
        if (IsGrounded && _yVelocity < 0)
        {
            JumpCount = 0;
            _yVelocity = -2f;
            Debug.Log("착지함");
        }
    }
}

/*using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    

    // - 이동속도
    public float MoveSpeed = 7f;
    public float RunSpeed = 12f;
    public float CurrnetSpeed;

    public float DashDuration = 5f;//대쉬하는데 걸리는 시간
    public float _DashSpeed = 20f;
    public bool IsDashing = false;
    private float _dashTimer = 0f;


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
            CurrnetSpeed = RunSpeed;
           // Debug.Log("왼쪽 시프트 키 눌림, 스테미너 감소!");
        }

        else if (Input.GetKey(KeyCode.RightShift))
        {
            //플레이어 달림
            Steminer.Instance.DecreaseSteminer();
            CurrnetSpeed = RunSpeed;
            //Debug.Log("오른쪽 시프트 키 눌림, 스테미너 감소!");
        }

        else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            //달리지 않을 경우 +  벽을 타지 않을 경우
            Steminer.Instance.IncreaseSteminer();
            CurrnetSpeed = MoveSpeed;
            //  Debug.Log("스테미너 회복!");
        }

       

        else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && !Input.GetKey(KeyCode.E))
        {
            //달리지 않을 경우 +  벽을 타지 않을 경우
            Steminer.Instance.IncreaseSteminer();
          //  Debug.Log("스테미너 회복!");
        }

        if (Input.GetKeyDown(KeyCode.E) && !IsDashing)
        {
            //앞으로 대시
            IsDashing = true;
            _dashTimer = DashDuration;
            Steminer.Instance.DashDecreaseSteminer();
        }

        if (IsDashing)
        {
            CurrnetSpeed = _DashSpeed;
            _dashTimer -= Time.deltaTime;
            dir = Camera.main.transform.forward;
            dir.y = 0f;
            dir.Normalize();

            if (_dashTimer <= 0f)
            {
                IsDashing = false;
            }

        }
        
        
        // 4. 방향에 따라 플레이어를 이동한다.
        //transform.position += dir * MoveSpeed * Time.deltaTime;
        _characterController.Move(dir * CurrnetSpeed * Time.deltaTime);
    }
}*/

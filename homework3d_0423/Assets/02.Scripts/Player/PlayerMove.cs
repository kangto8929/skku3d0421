using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;

    private GameObject player;

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

    //벽타기
    public float WallCheckDistance;
    public LayerMask WallLayer;
    public bool IsOnWall;
    private Vector3 wallNormal;

    public float WallClimbSpeed = 3f;
    private bool IsClimbingWall;

    public Animator Animator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Instance = this;
    }

    void Update()
    {
        // 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        Vector3 dir = new Vector3(h, 0, v);
        //_animator.SetLayerWeight(2, weight: player.Health / player.MaxHealth);
        Animator.SetFloat(name: "MoveAmount", dir.magnitude);
        dir = dir.normalized;
        //여기에 노멀라이즈 해야 함

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0f;

       

        // 대시
        if (Input.GetKeyDown(KeyCode.E) && !IsDashing)
        {
            IsDashing = true;
            _dashTimer = DashDuration;
            Steminer.Instance.DecreaseSteminer();
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
            

            if(Steminer.Instance.SteminerSlider.value == 0)
            {
                CurrnetSpeed = MoveSpeed;
            }

            else
            {
                CurrnetSpeed = RunSpeed;
            }
        }

        else if(!Input.GetKey(KeyCode.J)
            && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)
           && !Input.GetKey(KeyCode.E) && !Input.GetButtonDown("Jump"))
        {
            Steminer.Instance.IncreaseSteminer();
            CurrnetSpeed = MoveSpeed;
        }

        // 점프
        GroundCheck();
        //벽 체크
        WallCheck();

        //벽 타기
        IsClimbingWall = false;
        if(IsOnWall && Input.GetKey(KeyCode.J) && Steminer.Instance.SteminerSlider.value > 0)
        {
            //스테미너가 0이 아닌 이상 벽타기 가능
            IsClimbingWall = true;
            Steminer.Instance.DecreaseSteminer();
            _yVelocity = WallClimbSpeed;
        }

        //점프
        if (Input.GetButtonDown("Jump") && JumpCount < 2)
        {
            Steminer.Instance.DecreaseSteminer();
            _yVelocity = JumpPower;
            JumpCount++;
            Debug.Log($"{JumpCount}단 점프!");
        }

        _yVelocity += GRAVITY * Time.deltaTime;
        dir.y = _yVelocity;

        _characterController.Move(dir * CurrnetSpeed * Time.deltaTime);
    }

    //땅 체크
    void GroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        IsGrounded = Physics.Raycast(ray, GroundCheckDistance, GroundLayer);
        if (IsGrounded && _yVelocity < 0)
        {
            JumpCount = 0;
            _yVelocity = -2f;
            //Debug.Log("착지함");
        }
    }
    
    //벽 체크
    void WallCheck()
    {
        RaycastHit hit;
        Vector3[] directions =
        {
            transform.right,//오른쪽
            -transform.right,//왼쪽
            transform.forward,//앞
            -transform.forward//뒤
        };

        IsOnWall = false;

        foreach(var dir in directions)
        {
            if(Physics.Raycast(transform.position, dir, out hit, WallCheckDistance, WallLayer))
            {
                IsOnWall = true;//검사했는데 벽 있음
                wallNormal = hit.normal;
                Debug.DrawRay(transform.position, dir * WallCheckDistance, Color.green);
                Debug.Log("벽있다");
                return;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            Debug.Log("코인 충돌 감지됨!");
        }
    }
}


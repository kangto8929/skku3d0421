using UnityEngine;

public class WallClimbing : MonoBehaviour
{
    public float ClimbSpeed = 3f;
    public float WallCheckDistance = 1f;
    public LayerMask WallLayer;
    public float Gravity = -9.81f;

    private CharacterController _characterController;
    public bool IsClimbing = false;
    private float _verticalVelocity = 0f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleWallClimb();
    }

    void HandleWallClimb()
    {
        bool wallFront = Physics.Raycast(transform.position, transform.forward, WallCheckDistance, WallLayer);

        //벽 앞에서 W키를 누르면 벽타기 시작
        if(wallFront && Input.GetKeyDown(KeyCode.W))
        {
            IsClimbing = true;
        }

        else if(!wallFront || Input.GetKeyUp(KeyCode.W))
        {
            IsClimbing = false;
        }

        Vector3 move = Vector3.zero;

        if(IsClimbing)
        {
            _verticalVelocity = ClimbSpeed;
        }

        else
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }

        move.y = _verticalVelocity;
        _characterController.Move(move*Time.deltaTime);
    }
}

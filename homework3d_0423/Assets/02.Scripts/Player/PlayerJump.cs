using JetBrains.Annotations;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private CharacterController _characterController;

    public float JumpPower = 5f;
    public float GroundCheckDistance = 1.0f;
    public LayerMask GroundLayer;

    private float _yVelocity = 0f;       // 중력가속도
    private const float GRAVITY = -9.8f; // 중력


    public int JumpCount = 0;
    public bool isGrounded;

   // private bool _isJumping = false;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundCheck();

        if(Input.GetButtonDown("Jump") && JumpCount < 2 )
        {
            _yVelocity = JumpPower;
            JumpCount++;
            Debug.Log($"{JumpCount}단 점프!");

        }

        _yVelocity += GRAVITY * Time.deltaTime;
        _characterController.Move(new Vector3(0, _yVelocity, 0) * Time.deltaTime);
        
    }


    void GroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(ray, GroundCheckDistance, GroundLayer);

        if(isGrounded && _yVelocity < 0)
        {
            JumpCount = 0;
            Debug.Log("착지함");
        }
    }
    

    
}

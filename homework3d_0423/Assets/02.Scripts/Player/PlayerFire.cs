using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerFire : MonoBehaviour
{

    // 필요 속성
    // - 발사 위치
    public GameObject FirePosition;
    // - 폭탄 프리팹
    public GameObject BombPrefab;
    // - 던지는 힘
    public float ThrowPower = 15f;


    // 목표: 마우스의 왼쪽 버튼을 누르면 카메라가 바라보는 방향으로 총을 발사하고 싶다.
    public ParticleSystem BulletEffect;


    //폭탄 개수 제한
    public int MaxBombCount = 50;
    public int BombCount = 50;
    public TextMeshProUGUI BombCountText;

    private Animator _animator;

    public GameObject UI_Crosshair;
    public GameObject UI_SniperZoom;

    public float ZoomInSize = 15f;
    public float ZoomOutSize = 60f;
    private bool _zoomMode = false;


    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        BombCountText.text = BombCount + " / " + MaxBombCount;
    }

    private void LateUpdate()
    {
        FirePosition.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        FirePosition.transform.rotation = Camera.main.transform.rotation;
    }


    private void Update()
    {
        // 2. 오른쪽 버튼 입력 받기
        // - 0: 왼쪽, 1: 오른쪽, 2: 휠
        if (Input.GetMouseButtonDown(2))
        {
            _zoomMode = !_zoomMode;
            if(_zoomMode)
            {
                UI_SniperZoom.SetActive(true);
                UI_Crosshair.SetActive(false);
                Camera.main.fieldOfView = ZoomInSize;
            }

            else
            {
                UI_SniperZoom.SetActive(false);
                UI_Crosshair.SetActive(true);
                Camera.main.fieldOfView = ZoomOutSize;
            }


        }

        if (Input.GetMouseButtonDown(1))
        {
            if (BombCount == 0)
            {
                Debug.Log("폭탄 발사 못해");
                BombCountText.text = BombCount + " / " + MaxBombCount;
            }

            else
            {

                _animator.SetTrigger("Attack");
                BombCount--;
                BombCountText.text = BombCount + " / " + MaxBombCount;
                // 3. 발사 위치에 수류탄 생성하기

                GameObject bomb = BombPoolManager.Instance.GetBomb();
                bomb.transform.position = FirePosition.transform.position;
                bomb.transform.position = FirePosition.transform.position;
                bomb.transform.rotation = FirePosition.transform.rotation;

                Collider bombCol = bomb.GetComponent<Collider>();
                Collider playerCol = GetComponent<Collider>();
                if(bombCol != null && playerCol != null)
                {
                    Physics.IgnoreCollision(bombCol, playerCol);
                }

                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                Rigidbody bombRb = bomb.GetComponent<Rigidbody>();
                bombRb.angularVelocity = Vector3.zero;

                bombRb.AddForce(ray.direction * ThrowPower, ForceMode.Impulse);
                bombRb.AddTorque(Vector3.one);
                _animator.SetTrigger("FinishAttack");

            }
        }


          



    }
}

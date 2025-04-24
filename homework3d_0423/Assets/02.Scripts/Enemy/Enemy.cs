using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


// �ΰ�����: ���ó�� �ȶ��ϰ� �ൿ�ϴ� �˰���
// - ������/��ȹ�� -> ��Ģ ��� �ΰ����� (�������� ���)
//               ->   �� ���(���ǹ�, �ݺ���)

public class Enemy : MonoBehaviour
{
    // 1. ���¸� ���������� �����Ѵ�.
    public enum EnemyState
    {
        Idle,
        Trace,
        Return,
        Attack,
        Damaged,
        Die
    }

    // 2. ���� ���¸� �����Ѵ�.
    public EnemyState CurrentState = EnemyState.Idle;

    private GameObject _player;                       // �÷��̾�
    private CharacterController _characterController; // ĳ���� ��Ʈ�ѷ�
    private Vector3 _startPosition;                   // ���� ��ġ

    private NavMeshAgent _agent;

    public float FindDistance = 5f;     // �÷��̾� �߰� ����
    public float ReturnDistance = 5f;     // �� ���� ����
    public float AttackDistance = 2.5f;   // �÷��̾� ���� ����
    public float MoveSpeed = 3.3f;   // �̵� �ӵ�
    public float AttackCooltime = 2f;     // ���� ��Ÿ��
    private float _attackTimer = 0f;     // �� üũ��
    public int Health = 100;
    public float DamagedTime = 0.5f;   // ���� �ð�
    public float DeathTime = 1f;

    private void Start()
    {
       
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = MoveSpeed;

        _startPosition = transform.position;
        _characterController = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        // ���� ���� ���¿� ���� ���� �Լ��� ȣ���Ѵ�.
        switch (CurrentState)
        {
            case EnemyState.Idle:
                {
                    Idle();
                    break;
                }

            case EnemyState.Trace:
                {
                    Trace();
                    break;
                }

            case EnemyState.Return:
                {
                    Return();
                    break;
                }

            case EnemyState.Attack:
                {
                    Attack();
                    break;
                }
        }
    }

    public void TakeDamage(Damage damage)
    {
        // ����߰ų� ���ݹް� �ִ� ���̸�..
        if (CurrentState == EnemyState.Damaged || CurrentState == EnemyState.Die)
        {
            return;
        }

        Health -= damage.Value;

        if (Health <= 0)
        {
            CurrentState = EnemyState.Die;
            Debug.Log($"������ȯ: {CurrentState} -> Die");
            CurrentState = EnemyState.Die;
            StartCoroutine(Die_Coroutine());
            return;
        }


        Debug.Log($"������ȯ: {CurrentState} -> Damaged");

        CurrentState = EnemyState.Damaged;
        StartCoroutine(Damaged_Coroutine());
    }


    // 3. ���� �Լ����� �����Ѵ�.

    private void Idle()
    {
        // �ൿ: ������ �ִ´�. 

        // ����: �÷��̾�� ����� ���� -> Trace
        if (Vector3.Distance(transform.position, _player.transform.position) < FindDistance)
        {
            Debug.Log("������ȯ: Idle -> Trace");
            CurrentState = EnemyState.Trace;
        }
    }

    private void Trace()
    {
        // ����: �÷��̾�� �־����� -> Return
        if (Vector3.Distance(transform.position, _player.transform.position) > ReturnDistance)
        {
            Debug.Log("������ȯ: Trace -> Return");
            CurrentState = EnemyState.Return;
            return;
        }

        // ����: ���� ���� ��ŭ ����� ���� -> Attack
        if (Vector3.Distance(transform.position, _player.transform.position) < AttackDistance)
        {
            Debug.Log("������ȯ: Trace -> Attack");
            CurrentState = EnemyState.Attack;
            return;
        }

        // �ൿ: �÷��̾ �����Ѵ�.
        //ctor3 dir = (_player.transform.position - transform.position).normalized;
        //_characterController.Move(dir * MoveSpeed * Time.deltaTime);
        _agent.SetDestination(_player.transform.position);
    }

    private void Return()
    {
        // ����: ���� ��ġ�� ����� ���� -> Idle
        if (Vector3.Distance(transform.position, _startPosition) <= _characterController.minMoveDistance)
        {
            Debug.Log("������ȯ: Return -> Idle");
            transform.position = _startPosition;
            CurrentState = EnemyState.Idle;
            return;
        }

        // ����: �÷��̾�� ����� ���� -> Trace
        if (Vector3.Distance(transform.position, _player.transform.position) < FindDistance)
        {
            Debug.Log("������ȯ: Return -> Trace");
            CurrentState = EnemyState.Trace;
        }


        // �ൿ: ���� ��ġ�� �ǵ��ư���.
        //Vector3 dir = (_startPosition - transform.position).normalized;
        //_characterController.Move(dir * MoveSpeed * Time.deltaTime);
        _agent.SetDestination(_startPosition);
    }

    private void Attack()
    {
        // ����: ���� ���� ���� �־����� -> Trace
        if (Vector3.Distance(transform.position, _player.transform.position) >= AttackDistance)
        {
            Debug.Log("������ȯ: Attack -> Trace");
            CurrentState = EnemyState.Trace;
            _attackTimer = 0f;
            return;
        }

        // �ൿ: �÷��̾ �����Ѵ�.
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= AttackCooltime)
        {
            Debug.Log("�÷��̾� ����!");
            _attackTimer = 0f;
        }
    }

    private IEnumerator Damaged_Coroutine()
    {
        // �ൿ: ���� �ð����� �����ִٰ� -> Trace
        /*_damagedTimer += Time.deltaTime;
        if (_damagedTimer >= DamagedTime)
        {
            _damagedTimer = 0f;
            Debug.Log("������ȯ: Damaged -> Trace");
            CurrentState = EnemyState.Trace;
        }*/

        // �ڷ�ƾ ������� ����


       // _agent.isPathStale = true;
        //_angent.ReSetPath();
        yield return new WaitForSeconds(DamagedTime);
        Debug.Log("������ȯ: Damaged -> Trace");
        CurrentState = EnemyState.Trace;
    }

    private IEnumerator Die_Coroutine()
    {
        yield return new WaitForSeconds(DeathTime);
        gameObject.SetActive(false);
    }
}
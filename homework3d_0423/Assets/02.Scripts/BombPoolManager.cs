using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BombPoolManager : MonoBehaviour
{
    public static BombPoolManager Instance;

    public GameObject BombPrefab;
    public int PoolSize = 50;

    private Queue<GameObject> _bombPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        for(int i = 0; i< PoolSize; i++)
        {
            GameObject bomb = Instantiate(BombPrefab);
            bomb.SetActive(false);
            _bombPool.Enqueue(bomb);
        }
    }

    public GameObject GetBomb()
    {
        if(_bombPool.Count > 0)
        {
            GameObject bomb = _bombPool.Dequeue();
            bomb.SetActive(true);
            return bomb;
        }

        return null;//�� �̻� �� �� �ִ� ��ź�� ���� ��
    }

    //��ź ��� �Ϸ� �� �ٽ� ������Ʈ Ǯ�� ���������� �Լ�.
    public void ReturnBomb(GameObject bomb)//��ź�� �ٽ� ��� ������ ���·� �ǵ����� �Լ�
    {
        bomb.SetActive(false);
        _bombPool.Enqueue(bomb);//��Ȱ��ȭ �� ��ź�� ť�� �־ ����
    }
}

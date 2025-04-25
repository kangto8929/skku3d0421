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

        return null;//더 이상 쓸 수 있는 폭탄이 없을 때
    }

    //폭탄 사용 완료 후 다시 오브젝트 풀로 돌려보내는 함수.
    public void ReturnBomb(GameObject bomb)//폭탄을 다시 사용 가능한 상태로 되돌리는 함수
    {
        bomb.SetActive(false);
        _bombPool.Enqueue(bomb);//비활성화 된 폭탄을 큐에 넣어서 저장
    }
}

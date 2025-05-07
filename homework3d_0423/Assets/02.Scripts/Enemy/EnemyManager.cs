using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public GameObject EnemyPrefab;
    public GameObject HpBarPrefab;
    public Canvas WorldSpaceCanvas;
    public int PoolSize = 30;
    public float SpawnInterval = 3f;

    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private Queue<EnemyHealthBar> _hpBarPool = new Queue<EnemyHealthBar>();
    //public Transform[] SpawnPoints;//여러 위치...

    private void Start()
    {
        Instance = this;

        for (int i = 0; i< PoolSize; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, transform);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);//enemy오브젝트를 enemyPool큐의 맨 뒤에 넣는다는 뜻

            GameObject _hpBarObject = Instantiate(HpBarPrefab, WorldSpaceCanvas.transform);//WorldSpaceCanvas의 자식으로 할당
            _hpBarObject.SetActive(false);
            EnemyHealthBar _hpBar = _hpBarObject.GetComponent<EnemyHealthBar>();
            _hpBarPool.Enqueue(_hpBar);
        }

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while(true)
        {
            GameObject _enemy = enemyPool.Dequeue();
            _enemy.SetActive(true);


            Vector3 _randomPosition = new Vector3(UnityEngine.Random.Range(-85.5f, 59.7f), 1f, UnityEngine.Random.Range(-38.9f, 112.3f));
            _enemy.transform.position = _randomPosition;

            EnemyHealthBar _hpBar = _hpBarPool.Dequeue();
            _hpBar.gameObject.SetActive(true);
            _hpBar.EnemyObject = _enemy.transform;


            Enemy _enemyScript = _enemy.GetComponent<Enemy>();
            _enemyScript.HPbar = _hpBar.gameObject;

            _enemyScript.OnDie += () => OnEnemyDie(_enemy, _hpBar);

            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    public void OnEnemyDie(GameObject enemy, EnemyHealthBar hpBar)
    {
        //enemy.SetActive(false);
        //hpBar.gameObject.SetActive(false);

        enemyPool.Enqueue(enemy);
        _hpBarPool.Enqueue(hpBar);

        Debug.Log("죽고 다시 풀로 돌아갔어.");
    }
}

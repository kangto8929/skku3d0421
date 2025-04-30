using UnityEngine;
using UnityEngine.UI;

public class Drum : MonoBehaviour, IDamageable
{
    public Slider HealthBar;
    public GameObject ExplosionEffectPrefab;
    public int Health = 20;
    public int Damage = 100;
    public float ExplodeRange = 100;

    public float ExplodePower = 1500f;

    private Rigidbody _rigidbody;

    private bool _isExploded;

    void Start()
    {
        HealthBar.maxValue = Health;
        HealthBar.value = Health;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(Damage damage)
    {
        Health -= damage.Value;

        HealthBar.value = HealthBar.maxValue - Health;

        if (Health <= 0 && !_isExploded)
        {
            Explode();
           
        }

        Debug.Log("아야");
    }

    private void Explode()
    {
        _isExploded = true;

        if (ExplosionEffectPrefab != null)
        {
            GameObject explosionPrefab = Instantiate(ExplosionEffectPrefab);
            explosionPrefab.transform.position = transform.position;
        }

        _rigidbody.AddForce(Vector3.up * 1500f, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.up, ForceMode.Impulse);

        //6번을 제외한 모두
        //드럼통을 감지 안하고 싶고
        //유니티는 레이어를 넘버링하는게 아니라 비트로 관리한다.
        //Collider[] colls = Physics.OverlapSphere(transform.position, ExplodeRange, layerMask:~(1<<6));
        Collider[] colls = Physics.OverlapSphere(transform.position, ExplodeRange, ~LayerMask.NameToLayer("Drum"));


        foreach (Collider coll in colls)
        {
            if(coll.TryGetComponent(out IDamageable damageable))
            {
                Damage damage = new Damage();
                damage.Value = Damage;

                damageable.TakeDamage(damage);
            }
        }


        //드럼통만 감지하고 싶어오.
        //만약에 주위에 드럼통이 있다면 드럼통도 폭발
        //Collider[] drums = Physics.OverlapSphere(transform.position, ExplodeRange, layerMask: 1 << 6);
        Collider[] drums = Physics.OverlapSphere(transform.position, ExplodeRange, LayerMask.NameToLayer("Drum"));
        foreach (Collider drumCols in drums)
        {
            if(drumCols.TryGetComponent(out Drum drum))
            {
                drum.Explode();
            }
        }

        Destroy(gameObject, 3f);
    }
}

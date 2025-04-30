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

        Debug.Log("�ƾ�");
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

        //6���� ������ ���
        //�巳���� ���� ���ϰ� �Ͱ�
        //����Ƽ�� ���̾ �ѹ����ϴ°� �ƴ϶� ��Ʈ�� �����Ѵ�.
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


        //�巳�븸 �����ϰ� �;��.
        //���࿡ ������ �巳���� �ִٸ� �巳�뵵 ����
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

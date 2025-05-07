using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider HealthSlider;
    public Transform EnemyObject; //���� ��
    public Vector3 Offset = new Vector3(0, 2.0f, 0);//�� ���� ���� ����


    private void LateUpdate()
    {
        Vector3 worldPosition = EnemyObject.transform.position + Offset;
        //Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        HealthSlider.transform.position = worldPosition;

        //HealthSlider.transform.position = screenPosition;
    }

    //ü���� ������Ʈ
    public void SetHealth(float currnetHelath, float maxHealth)
    {
        if(HealthSlider != null)
        {
            HealthSlider.value = currnetHelath / maxHealth;
        }
    }
}
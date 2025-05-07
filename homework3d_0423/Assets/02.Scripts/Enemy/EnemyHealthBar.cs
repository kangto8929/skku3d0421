using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider HealthSlider;
    public Transform EnemyObject; //따라갈 적
    public Vector3 Offset = new Vector3(0, 2.0f, 0);//적 위에 띄우는 높이


    private void LateUpdate()
    {
        Vector3 worldPosition = EnemyObject.transform.position + Offset;
        //Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        HealthSlider.transform.position = worldPosition;

        //HealthSlider.transform.position = screenPosition;
    }

    //체력을 업데이트
    public void SetHealth(float currnetHelath, float maxHealth)
    {
        if(HealthSlider != null)
        {
            HealthSlider.value = currnetHelath / maxHealth;
        }
    }
}
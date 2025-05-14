using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    public Slider HealthSlider;

    public GameObject BloodImage;//화면에 피
    public int PlayerHealth = 750;

    public bool IsAttacked = false;//공격 당했는지

    public bool IsDead = false;

    private void Start()
    {
        Instance = this;

        HealthSlider.maxValue = PlayerHealth;
        HealthSlider.value = PlayerHealth;
    }


 

    public void Attacked()
    {
        

        Image bloodImage = BloodImage.GetComponent<Image>();

        if (IsAttacked == true)
        {
            Debug.Log("아야아야");

            if (HealthSlider.value <= 0 && IsDead == false)
            {
                PlayerMove.Instance.Animator.SetTrigger("Die");
                IsDead = true;

                StartCoroutine(GameOver());
                IEnumerator GameOver()
                {
                    yield return new WaitForSeconds(1.5f);
                    GameManager.Instance.TimePause();
                    PopupManager.Instance.GameOverPanel.SetActive(true);
                }
                
            }

            else
            {
                PlayerMove.Instance.Animator.SetTrigger("GetHIt");
                Debug.Log("맞았어");
            }

                StartCoroutine(BloodShow());
            IEnumerator BloodShow()
            {
                BloodImage.gameObject.SetActive(true);
                Color color = bloodImage.color;

                while (color.a < 1f)
                {
                    color.a += Time.deltaTime / 0.5f;
                    bloodImage.color = color;
                    yield return null;
                }

                yield return new WaitForSeconds(1.5f);
                while (color.a > 0f)
                {
                    color.a -= Time.deltaTime / 1.5f;
                    bloodImage.color = color;
                    yield return null;
                }
                //color.a = 0;

                bloodImage.color = color;

                 BloodImage.gameObject.SetActive(false);
            }

            
        }

        else
        {
            return;
        }


    }


}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    public GameObject BloodImage;//ȭ�鿡 ��
    public int PlayerHealth = 150;

    public bool IsAttacked = false;//���� ���ߴ���

    private void Start()
    {
        Instance = this;
    }

    public void Attacked()
    {
       

        Image bloodImage = BloodImage.GetComponent<Image>();

        if (IsAttacked == true)
        {
            StartCoroutine(BloodShow());
            IEnumerator BloodShow()
            {
                //BloodImage.gameObject.SetActive(true);
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

                // BloodImage.gameObject.SetActive(false);
            }
        }

        else
        {
            return;
        }

           

        /*if (IsAttacked == true)
        {
            //���࿡ ���ݴ��ߴٸ�
            StartCoroutine(BloodShow());
            IEnumerator BloodShow()
            {
                BloodImage.gameObject.SetActive(true);
                Color color = bloodImage.color;

                while(color.a < 1f)
                {
                    color.a += Time.deltaTime / 1.5f;
                    bloodImage.color = color;
                    yield return null;
                }

                yield return new WaitForSeconds(1.5f);
                color.a = 0;

                bloodImage.color = color;

                BloodImage.gameObject.SetActive(false);
            }



            IsAttacked = false;
        }

        else
        {
            //���ݴ����� �ʾҴٸ�
            IsAttacked = false;
            BloodImage.gameObject.SetActive(false);
        }*/
    }


}

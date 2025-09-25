using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 20f; //�̵��ӵ�

    public float lifeTime = 2f;         //�����ð� ��

    public int damage = 1;




    // Start is called before the first frame update
    void Start()
    {
        //���� �ð� �� �ڵ� ���� (�޸� ����)
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //������ forward �������� �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider Collision)
    {
        if(Collision.CompareTag("Enemy"))
        {
            //�� �浹 �� �� ����
            //Destroy(other.gameObject);
            // projectile ����

            //Destroy(gameObject);
            Enmey enemy = Collision.GetComponent<Enmey>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
                
               
            }
        }
    }
}

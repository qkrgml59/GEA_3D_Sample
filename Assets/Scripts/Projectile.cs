using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 20f; //이동속도

    public float lifeTime = 2f;         //생존시간 초

    public int damage = 1;




    // Start is called before the first frame update
    void Start()
    {
        //일정 시간 후 자동 삭제 (메모리 관리)
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //로컬의 forward 방향으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider Collision)
    {
        if(Collision.CompareTag("Enemy"))
        {
            //적 충돌 시 적 제거
            //Destroy(other.gameObject);
            // projectile 제거

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

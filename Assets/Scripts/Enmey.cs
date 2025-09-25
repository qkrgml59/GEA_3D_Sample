using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enmey : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int EnemyHP = 5;

    private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    public void TakeDamage(int damage)
    {
        EnemyHP -= damage;

        if (EnemyHP <=0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

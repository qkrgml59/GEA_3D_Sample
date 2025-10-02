using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;


public class Enmey : MonoBehaviour
{


    [Header("�� ����")]
    public float moveSpeed = 2f;
    public int MaxHP = 5;

    [Header("�� ���� ����")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float traceRange = 15f;             //�߰� ���� �Ÿ�
    public float attackRange = 6f;            //���� ���� �Ÿ�
    public float attackCooldown = 1.5f;
    public float RunAway = 15f;

    [Header("�� UI")]
    public Slider EnemySlider;

    public enum EnemyState {Idle, Trace, Attack, RunAway }
    
    public EnemyState state = EnemyState.Idle;

    private Transform player;

    private float lastAttackTime;
   
    private int currentHP;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown;
        currentHP = MaxHP;
        EnemySlider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);


        if (currentHP <= MaxHP * 0.2f && state != EnemyState.Idle)
        {
            state = EnemyState.RunAway;
        }
       

            //FSM ������ȯ
            switch (state)
        {
            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;

            case EnemyState.Trace:
                if (dist < attackRange)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                
                else TracePlayer();
                break;
            case EnemyState.RunAway:
                if (dist > traceRange +5)
                    state = EnemyState.Idle;
                else PlayerRunAway();
                    break;

            case EnemyState.Attack:
                if (dist > attackRange)
                    state = EnemyState.Trace;
                else
                    AttackPlayer();
                break;

        }

      
    }

    void TracePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    void PlayerRunAway()
    {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position -= direction * moveSpeed * Time.deltaTime;

        
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if (ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        EnemySlider.value = (float)currentHP / MaxHP;

        if (currentHP <=0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

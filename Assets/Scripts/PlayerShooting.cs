using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("빨간 공격")]
    public GameObject fileprojectilePrefab;       //projectile 프리팹
    

    [Header("파란 공격")]
    public GameObject IceprojectilePrefab;
   

    public Transform firePoint;              //발사  위치 ( 총구 )

    Camera cam;

    private bool isFire = false;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;   // 메인 카메라 가져오기
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isFire = !isFire;
        }
        if (Input.GetMouseButtonDown(0))    //좌클릭 발사
        {
            if (isFire)
            {
                FireShoot();
            }
            else
            {
                IceShoot();
            }
        }


    }

    void FireShoot()
    {
        //마우스 화면에서 -> 광선 (ray) 쏘기
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;    //방향 벡터

        //projectile 생성
        GameObject proj = Instantiate(fileprojectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
    }

    void IceShoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;    //방향 벡터

        //projectile 생성
        GameObject proj = Instantiate(IceprojectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("���� ����")]
    public GameObject fileprojectilePrefab;       //projectile ������
    

    [Header("�Ķ� ����")]
    public GameObject IceprojectilePrefab;
   

    public Transform firePoint;              //�߻�  ��ġ ( �ѱ� )

    Camera cam;

    private bool isFire = false;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;   // ���� ī�޶� ��������
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isFire = !isFire;
        }
        if (Input.GetMouseButtonDown(0))    //��Ŭ�� �߻�
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
        //���콺 ȭ�鿡�� -> ���� (ray) ���
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;    //���� ����

        //projectile ����
        GameObject proj = Instantiate(fileprojectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
    }

    void IceShoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;    //���� ����

        //projectile ����
        GameObject proj = Instantiate(IceprojectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
    }
}

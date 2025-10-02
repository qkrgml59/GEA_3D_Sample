using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Runtime.ExceptionServices;

public class PlayerController: MonoBehaviour
{
    [Header("플레이어 정보")]
    public float speed = 5f;
    public float jumpPower = 5f;
    public float gravity = -9.81f;
    public int maxHP = 100;

    [Header("플레이어 UI")]
    public Slider hpSlider;


    [Header("카메라 정보")]
    public CinemachineVirtualCamera virtualCamera;
    public float rotationSpeed = 10f;

    private CinemachinePOV pov;

    private CharacterController controller;

    private Vector3 velocity;


    public bool isGrounded;
    public bool usingFreeLook = false;

  

    private int currentHP;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        //virtual camera 의 POV 컴포넌트 가져오기
        GetComponent<CinemachineSwitcher>();

        currentHP = maxHP;
        hpSlider.value = 1f;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pov.m_HorizontalAxis.Value = transform.eulerAngles.y;
            pov.m_VerticalAxis.Value = 0f;
        }

        //땅에 닿아 있는지 확인
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0 )
        {
            velocity.y = -2f; //지면에 붙이기
        }



        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        

        //카메라 기준 방향 계산
        Vector3 camForward = virtualCamera.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = virtualCamera.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * z + camRight * x).normalized;  //이동 방향 = 카메라 forward/right 기반
        controller.Move(move * speed * Time.deltaTime);

        


        float cameraYaw = pov.m_HorizontalAxis.Value;          //마우스 회전 값
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        
       
        
        //달리기
        if (isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 15f;
            virtualCamera.m_Lens.FieldOfView = 80f;

        }
        if (isGrounded && Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
            virtualCamera.m_Lens.FieldOfView = 60f;
        }
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        

            //점프
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }
        //중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpSlider.value = (float)currentHP / maxHP;

        if (currentHP <0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

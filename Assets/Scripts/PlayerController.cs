using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.InteropServices;
using System.Xml.Schema;

public class PlayerController: MonoBehaviour
{
    public float speed = 5f;

    public float jumpPower = 5f;

    public float gravity = -9.81f;

    
    
    public CinemachineVirtualCamera virtualCamera;


    public float rotationSpeed = 10f;

    private CinemachinePOV pov;

    private CharacterController controller;

    private Vector3 velocity;

    public bool isGrounded;

    public bool usingFreeLook = false;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        //virtual camera 의 POV 컴포넌트 가져오기
        GetComponent<CinemachineSwitcher>();


    }

    // Update is called once per frame
    void Update()
    {
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

        if (usingFreeLook)
        {
            speed = 0;
            jumpPower = 0;
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
}

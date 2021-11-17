using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    [Header("Camera")]
    public RotationAxes m_axes = RotationAxes.MouseXAndY;
    public float m_sensitivityX = 10f;
    public float m_sensitivityY = 10f;

    // 水平方向的 镜头转向
    public float m_minimumX = -360f;
    public float m_maximumX = 360f;
    // 垂直方向的 镜头转向 (这里给个限度 最大仰角为45°)
    public float m_minimumY = -45f;
    public float m_maximumY = 45f;

    float m_rotationY = 0f;
    float m_rotationX = 0f;

    public GameObject myCamera;
    public bool canControl;

    [Header("Player")]
    public GameObject Arrow;
    public int ArrowNum;
    public float power;
    public float powerSpeed;
    public bool canFire;

    public float Score;


    // Use this for initialization
    void Start()
    {
        // 防止 刚体影响 镜头旋转
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
             SceneManager.LoadScene("SampleScene");
        }
        if (!canControl) return;
        if (Input.GetMouseButton(1))
        {
            canFire = true;
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                power += powerSpeed;
                if(power>1)power=1;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                power -= powerSpeed;
                if(power<0)power=0;
            }
        }
        else
        {
            if (canFire && ArrowNum>0)
            {
                canControl=false;
                ArrowNum--;
                canFire = false;
                Instantiate(Arrow, transform.position, transform.rotation).transform.GetComponent<Arrow>().Fire(power,this.gameObject);
                power = 0;
            }
        }
        if (m_axes == RotationAxes.MouseXAndY)
        {
            m_rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivityX;
            m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
            m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

            transform.localEulerAngles = new Vector3(-m_rotationY, m_rotationX, 0);
        }
        else if (m_axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * m_sensitivityX, 0);
        }
        else
        {
            m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
            m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

            transform.localEulerAngles = new Vector3(-m_rotationY, transform.localEulerAngles.y, 0);
        }
    }
}

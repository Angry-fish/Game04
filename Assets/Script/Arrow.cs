using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    public Rigidbody myRigidBody;
    public GameObject ArrowPoint;
    public GameObject myUI;
    Transform myArrowPoint;
    GameObject myAP;
    public float minSpeed;
    public float ISpeed;
    public float roY;
    public Vector3 roA;
    public GameObject[] myCamera;
    int index;
    bool stop;
    bool candes;
    Quaternion rota;
    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        myUI = GameObject.FindGameObjectWithTag("UI");
        myRigidBody = GetComponent<Rigidbody>();
        //for (int temp=0; temp < myCamera.Length; temp++)
        //{
        //    myCamera[temp].SetActive(false);
        //}
    }

    public void Fire(float s, GameObject p)
    {
        myUI = GameObject.FindGameObjectWithTag("UI");
        Player = p;
        Player.SetActive(false);
        myUI.GetComponent<UI>().target.SetActive(false);
        myCamera[0].SetActive(true);
        Time.timeScale = 0.5f;
        StartCoroutine(des());
        candes = true;
        myRigidBody = GetComponent<Rigidbody>();
        myRigidBody.velocity = transform.forward * (minSpeed + ISpeed * s);
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            if (!myAP.activeInHierarchy)
            {
                MainCamera();
                Destroy(this.gameObject);
            }
            transform.position = myArrowPoint.position;
            myRigidBody.velocity = Vector3.zero;
            transform.rotation = rota;
        }
        else
        {
            roA = transform.eulerAngles;
            roA.x = (myRigidBody.velocity.y) * roY;
            transform.rotation = Quaternion.Euler(roA);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (stop) return;
        myAP = Instantiate(ArrowPoint, transform.position, transform.rotation);
        myArrowPoint = myAP.transform;
        myArrowPoint.SetParent(other.transform);
        Time.timeScale = 1f;
        candes = false;
        rota = transform.rotation;
        myRigidBody.useGravity = false;
        stop = true;
        Player.SetActive(false);
        index = Random.Range(1, myCamera.Length);
        myCamera[0].SetActive(false);
        myCamera[index].SetActive(true);
        Invoke("MainCamera", 1.5f);

        if (other.gameObject.CompareTag("target1"))
        {

            if (other.transform.GetComponentInParent<Target>().canflick)
            {
                Player.GetComponent<Camera>().Score += 2f;
            }
            else
            {
                Player.GetComponent<Camera>().Score += 1f;
            }

        }
        else if (other.gameObject.CompareTag("target2"))
        {
            if (other.transform.GetComponentInParent<Target>().canflick)
            {
                Player.GetComponent<Camera>().Score += 3f;
            }
            else
            {
                Player.GetComponent<Camera>().Score += 2f;
            }
        }
        else if (other.gameObject.CompareTag("target3"))
        {
            if (other.transform.GetComponentInParent<Target>().canMove)
            {
                Player.GetComponent<Camera>().Score += 5f;
            }
            else
            {
                Player.GetComponent<Camera>().Score += 3f;
            }
            if (other.transform.GetComponentInParent<Target>().canflick)
            {
                Player.GetComponent<Camera>().Score += 1f;
            }

        }
    }

    void MainCamera()
    {
        Time.timeScale = 1f;
        myCamera[index].SetActive(false);
        Player.SetActive(true);
        myUI.GetComponent<UI>().target.SetActive(true);
        Player.GetComponent<Camera>().canControl = true;
    }
    IEnumerator des()
    {
        yield return new WaitForSeconds(3f);
        if (candes)
        {
            Time.timeScale = 1f;
            Player.SetActive(true);
            myUI.GetComponent<UI>().target.SetActive(true);
            Player.GetComponent<Camera>().canControl = true;
            Destroy(this.gameObject);
        }
    }
}

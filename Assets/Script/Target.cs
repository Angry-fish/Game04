using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] point;
    public Vector3[] myPoint;
    public float speed;
    public bool canMove;
    public bool canflick;
    int index;
    public GameObject Camera;
    Camera Control;
    float dis;

    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("Player");
        Control = Camera.GetComponent<Camera>();
        for(int temp=0;temp<point.Length;temp++){
            myPoint[temp]=point[temp].position;
        }
        index=Random.Range(0,myPoint.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove)return;
        if(Vector3.Distance(myPoint[index],transform.position)<.2f){
            index++;
            if(index>1)index=0;
        }
        transform.position=Vector3.MoveTowards(transform.position,myPoint[index],speed*Time.deltaTime);
    }

}

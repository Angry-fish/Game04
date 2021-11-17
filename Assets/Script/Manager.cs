using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject[] Target; 
    // Start is called before the first frame update
    void Start()
    {
        Invoke("FindAllTarget",0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FindAllTarget(){
        Target=GameObject.FindGameObjectsWithTag("target");
        for(int temp=0;temp<Target.Length;temp++){
            if(Target[temp].GetComponent<Target>().canflick){
                StartCoroutine(Flick(Target[temp]));
            }
        }
    }
    IEnumerator Flick(GameObject t){
        while(true){
            t.SetActive(true);
            yield return new WaitForSeconds(2f);
            t.SetActive(false);
            yield return new WaitForSeconds(Random.Range(4f,8f));
        }
    }
}

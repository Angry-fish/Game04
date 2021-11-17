using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject Control;
    Camera Player;
    public Image power;
    public Text ArrowNum;
    public Text Score;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        Player = Control.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ArrowNum.text = Player.ArrowNum.ToString();
        Score.text=Player.Score.ToString();
        power.fillAmount = Player.power;

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BananaControl : MonoBehaviour
{
    public bool IsPlayerPosUp = false;
    public GameObject Banana; // player
    public Transform[] PlayerPos;
    void Start()
    {

    }
    void Update()
    {
        if (!GameControl.IsGameStart) return;
        SwitchBananaPosition();
    }
   
    void  SwitchBananaPosition()
    {
        if (Input.GetKeyDown(KeyCode.D)||Input.GetMouseButtonDown(0))
        {
            if (IsPlayerPosUp) return;
            Debug.Log("�W�� ");
            IsPlayerPosUp = true;
            Banana.transform.position = PlayerPos[0].position;
        }

       else if (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(1))
        {
            if (!IsPlayerPosUp) return;
            Debug.Log("�U�� ");
            IsPlayerPosUp = false;
            Banana.transform.position = PlayerPos[1].position;
        }
    }
}

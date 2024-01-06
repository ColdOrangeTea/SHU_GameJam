using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailControl : MonoBehaviour
{
    [SerializeField] float lifeTime = 2f;
    [SerializeField] float speed = 1;
    private float Timer = 0;
    public float timeLimit = 10f;

    void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += new Vector3(-1,0,0)* 10 * Time.deltaTime; 
       transform.position += Vector3.left* 8 * Time.deltaTime;


        if (!GameControl.IsGameStart) return;   
    }

}

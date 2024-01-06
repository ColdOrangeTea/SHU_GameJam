using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground_0 : MonoBehaviour
{
    public bool Camera_Move;
    public float Camera_MoveSpeed = 1.5f;
    [Header("Layer Setting")]
    public float[] Layer_Speed = new float[7];
    public GameObject[] Layer_Objects = new GameObject[7];
    private float Timer = 0;
    public float timeLimit = 10f;

    private Transform _camera;
    private float[] startPos = new float[7];
    private float boundSizeX;
    private float sizeX;
    private GameObject Layer_0;
    public float BG4_yOffset;
    public float BG5_yOffset;

    void Start()
    {
        Timer += Time.deltaTime;
        _camera = Camera.main.transform;
        sizeX = Layer_Objects[0].transform.localScale.x;
        boundSizeX = Layer_Objects[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        for (int i=0;i<6;i++){
            startPos[i] = _camera.position.x;
        }
    }

    void Update(){
        //Moving camera
        if (Camera_Move){
        _camera.position += Vector3.right * Time.deltaTime * Camera_MoveSpeed;
        }
        for (int i=0;i<6;i++){
            float temp = (_camera.position.x * (1-Layer_Speed[i]) );
            float distance = _camera.position.x  * Layer_Speed[i];

            Layer_Objects[i].transform.position = new Vector2 (startPos[i] + distance, _camera.position.y);
            Layer_Objects[4].transform.position = new Vector2(startPos[i] + distance, _camera.position.y + BG4_yOffset);
            Layer_Objects[5].transform.position = new Vector2(startPos[i] + distance, _camera.position.y + BG5_yOffset);

            if (temp > startPos[i] + boundSizeX*sizeX){
                startPos[i] += boundSizeX*sizeX;
            }else if(temp < startPos[i] - boundSizeX*sizeX){
                startPos[i] -= boundSizeX*sizeX;
            }
            
        }
        //if (!GameControl.IsGameStart) return;
        //ChangeSpeed();
    }
    public void ResetSpeed()
    {
        Timer = 0;
        Camera_MoveSpeed = 1.5f;
        Layer_Speed[5] = 1f;
    }

    void ChangeSpeed()
    {
        Timer += Time.deltaTime;
        if (Timer >= timeLimit)
        {
            Timer = 0;
            Camera_MoveSpeed += 0.15f;
            Layer_Speed[5] += 0.15f;
        }
    }
}

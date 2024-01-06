using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour
{
    public int Hp = 3;
    public Vector2 rangePoint;
    public Vector2 size;
    public Transform pivot;

    public GameObject Hp1;
    public GameObject Hp2;
    public GameObject Hp3;

    void Start()
    {
        Hp1.SetActive(true);
        Hp2.SetActive(true);
        Hp3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.IsGameStart) return;
        HpControl();
        HpUIControl();
    }

    public void GameReset()
    {
        Hp = 3;
        Hp1.SetActive(true);
        Hp2.SetActive(true);
        Hp3.SetActive(true);
    }
    public void HpUIControl()
    {
        if(Hp == 2)
        {
            Hp3.SetActive(false);
        }
        else if(Hp == 1)
        {
            Hp2.SetActive(false);
        }
        else if(Hp <= 0)
        {
            Hp1.SetActive(false);
        }
    }
    public void HpControl()
    {
        if (Hp <= 0)
        {
            GameControl.canSpawn = false;
        }
        if (Hp <= 0) return;
        if (Physics2D.OverlapBoxAll((Vector2)pivot.transform.position + rangePoint, size, 0) != null)
        {
            Collider2D[] nails = Physics2D.OverlapBoxAll((Vector2)pivot.transform.position + rangePoint, size, 0);
            if (nails != null)
            {
                foreach (Collider2D c in nails)
                {
                    if (c.tag == "Nail")
                    {
                        Debug.Log("Damage: " + c.name);
                        Hp -= 1;
                        Debug.Log("Damage. PlayerHp: " + Hp);
                        c.gameObject.SetActive(true);
                        Destroy(c.gameObject);
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)pivot.transform.position + rangePoint, size);
    }
}

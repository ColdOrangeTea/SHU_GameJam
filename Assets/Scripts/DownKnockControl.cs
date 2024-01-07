using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownKnockControl : MonoBehaviour
{
    public BananaControl BananaControl;
    
    public Transform pivot;
    public Vector2 rangePoint;
    public Vector2 size;
  
    public ParticleSystem smoke;
    public Animator anim;

    public AudioSource source;
    public AudioClip clip;

    void Start()
    {
      
    }

    void Update()
    {
        OnKnockNail();
        ChangeAnimSkin();
    }

    void ChangeAnimSkin()
    {
        if (GameControl.KnockCounts >= 45)
        {
            anim.SetBool("hitted3Idle", true);
            anim.SetBool("hittedIdle", false);
            anim.SetBool("hitted2Idle", false);
        }
        else if (GameControl.KnockCounts >= 30)
        {
            anim.SetBool("hitted2Idle", true);
            anim.SetBool("hittedIdle", false);
            anim.SetBool("hitted3Idle", false);
        }
        else if (GameControl.KnockCounts >= 15)
        {
            anim.SetBool("hittedIdle", true);
            anim.SetBool("hitted2Idle", false);
            anim.SetBool("hitted3Idle", false);
        }
        else if (GameControl.KnockCounts < 15)
        {
            anim.SetBool("hittedIdle", false);
            anim.SetBool("hitted2Idle", false);
            anim.SetBool("hitted3Idle", false);
        }
    }
    void ChangeKnockAnim()
    {
        if (GameControl.KnockCounts >= 45)
        {
            anim.SetTrigger("hitted3Knock");
        }
        else if (GameControl.KnockCounts >= 30)
        {
            anim.SetTrigger("hitted2Knock");
        }
        else if (GameControl.KnockCounts >= 15)
        {
            anim.SetTrigger("hittedKnock");
        }
        else if (GameControl.KnockCounts < 15)
        {
            anim.SetTrigger("knock");
        }
    }
    void OnKnockNail()
    {
        if (!GameControl.IsGameStart) return;
        if (BananaControl.IsPlayerPosUp) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeKnockAnim();
            Collider2D nail = Physics2D.OverlapBox((Vector2)pivot.transform.position + rangePoint, size, 0);
            if (nail != null)
            {
                if (nail.tag == "Nail")
                {
                    Debug.Log("Knock Nail.");
                    smoke.Play();
                    source.PlayOneShot(clip);
                    GameControl.TotalScore += GameControl.ADDSCORE;
                    GameControl.KnockCounts += 1;
                    nail.gameObject.SetActive(true);
                    Destroy(nail.gameObject);
                }
            }
        }
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)pivot.transform.position + rangePoint, size);
    }

}

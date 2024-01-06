using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpKnockControl : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        OnKnockNail();
        ChangeAnimSkin();
    }

    void ChangeAnimSkin()
    {
        if (GameControl.KnockCounts >= 15)
        {
            anim.SetBool("hittedIdle", true);
        }
        else if (GameControl.KnockCounts < 15)
        {
            anim.SetBool("hittedIdle", false);
        }
    }
    void ChangeKnockAnim()
    {
        if (GameControl.KnockCounts >= 15)
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
        if (!BananaControl.IsPlayerPosUp) return;
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

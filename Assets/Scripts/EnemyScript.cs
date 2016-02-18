using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

    public string path;
    public int time;

    private Animator animator;
    private bool dead;
    private bool touchingEnemy;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        dead = false;
        if (path != "") {
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(path),
                "time", time,
                "looptype", iTween.LoopType.loop,
                "orientToPath", true, 
                "delay", 0,
                "easetype", iTween.EaseType.linear,
                "moveToPath", false));

            animator.SetBool("isWalking", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //void OnTriggerEnter(Collider other)
    //{
    //    if (Input.GetKey(KeyCode.K))
    //    {
    //        dead = true;
    //        animator.Play("dead");
    //        StartCoroutine(KillOnAnimationEnd());
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dead)
        {
            if (other.GetComponent<PlayerController>().isAttacking())
            {
                killEnemy();
                //GetComponent<Rigidbody>()
                //    .AddExplosionForce(2000.0f, other.transform.position + (Vector3.down * 0.1f), 50000000.0f);
            } else {
                killEnemy();
                other.GetComponent<PlayerController>().killPlayer();
            }
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        touchingEnemy = false;
    //    }
    //}

    public void KillOnAnimationEnd()
    {
        //yield return new WaitForSeconds(3.95f);
        this.gameObject.SetActive(false);
    }

    private void killEnemy() {
        Debug.Log("ENEMY DEAD");
        dead = true;
        if (path != "")
        {
            iTween.Stop(this.gameObject);
        }
        animator.SetBool("isDead", true);
        //StartCoroutine(KillOnAnimationEnd());
    }
}

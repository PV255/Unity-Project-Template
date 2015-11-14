using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

    public string path;

    private Animator animator;
    private bool dead;
    private bool touchingEnemy;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        dead = false;
        if (path != null) {
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(path),
                "time", 5,
                "looptype", iTween.LoopType.loop,
                "orientToPath", true, 
                "delay", 0,
                "easetype", iTween.EaseType.linear));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (touchingEnemy)
            {
                Debug.Log("ENEMY DEAD");
                dead = true;
                animator.Play("dead");
                //StartCoroutine(KillOnAnimationEnd());
            }

        }
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
        if (other.CompareTag("Player"))
        {
            touchingEnemy = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchingEnemy = false;
        }
    }

    private void KillOnAnimationEnd()
    {
        //yield return new WaitForSeconds(3.95f);
        this.gameObject.SetActive(false);
    }
}

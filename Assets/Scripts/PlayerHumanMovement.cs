using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriterend;
    private BoxCollider2D boxcollider;

    public bool onGround = true;
    public float speed = 2f;
    public float speedghost = 2f;
    public float jumpspeed = 200f;
    public bool isGhost = false;

	void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriterend = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
	}
	void Update () {
        int horizontal = 0;
        int vertical = 0;
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isGhost)
            {
                if (!boxcollider.IsTouchingLayers(LayerMask.GetMask("BlockingLayer")))
                {
                    rb.isKinematic = false;
                    boxcollider.isTrigger = false;
                    spriterend.color = Color.white;
                    isGhost = !isGhost;
                }
                else
                { }
            }
            else
            {
                boxcollider.isTrigger = true;
                rb.isKinematic = true;
                spriterend.color = new Color(0f, 1f, 1f, 0.5f);
                isGhost = !isGhost;
            }
            animator.SetBool("isGhost", isGhost);
        }

        // MOVEMENT
        if (isGhost)
        {
            if (horizontal != 0)
            {
                if (horizontal > 0)
                    transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                else
                    transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
                transform.Translate(speedghost * Time.deltaTime, 0f, 0f);
            }
            if (vertical > 0)
            {
                transform.Translate(0f, speedghost * Time.deltaTime, 0f);
            }
            else if (vertical < 0)
            {
                transform.Translate(0f, -speedghost * Time.deltaTime, 0f);
            }
        }
        else
        {
            if (Physics2D.Raycast(transform.position, Vector3.down, 1 + boxcollider.size.y / 2, LayerMask.GetMask("BlockingLayer")))
                onGround = true;
            else
                onGround = false;
            animator.SetBool("onGround", onGround);
            

            if (horizontal == 0)
            {
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isRunning", true);
                if (horizontal > 0)
                    transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                else
                    transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
                if (
                    !Physics2D.Raycast(transform.position + new Vector3(0f, boxcollider.size.x / 2, 0f), transform.right, 1 + boxcollider.size.x / 2, LayerMask.GetMask("BlockingLayer")) &&
                    !Physics2D.Raycast(transform.position - new Vector3(0f, boxcollider.size.x / 2, 0f), transform.right, 1 + boxcollider.size.x / 2, LayerMask.GetMask("BlockingLayer"))
                    )
                    transform.Translate(speed * Time.deltaTime, 0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("want to jump");
                if (onGround)
                {
                    Debug.Log("jumped");
                    rb.AddForce(jumpspeed * Vector2.up);
                }
            }
        }
	}
}

using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    Rigidbody m_Rigidbody;
    private GameObject cam;
    private float gravity;

    bool is_grounded;
    bool is_jumping;

    public float maxJump;
    public float speed;

    private float currentMaxY;
    private Animator m_Animator;
    private Transform cameraPivot;

    private bool attacking;
    private bool dying;


    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        gravity = 5f;

        cam = GameObject.Find("MainCamera");
        is_jumping = false;
        attacking = false;
        m_Animator = GetComponent<Animator>();
        cameraPivot = transform.Find("CameraPivot");
        dying = false;
    }

    void Update()
    {
        if (is_grounded)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                is_grounded = false;
                is_jumping = true;
                currentMaxY = m_Rigidbody.transform.position.y + maxJump;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.K))
        {
            attacking = true;
            m_Animator.SetTrigger("attack");
        }
    }

    private void FixedUpdate()
    {
        if (dying) {
            return;
        }

        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 flatFWD = cam.transform.forward;
        flatFWD.y = 0;
        flatFWD.Normalize();

        Vector3 flatRT = cam.transform.right;
        flatRT.y = 0;
        flatRT.Normalize();

        Vector3 move = (v * flatFWD + h * flatRT) * speed;

        CapsuleCollider col = gameObject.GetComponent<CapsuleCollider>();
        PhysicMaterial phys = col.material;
        bool useHigherFriction = (v == 0) && (h == 0); // move.magnitude < 0.001f;

        if (useHigherFriction){
            phys.staticFriction = 10;
        }else{
            phys.staticFriction = 0;
        }

        m_Rigidbody.velocity = move;
        
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(m_Rigidbody.transform.forward, move, step, 0.0F);
        //Debug.DrawLine(m_Rigidbody.transform.position, newDir, Color.red);
        newDir.Scale(new Vector3(1, 0, 1));
        newDir.Normalize();
        transform.rotation = Quaternion.LookRotation(newDir);
       

        if (is_jumping)
        {
            transform.position += Vector3.up * 10f * Time.deltaTime;

            if (transform.position.y > currentMaxY) is_jumping = false;
        }

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.05f), Vector3.down, out hitInfo, 0.3f))
        {
            is_grounded = true;
        }
        
        m_Rigidbody.velocity += Vector3.down * gravity;

        //---------
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);

        // send input and other state parameters to the animator
        UpdateAnimator(move);
    }

    void UpdateAnimator(Vector3 move)
    {
        if (!Vector3.Equals(move, Vector3.zero))
            m_Animator.SetBool("moving", true);
        else
            m_Animator.SetBool("moving", false);

        if (is_jumping)
            m_Animator.SetBool("jumping", true);
        else
            m_Animator.SetBool("jumping", false);


        //// update the animator parameters
        //m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        //m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        //m_Animator.SetBool("Crouch", m_Crouching);
        //m_Animator.SetBool("OnGround", m_IsGrounded);
        //if (!m_IsGrounded)
        //{
        //    m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        //}

        //// calculate which leg is behind, so as to leave that leg trailing in the jump animation
        //// (This code is reliant on the specific run cycle offset in our animations,
        //// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        //float runCycle =
        //    Mathf.Repeat(
        //        m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        //float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
        //if (m_IsGrounded)
        //{
        //    m_Animator.SetFloat("JumpLeg", jumpLeg);
        //}

        //// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        //// which affects the movement speed because of the root motion.
        //if (m_IsGrounded && move.magnitude > 0)
        //{
        //    m_Animator.speed = m_AnimSpeedMultiplier;
        //}
        //else
        //{
        //    // don't use that while airborne
        //    m_Animator.speed = 1;
        //}
    }

    public bool isAttacking() {
        return attacking;
    }

    public void StopAttacking()
    {
        this.attacking = false;
    }

    public void StopDying() {
        dying = false;
        m_Animator.SetBool("moving", false);
        m_Animator.SetBool("jumping", false);
    }

    public void killPlayer()
    {
        GameManager.Instance.GetComponent<GameManager>().DestroyLife();
        m_Animator.SetTrigger("isDead");
        dying = true;
    }
}

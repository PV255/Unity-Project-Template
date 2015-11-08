using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    Rigidbody m_Rigidbody;
    public GameObject cam;
    private float gravity;

    bool is_grounded;
    bool is_jumping;

    public float maxJump;
    public float speed;

    private float currentMaxY;

    void Start()
    {
        //camera = GameObject.Find("cstmCamera");
        m_Rigidbody = GetComponent<Rigidbody>();
        gravity = 5f;

        is_jumping = false;
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
    }

    private void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");


        m_Rigidbody.velocity = (v * cam.transform.forward + h * cam.transform.right) * speed;

        /*Transform dir = camera.transform;
 
        //m_Rigidbody.velocity = (v * Vector3.forward + h * Vector3.right) * 10;
        m_Rigidbody.velocity = (v * dir.forward + h * dir.right) * 10;*/

        if (is_jumping)
        {
            transform.position += Vector3.up * 0.18f;

            if (transform.position.y > currentMaxY) is_jumping = false;
        }

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.3f))
        {
            is_grounded = true;
        }


        m_Rigidbody.velocity += Vector3.down * gravity;
    }
}

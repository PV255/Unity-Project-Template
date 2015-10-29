using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CstmController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    GameObject camera;
    public float gravity;
    public float nextJump;

    bool m_Jump;

    public float maxJump;
    public float speed;

    void Start()
    {
        camera = GameObject.Find("cstmCamera");
        m_Rigidbody = GetComponent<Rigidbody>();
        gravity = 3.7f;
        nextJump = Time.time;

        m_Jump = false;
    }

    void Update()
    {
        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }

    private void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");


        m_Rigidbody.velocity = (v * Vector3.forward + h * Vector3.right) * speed;

        /*Transform dir = camera.transform;
 
        //m_Rigidbody.velocity = (v * Vector3.forward + h * Vector3.right) * 10;
        m_Rigidbody.velocity = (v * dir.forward + h * dir.right) * 10;*/

        if (m_Jump)
        {
            transform.position += Vector3.up * 0.16f;

            if (transform.position.y > maxJump) m_Jump = false;
        }


        m_Rigidbody.velocity += Vector3.down * gravity;
    }

}

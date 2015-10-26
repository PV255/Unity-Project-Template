using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class cstmController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    float gravity;
    float nextJump;

    // Use this for initialization
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        gravity = 10;
        nextJump = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
            
    }

    private void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        m_Rigidbody.velocity = (v * Vector3.forward + h * Vector3.right) * 7;

        if (CrossPlatformInputManager.GetButtonDown("Jump") && Time.time > nextJump)
        {
            // m_Rigidbody.velocity += Vector3.up * 20f;
            // m_Rigidbody.AddForce(Vector3.up * 10);
            transform.position += Vector3.up * 3;
            nextJump = Time.time + 0.3f;
        }

        if (Time.time > nextJump)
        {
            m_Rigidbody.velocity += Vector3.down * gravity;
        }

    }
}

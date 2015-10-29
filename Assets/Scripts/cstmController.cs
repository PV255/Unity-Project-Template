using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class cstmController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    GameObject camera;
    public float gravity;
    public float nextJump;

    bool grounded;

    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("cstmCamera");
        m_Rigidbody = GetComponent<Rigidbody>();
        gravity = 10;
        nextJump = Time.time;
        grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
            
    }

    void OnCollisionStay(Collision c)
    {
        foreach(ContactPoint contact in c.contacts) {
            if (contact.point.y < transform.position.y)
            {
                grounded = true;
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        Transform dir = camera.transform;

        //m_Rigidbody.velocity = (v * Vector3.forward + h * Vector3.right) * 10;
        m_Rigidbody.velocity = (v * dir.forward + h * dir.right) * 10;

        if (CrossPlatformInputManager.GetButtonDown("Jump") && grounded)
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z);
            m_Rigidbody.AddForce(new Vector3(0f, 100, 0f), ForceMode.VelocityChange);
            //grounded = false;
        }

        m_Rigidbody.AddForce(new Vector3(0f, -500, 0f), ForceMode.Acceleration);    
    }

}

using UnityEngine;
using System.Collections;

public class CF02 : MonoBehaviour
{

    public Transform target;
    float moveSpeed = 8f;
    float rotationSpeed = 3f;
    Transform myTransform;
    State state;
    public enum State
    {
        Idle,
        Way1,
        Way2,
        Way3,
        Way4
    }
    void Awake()
    {
        myTransform = transform;
    }
    // Use this for initialization
    void Start()
    {
        Debug.Log("Scripts Strart");
        state = State.Idle;
        //target = GameObject.FindWithTag("W1").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Way1:
                waypoint1();
                break;
            case State.Way2:
                waypoint2();
                break;
            case State.Way3:
                waypoint3();
                break;
            case State.Way4:
                waypoint4();
                break;
        }


    }
    public void Idle()
    {
        state = State.Way1;
    }
    void waypoint1()
    {

        //target = GameObject.FindWithTag("W1").transform;
        float distance = Vector3.Distance(myTransform.position, target.transform.position);
        //animation.Play("Walk");
        Debug.DrawLine(target.transform.position, myTransform.position, Color.red);
        //myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
        myTransform.LookAt(new Vector3(target.position.x, target.position.y + 2f, target.position.z));

        //move towards the player
        if (distance > 5f)
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        //if (distance < 2f)
        //     state = State.Way2;
    }
    void waypoint2()
    {
        //target = GameObject.FindWithTag("W2").transform;
        float distance = Vector3.Distance(myTransform.position, target.transform.position);
        //animation.Play("Walk");
        Debug.DrawLine(target.transform.position, myTransform.position, Color.red);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        //move towards the player
        myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        if (distance < 2f)
            state = State.Way3;
    }
    void waypoint3()
    {
        //target = GameObject.FindWithTag("W3").transform;
        float distance = Vector3.Distance(myTransform.position, target.transform.position);
        //animation.Play("Walk");
        Debug.DrawLine(target.transform.position, myTransform.position, Color.red);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        //move towards the player
        myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        if (distance < 2f)
            state = State.Way4;
    }
    void waypoint4()
    {
        //target = GameObject.FindWithTag("W4").transform;
        float distance = Vector3.Distance(myTransform.position, target.transform.position);
        //animation.Play("Walk");
        Debug.DrawLine(target.transform.position, myTransform.position, Color.red);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        //move towards the player
        myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        if (distance < 2f)
            state = State.Way1;

        //Destroy (abc);
        //Application.LoadLevel ("result");

    }
}
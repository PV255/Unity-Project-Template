using UnityEngine;
using System.Collections;


public class PlayerLimit : MonoBehaviour
{
    
    public Material normal;
    public Material attack;

    private Vector3 startPos;
    private Vector3 cameraStartPos;
    private GameObject mainCamera;

    private Animator animator;
    private Renderer bodyRenderer;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();
        //bodyRenderer = GameObject.Find("EthanBody").GetComponent<Renderer>();
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -30) // temp edit for Earth level
        {
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        transform.position = startPos;
        mainCamera.GetComponent<CameraFollow>().ResetCamera(cameraStartPos);
    }

    public void SetStartPos(Vector3 position, Vector3 cameraPos)
    {
        startPos = position;
        cameraStartPos = cameraPos;
    }
}

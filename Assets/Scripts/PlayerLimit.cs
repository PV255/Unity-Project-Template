using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLimit : MonoBehaviour
{
    public Text scoreText;
    public Material normal;
    public Material attack;

    private Vector3 startPos;
    private GameObject mainCamera;
    private int score;
    private Animator animator;
    private Renderer bodyRenderer;
   

    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        startPos = transform.position;
        score = 0;
        SetScoreText();
        animator = GetComponent<Animator>();
        bodyRenderer = GameObject.Find("EthanBody").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5)
        {
            ResetLevel();
        }

        if (Input.GetKey(KeyCode.K)) {
            Debug.Log("ATTACK!!!");
            //animator.Play("Crouching");
            bodyRenderer.sharedMaterial = attack;
        } else {
            bodyRenderer.sharedMaterial = normal;
        }
    }

    public void AddScore(int ammount)
    {
        score += ammount;
        SetScoreText();
    }

    public void SetStartPos(Vector3 position)
    {
        startPos = position;
    }

    private void ResetLevel()
    {
        transform.position = startPos;
        mainCamera.GetComponent<CameraFollow>().ResetCamera();
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + score; 
    }
}

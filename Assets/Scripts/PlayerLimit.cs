using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLimit : MonoBehaviour
{
    public Text scoreText;

    private Vector3 startPos;
    private GameObject mainCamera;
    private int score;
   

    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        startPos = transform.position;
        score = 0;
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5)
        {
            ResetLevel();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Points"))
        {
            collision.gameObject.SetActive(false);
            score += 1;
            SetScoreText();
        }

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

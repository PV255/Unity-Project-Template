using UnityEngine;
using System.Collections;

public class ItemLife : MonoBehaviour {

    float startY;
    float maxYOffset = 0.1f;

    void Start(){
        startY = transform.position.y;
    }

    Vector3 up = new Vector3(0,1,0);

    // Update is called once per frame
    void Update(){
        transform.RotateAround(transform.position, up, Time.deltaTime * 20f);

        Vector3 pos = transform.position;
        pos.y = startY + Mathf.Sin(Time.time) * maxYOffset;
        transform.position = pos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            GameManager.Instance.AddLife();
        }
    }
}

using UnityEngine;
using System.Collections;

public class LoosePlatform : MonoBehaviour {
    private float origY;

    public float timeFallAfter = 2;
    public float timeStayDown = 5;

    private float timeFallAt = -1;
    private float timeRiseAt = -1;

    // Use this for initialization
    void Start () {
        origY = gameObject.transform.position.y;
	}

    void Update() {
        Vector3 curPos = gameObject.transform.position;
        float targetY = curPos.y;

        if ((timeFallAt != -1) && (timeFallAt < Time.time)) {
            targetY = origY - 10 * 1;
            timeFallAt = -1;
        }

        if ((timeRiseAt != -1) && (timeRiseAt < Time.time))
        {
            targetY = origY;
            timeRiseAt = -1;
        }

        //curPos.y = curPos.y + Time.deltaTime * (targetY - curPos.y);
        curPos.y = targetY;
        gameObject.transform.position = curPos;
    }

    void OnCollisionEnter(Collision col){
        if (col.other.CompareTag("Player")) {
            timeFallAt = Time.time + timeFallAfter;
            timeRiseAt = timeFallAt + 5;
        }
    }
}

using UnityEngine;
using System.Collections;

public class LoosePlatform : MonoBehaviour {
    private Vector3 origPos;
    private Quaternion origRot;

    private float timeFallAfter = 1;
    private float timeStayDown = 7.5f;

    private float timeFallAt = -1;
    private float timeRiseAt = -1;

    // Use this for initialization
    void Start () {

        
        
        origPos = gameObject.transform.position;
        origRot = gameObject.transform.rotation;
    }

    void Update() {
        if ((timeFallAt != -1) && (timeFallAt < Time.time)) {
            gameObject.AddComponent<Rigidbody>();
            timeFallAt = -1;
        }

        if ((timeRiseAt != -1) && (timeRiseAt < Time.time)){
            DestroyImmediate(gameObject.GetComponent<Rigidbody>());

            gameObject.transform.position = origPos;
            gameObject.transform.rotation = origRot; 
            timeRiseAt = -1;
        }
    }

    void OnCollisionEnter(Collision col){
        if (col.other.CompareTag("Player") && timeFallAt == -1) {
            timeFallAt = Time.time + timeFallAfter;
            timeRiseAt = timeFallAt + timeStayDown;
        }
    }
}

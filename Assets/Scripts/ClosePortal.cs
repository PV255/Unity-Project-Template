using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ClosePortal : MonoBehaviour {
    public AddPortal AddPortalScript;

    void Start() { 
        
    }
    public GameObject inputPortal;
    public GameObject outputPortal;
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        List<AddPortal.Tuple> portals = AddPortalScript.portals;
        Debug.Log("collision with: " + coll.name);
        if ((coll.name.StartsWith(outputPortal.name))) {
            
            PortalId something = coll.gameObject.GetComponent<PortalId>();
            int id = something.id;
            Debug.Log("Collision portal id: " + id);
            AddPortal.Tuple onIndex = null;
            foreach (AddPortal.Tuple tup in portals)
            {
                if (tup.outputPortal.getId() == id)
                {
                    onIndex = tup;
                    break;
                }
            }
            Destroy(coll.gameObject);
            Destroy(onIndex.inputPortal.InputPortalGO);
        }
        
    }
}

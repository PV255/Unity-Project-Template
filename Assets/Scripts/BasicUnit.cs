using UnityEngine;
using System.Collections;

public class BasicUnit : MonoBehaviour {

    public int reach; //jak daleko muze jednotka dojit
    public int speed; //rychlost pohybu pres policka
    public int side; //strana za za kterou jednotka hraje
    public ArrayList path;
    private bool move = false; //je v pohybu
    private Vector3 target; //souradnice policek na ktera se jednotka postupne presouva
    private GameObject targetTile; //reference na policko kam se ve finale presunem


    public void setPath(ArrayList pathList)
    {
        path = pathList;
    }

    public void proceedPath()
    {
        foreach (GameObject obj in path)
        {
            target = obj.transform.position;
            if (path.Count == 1)
                targetTile = obj;
            break;
        }
        path.RemoveAt(0);
        move = true;
    }

    public void finishPath()
    {
        move = false;
        path.Clear();
        HexGridFieldManager.instance.selectedHex.unHighlightTile(true);
        HexGridFieldManager.instance.selectedHex.selectNeighbours(HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().reach, false);
        targetTile.GetComponent<HexTile>().unit = HexGridFieldManager.instance.selectedHex.unit;
        HexGridFieldManager.instance.selectedHex.unit = null;
        HexGridFieldManager.instance.selectedHex = null;
    }

    public bool isMoving()
    {
        return move;
    }

	// Use this for initialization
	void Start () {
        path = new ArrayList();
	}

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (transform.position.Equals(target))
            {
                if (path.Count == 0)
                    finishPath();
                else
                    proceedPath();
            }
        }
    }
}

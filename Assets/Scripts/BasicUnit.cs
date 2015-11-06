using UnityEngine;
using System.Collections;

public class BasicUnit : MonoBehaviour {

    public int reach; //jak daleko muze jednotka dojit
    public int speed; //rychlost pohybu pres policka
    public int side; //strana za za kterou jednotka hraje
    public int rank;
    public ArrayList path;
    private bool moving = false, attacking = false; //je v pohybu
    private Vector3 target; //souradnice policek na ktera se jednotka postupne presouva
    private HexTile targetTile, attackingTile; //reference na policko kam se ve finale presunem
    public Mesh mainMesh;
    public Mesh hiddenMesh;


    public void setPath(ArrayList pathList)
    {
        path = (ArrayList) pathList.Clone();
    }

    public void proceedPath()
    {
        foreach (HexTile obj in path)
        {
            target = obj.transform.position;
            targetTile = obj;
            break;
        }
        path.RemoveAt(0);
        moving = true;
    }

    public void proceedAttack(HexTile destinationTile)
    {
        attackingTile = destinationTile;
        attacking = true;
        path.RemoveAt(path.Count - 1);
        if (path.Count > 0) 
            proceedPath();
        else
            attackTile();
    }

    public void attackTile()
    {
        moving = false;
        attacking = false;
        
        if (rank > attackingTile.unit.GetComponent<BasicUnit>().rank)
        {
            
            Destroy(attackingTile.unit);
            path.Add(attackingTile);
            proceedPath();
        }
        else if(rank < attackingTile.unit.GetComponent<BasicUnit>().rank)
        {
            
            Destroy(HexGridFieldManager.instance.selectedHex.unit);
            path.Clear();
            HexGridFieldManager.instance.selectedHex.unHighlightUnitTile();
            HexGridFieldManager.instance.selectedHex.unHighlightTile(true);
            HexGridFieldManager.instance.selectedHex.selectNeighbours(HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().reach, false);
            HexGridFieldManager.instance.selectedHex.unit = null;
            HexGridFieldManager.instance.selectedHex = null;
        }
        else
        {
            Destroy(attackingTile.unit);
            Destroy(HexGridFieldManager.instance.selectedHex.unit);
            path.Clear();
            HexGridFieldManager.instance.selectedHex.unHighlightUnitTile();
            HexGridFieldManager.instance.selectedHex.unHighlightTile(true);
            HexGridFieldManager.instance.selectedHex.selectNeighbours(HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().reach, false);
            HexGridFieldManager.instance.selectedHex.unit = null;
            HexGridFieldManager.instance.selectedHex = null;
        }
    }

    public void finishPath()
    {
        moving = false;
        path.Clear();
        HexGridFieldManager.instance.selectedHex.unHighlightTile(true);
        HexGridFieldManager.instance.selectedHex.selectNeighbours(HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().reach, false);
        targetTile.unit = HexGridFieldManager.instance.selectedHex.unit;
        targetTile.highlightUnitTile();
        HexGridFieldManager.instance.selectedHex.unit = null;
        HexGridFieldManager.instance.selectedHex.unHighlightUnitTile();
        if (attacking)
        {
            HexGridFieldManager.instance.selectedHex = targetTile;
            attackTile();
        }
        else
        {
            HexGridFieldManager.instance.selectedHex = null;
        }
    }

    public bool isMoving()
    {
        return moving;
    }

    public void hideUnit()
    {
        GetComponent<MeshFilter>().mesh = hiddenMesh;
    }

    public void unhideUnit()
    {
        GetComponent<MeshFilter>().mesh = mainMesh;
    }

    // Use this for initialization
    void Start () {
        path = new ArrayList();
	}

    // Update is called once per frame
    void Update()
    {
        if (moving)
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

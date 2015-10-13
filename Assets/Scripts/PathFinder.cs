using UnityEngine;
using System.Collections;


/*
Trida slouzi pro vypocet cesty z bodu A do bodu B
*/
public class PathFinder : MonoBehaviour {

    public ArrayList visitedTiles, currentPath;
    private HexTile start;

    public static PathFinder instance = null;

    void inicialization()
    {
        instance = this;
    }

    float getDistance(HexTile from, HexTile to)
    {
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(from.boardPosition.x - to.boardPosition.x), 2) + Mathf.Pow(Mathf.Abs(from.boardPosition.y - to.boardPosition.y), 2));
    }

    public void buildPath(HexTile pFrom, HexTile destination)
    {
        foreach (GameObject til in currentPath)
        {
            til.GetComponent<HexTile>().changeColor(Color.yellow);
        }
        currentPath.Clear();
        start = pFrom;
        computePath(start, destination);
    }

    void computePath(HexTile from, HexTile destination)
    {
        float bestDistance = 99999;
        GameObject bestTile = null;
        foreach (GameObject til in from.AllNeighbours)
        {
            if (til.GetComponent<HexTile>().isPassable() && !visitedTiles.Contains(til))
            {
                float currentDistance = getDistance(til.GetComponent<HexTile>(), destination);
                if (currentDistance < bestDistance)
                {
                    bestDistance = currentDistance;
                    bestTile = til;
                }
            }
        }
        if (bestTile != null)
        {
            visitedTiles.Add(bestTile);
            currentPath.Add(bestTile);
            if (!bestTile.GetComponent<HexTile>().boardPosition.Equals(destination.boardPosition))
            {
                computePath(bestTile.GetComponent<HexTile>(), destination);
            }
            else
            {
                foreach (GameObject til in currentPath)
                {
                    til.GetComponent<HexTile>().changeColor(Color.blue);
                }
                start.unit.GetComponent<BasicUnit>().setPath(currentPath);
                visitedTiles.Clear();
            }
        }
        else
        {
            currentPath.Clear();
            computePath(start, destination);
        }
    }

    // Use this for initialization
    void Start () {
        inicialization();
        visitedTiles = new ArrayList();
        currentPath = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

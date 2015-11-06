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
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(from.transform.position.x - to.transform.position.x), 2) + Mathf.Pow(Mathf.Abs(from.transform.position.z - to.transform.position.z), 2));
    }

    public void reset()
    {
        visitedTiles.Clear();
        currentPath.Clear();
        start = null;
    }

    public void buildPath(HexTile pFrom, HexTile destination, Color c)
    {
        foreach (HexTile til in currentPath)
        {
            til.changeBackColor();
        }
        currentPath.Clear();
        start = pFrom;
        computePath(start, destination, c);
    }

    void computePath(HexTile from, HexTile destination, Color c)
    {
        float bestDistance = 99999;
        HexTile bestTile = null;
        foreach (HexTile til in from.AllNeighbours)
        {
            if ((til.isPassable() || til == destination) && !visitedTiles.Contains(til) && til.isHighlighted())
            {
                float currentDistance = getDistance(til, destination);
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
            if (!bestTile.boardPosition.Equals(destination.boardPosition))
            {
                computePath(bestTile, destination, c);
            }
            else
            {
                foreach (HexTile til in currentPath)
                {
                    til.changeColor(c);
                }
                start.unit.GetComponent<BasicUnit>().setPath(currentPath);
                visitedTiles.Clear();
            }
        }
        else
        {
            currentPath.Clear();
            computePath(start, destination, c);
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

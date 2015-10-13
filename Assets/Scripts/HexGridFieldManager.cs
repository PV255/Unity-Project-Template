using UnityEngine;
using System.Collections;

public struct Point
{
    public int x, y;

    public Point(int pX, int pY)
    {
        x = pX;
        y = pY;
    }
}

public class HexGridFieldManager : MonoBehaviour {

    public GameObject hex;
    public GameObject unitObject;
    public GameObject unitObject2;
    public Vector2 gridSize;
    public HexTile selectedHex;

    public static HexGridFieldManager instance = null;

    void inicialization()
    {
        instance = this;
    }

    void createGrid()
    {
        Hashtable board = new Hashtable();
        float offsetX = 0;
        for (int j = 0; j < gridSize.y; j++)
        {
            if (j % 2 == 0)
            {
                offsetX = hex.GetComponent<Renderer>().bounds.size.x / 2;
            }
            else
            {
                offsetX = 0;
            }
            for (int i = 0; i < gridSize.x; i++)
            {
                GameObject oneHex = (GameObject)Instantiate(hex);
                oneHex.transform.position = new Vector3(i * hex.GetComponent<Renderer>().bounds.size.x + offsetX, 0, j * (hex.GetComponent<Renderer>().bounds.size.z * 3 / 4));
                if((j == 4 && i <= 9 && i >= 0))
                    oneHex.GetComponent<HexTile>().setUnit((GameObject)Instantiate(unitObject));
                if((j == 2 && i == 5))
                    oneHex.GetComponent<HexTile>().setUnit((GameObject)Instantiate(unitObject2));
                oneHex.GetComponent<HexTile>().setBoardPosition(i, j);
                board.Add(new Point(i, j), oneHex);
            }
        }

        foreach (GameObject tile in board.Values)
        {
            tile.GetComponent<HexTile>().FindNeighbours(board, gridSize);
        }
    }

    // Use this for initialization
    void Start () {
        inicialization();
        createGrid();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

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
    public GameObject[] unitObject;
    public Vector2 gridSize;
    public HexTile selectedHex;
    public int playerTurn = 0;
    public int numberOfActions = 1;
    private Hashtable board;


    public static HexGridFieldManager instance = null;

    void inicialization()
    {
        instance = this;
    }

    public void nextTurn()
    {
        playerTurn = (playerTurn == 0 ? 1 : 0);
        numberOfActions = 1;

        for(int j = 0; j < gridSize.y; j++)
        {
            for (int i = 0; i < gridSize.x; i++)
            {
                HexTile tile = ((GameObject)board[new Point(i, j)]).GetComponent<HexTile>();
                if (tile.unit != null)
                {
                    if (tile.unit.GetComponent<BasicUnit>().side == playerTurn)
                    {
                        tile.unit.GetComponent<BasicUnit>().unhideUnit();
                        tile.highlightUnitTile(Color.green);
                    }
                    else
                    {
                        tile.unit.GetComponent<BasicUnit>().hideUnit();
                        tile.unHighlightUnitTile();
                    }
                }
            }
        }
    }

    void createGrid()
    {
        board = new Hashtable();
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
                if ((j == 9))
                {
                    oneHex.GetComponent<HexTile>().setUnit((GameObject)Instantiate(unitObject[i]));
                    oneHex.GetComponent<HexTile>().unit.GetComponent<BasicUnit>().side = 1;
                }
                if ((j == 0))
                {
                    oneHex.GetComponent<HexTile>().setUnit((GameObject)Instantiate(unitObject[i]));
                    oneHex.GetComponent<HexTile>().unit.GetComponent<BasicUnit>().side = 0;
                }
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

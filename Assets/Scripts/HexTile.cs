using UnityEngine;
using System.Collections;

public class HexTile : MonoBehaviour {

    public GameObject unit;
    public Point boardPosition;

    public ArrayList AllNeighbours;

    public Material fullMaterial;
    public Material defaultMaterial;
    public Sprite fullSprite;
    public Sprite outlineSprite;

    private bool selected = false, neighbourHighlight = false;
    Color orange = new Color(255f / 255f, 127f / 255f, 0, 127f / 255f);
    Color orange2 = new Color(255f / 255f, 191f / 255f, 54f / 255f, 127f / 255f);
    private Color previousColor, highlightedColor;

    public void changeColor(Color color)
    {
        GetComponent<SpriteRenderer>().sprite = fullSprite;
        GetComponent<Renderer>().material = fullMaterial;
        GetComponent<Renderer>().material.color = color;
    }

    public void changeOutlineColor(Color color)
    {
        GetComponent<SpriteRenderer>().sprite = outlineSprite;
        GetComponent<Renderer>().material = defaultMaterial;
        GetComponent<Renderer>().material.color = color;
    }

    public void changeBackColor()
    {
        GetComponent<SpriteRenderer>().sprite = fullSprite;
        GetComponent<Renderer>().material = fullMaterial;
        GetComponent<Renderer>().material.color = highlightedColor;
    }

    void OnMouseEnter() //po najeti mysi na pole
    {
        previousColor = GetComponent<Renderer>().material.color;
        changeColor(Color.green); //zmenime barvu na zelenou
        if (isPassable()) //a jestli je pole pristupne
        {
            if (neighbourHighlight) //a je v dosahu
            {
                //a mame naklikle pole s jednoutkou, ktera se nepohybuje
                if (HexGridFieldManager.instance.selectedHex != null && !HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().isMoving())
                {
                    previousColor = Color.blue;
                    PathFinder.instance.buildPath(HexGridFieldManager.instance.selectedHex, this, Color.blue); //tak zvyraznime cestu k tomuto poli
                }
            }
        }
        else
        {
            if(unit != null)
            {
                if (neighbourHighlight) //a je v dosahu
                {
                    //a mame naklikle pole s jednoutkou, ktera se nepohybuje
                    if (HexGridFieldManager.instance.selectedHex != null && !HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().isMoving())
                    {
                        previousColor = Color.red;
                        PathFinder.instance.buildPath(HexGridFieldManager.instance.selectedHex, this, Color.red); //tak zvyraznime cestu k tomuto poli
                    }
                }
            }
        }
    }

    void OnMouseExit() //jak mile mys neni na poli, tak vratime barvu pole do puvodni barvy
    {
        if (!selected && !neighbourHighlight) {
            GetComponent<SpriteRenderer>().sprite = outlineSprite;
            GetComponent<Renderer>().material = defaultMaterial;
            if(unit == null || unit.GetComponent<BasicUnit>().side != HexGridFieldManager.instance.playerTurn)
                GetComponent<Renderer>().material.color = orange2;
            else
                GetComponent<Renderer>().material.color = Color.green;
        }
        else if(neighbourHighlight || selected)
        {
            changeColor(previousColor);
        }
    }

    void OnMouseOver() //pokud je mys na poli a klikneme pravym tlacitkem, tak provedeme akci
    {
        if (Input.GetMouseButtonDown(1) && HexGridFieldManager.instance.selectedHex != null && !HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().isMoving() && HexGridFieldManager.instance.numberOfActions > 0)
            mouseRightButtonClick();
    }

    void OnMouseDown()
    {
        if (unit == null || unit.GetComponent<BasicUnit>().side != HexGridFieldManager.instance.playerTurn ||
            (HexGridFieldManager.instance.selectedHex != null && HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().isMoving()))
            return;
        //pokud se jednotka nepohybuje, tak pole vybereme
        if (!selected)
        {
            if (HexGridFieldManager.instance.selectedHex != null) //pokud bylo nejake pole vybrano, tak vyber zrusime
            {
                HexGridFieldManager.instance.selectedHex.unHighlightTile(true);
                HexGridFieldManager.instance.selectedHex.selectNeighbours(HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().reach, false);
            }
            HexGridFieldManager.instance.selectedHex = this;
            highlightTile(orange, true);
            selectNeighbours(unit.GetComponent<BasicUnit>().reach, true); //a zaroven zvyraznime dosah ktery ma jednotka na tomto poli
        }
        else //pokud pole bylo vybrano a znovu na nej klikneme, tak zrusime vybrani a zvyrazneni dalsich poli
        {
            unHighlightTile(true);
            selectNeighbours(unit.GetComponent<BasicUnit>().reach, false);
            HexGridFieldManager.instance.selectedHex = null;
        }
        PathFinder.instance.reset();
    }

    void mouseRightButtonClick() //jednotka se zacne pohybovat smerem k tomuto poli pokud je to mozne
    {
        if (HexGridFieldManager.instance.numberOfActions <= 0)
            return;
        if (isPassable())
        {
            if (neighbourHighlight)
            {
                if (HexGridFieldManager.instance.selectedHex != null)
                {
                    HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().proceedPath();
                    HexGridFieldManager.instance.numberOfActions--;
                    //HexGridFieldManager.instance.nextTurn();
                }
            }
        }
        else
        {
            if (neighbourHighlight)
            {
                if (HexGridFieldManager.instance.selectedHex != null)
                {
                    HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().proceedAttack(this);
                    HexGridFieldManager.instance.numberOfActions--;
                    //HexGridFieldManager.instance.nextTurn();
                }
            }
        }
    }

    public bool isPassable() //v tuto chvili bereme jako prekazku pouze jednotku na poli, ale neni problem pridat dalsi podminky
    {
        if (unit == null)
            return true;
        else
            return false;
    }

    public void highlightTile(Color c, bool player)
    {
        if (player)
        {
            if (!selected)
            {
                previousColor = c;
                changeColor(Color.green);
                selected = true;
            }
        }
        else
        {
            if (!neighbourHighlight)
            {
                if (unit != null && unit.GetComponent<BasicUnit>().side != HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().side)
                {
                    changeColor(Color.red);
                    highlightedColor = Color.red;
                    neighbourHighlight = true;
                }
                else if(unit == null)
                {
                    changeColor(c);
                    highlightedColor = c;
                    neighbourHighlight = true;
                }
            }
        }
    }

    public void highlightUnitTile()
    {
        changeOutlineColor(Color.green);
    }

    public void unHighlightUnitTile()
    {
        GetComponent<SpriteRenderer>().sprite = outlineSprite;
        GetComponent<Renderer>().material = defaultMaterial;
        GetComponent<Renderer>().material.color = orange2;
    }

    public void unHighlightTile(bool player)
    {
        if (player)
        {
            if (selected)
            {
                GetComponent<SpriteRenderer>().sprite = outlineSprite;
                GetComponent<Renderer>().material = defaultMaterial;
                if(unit == null)
                    GetComponent<Renderer>().material.color = orange2;
                else
                    GetComponent<Renderer>().material.color = Color.green;
                selected = false;
            }
        }
        else
        {
            if (neighbourHighlight)
            {
                GetComponent<SpriteRenderer>().sprite = outlineSprite;
                GetComponent<Renderer>().material = defaultMaterial;
                GetComponent<Renderer>().material.color = orange2;
                neighbourHighlight = false;
            }
        }
    }

    public void setUnit(GameObject unitObject)
    {
        unit = unitObject;
        unit.transform.position = transform.position;
    }

    public static ArrayList neighbourEven //lichy radek
    {
        get
        {
            return new ArrayList
                {
                    new Point(1, 0),
                    new Point(-1, 0),
                    new Point(-1, -1),
                    new Point(0, -1),
                    new Point(0, 1),
                    new Point(-1, 1),
                };
        }
    }

    public static ArrayList neighbourOdd //sudy radek
    {
        get
        {
            return new ArrayList
                {
                    new Point(1, 0),
                    new Point(-1, 0),
                    new Point(1, -1),
                    new Point(0, -1),
                    new Point(0, 1),
                    new Point(1, 1),
                };
        }
    }

    public void FindNeighbours(Hashtable board, Vector2 boardSize)
    {
        ArrayList neighbours = new ArrayList();

        foreach (Point point in (boardPosition.y % 2 == 0) ? neighbourOdd : neighbourEven) //podle sude/liche vybereme spravny list se souradnicemi
        {
            int neighbourX = boardPosition.x + point.x;
            int neighbourY = boardPosition.y + point.y;

            //drzime se pouze v rozmezi herni plochy
            if (neighbourX >= 0 &&
                neighbourX < (int)boardSize.x &&
                neighbourY >= 0 && neighbourY < (int)boardSize.y)
            {
                GameObject n = (GameObject) board[new Point(neighbourX, neighbourY)];
                neighbours.Add(n.GetComponent<HexTile>());
            }
        }

        AllNeighbours = neighbours;
    }

    public void selectNeighbours(int reach, bool highlight) //zvyraznime sousedy na ktere muze jednotka jit
    {
        if (!isPassable() && HexGridFieldManager.instance.selectedHex != this)
            return;
        foreach (HexTile til in AllNeighbours)
        {
            if(highlight)
                til.highlightTile(Color.yellow, false);
            else
                til.unHighlightTile(false);
            if (reach > 1 && (isPassable() || HexGridFieldManager.instance.selectedHex == this))
            {
                til.selectNeighbours(reach - 1, highlight);
            }
        }
    }

    public void setBoardPosition(int x, int y)
    {
        boardPosition.x = x;
        boardPosition.y = y;
    }

    // Use this for initialization
    void Start () {
        /*GetComponent<SpriteRenderer>().sprite = outlineSprite;
        GetComponent<Renderer>().material = defaultMaterial;
        GetComponent<Renderer>().material.color = orange2;*/
        /*if (unit.GetComponent<BasicUnit>().side == HexGridFieldManager.instance.playerTurn)
        {
            unit.GetComponent<BasicUnit>().unhideUnit();
            highlightUnitTile();
        }
        else
        {
            GetComponent<BasicUnit>().hideUnit();
            unHighlightUnitTile();
        }*/
    }
	
	// Update is called once per frame
	void Update () {

    }
}

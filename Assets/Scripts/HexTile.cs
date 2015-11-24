using UnityEngine;
using System.Collections;

public class HexTile : MonoBehaviour {

    public GameObject unit;
    private GameObject placeHolder;
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
        if (Input.GetMouseButtonDown(1) && HexGridFieldManager.instance.selectedHex != null
            && !HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().isMoving()
            && HexGridFieldManager.instance.hasAvailableActions())
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
                HexGridFieldManager.instance.unitRankText.text = "Unit rank: ";
                HexGridFieldManager.instance.selectedHex.unHighlightTile(true);
                HexGridFieldManager.instance.selectedHex.selectNeighbours(HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().reach, false);
            }
            HexGridFieldManager.instance.selectedHex = this;
            highlightTile(orange, true);
            selectNeighbours(unit.GetComponent<BasicUnit>().reach, true); //a zaroven zvyraznime dosah ktery ma jednotka na tomto poli
            HexGridFieldManager.instance.unitRankText.text = "Unit rank: " + this.unit.GetComponent<BasicUnit>().rank;
        }
        else //pokud pole bylo vybrano a znovu na nej klikneme, tak zrusime vybrani a zvyrazneni dalsich poli
        {
            unHighlightTile(true);
            selectNeighbours(unit.GetComponent<BasicUnit>().reach, false);
            HexGridFieldManager.instance.selectedHex = null;
            HexGridFieldManager.instance.unitRankText.text = "Unit rank: ";
        }
        PathFinder.instance.reset();
    }

    void mouseRightButtonClick() //jednotka se zacne pohybovat smerem k tomuto poli pokud je to mozne
    {
        if (!HexGridFieldManager.instance.hasAvailableActions())
            return;
        if (isPassable())
        {
            if (neighbourHighlight)
            {
                if (HexGridFieldManager.instance.selectedHex != null)
                {
                    HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().proceedPath();
                    HexGridFieldManager.instance.proceedAction();
                    //HexGridFieldManager.instance.nextTurn();
                }
            }
        }
        else
        {
            if (neighbourHighlight && (HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().isVulnerable()))
            {
                if (HexGridFieldManager.instance.selectedHex != null)
                {
                    HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().proceedAttack(this);
                    HexGridFieldManager.instance.proceedAction();
                    //HexGridFieldManager.instance.nextTurn();
                }
            }
        }
    }

    public bool isPassable() //v tuto chvili bereme jako prekazku pouze jednotku na poli, ale neni problem pridat dalsi podminky
    {
        if (unit == null && placeHolder == null)
            return true;
        else
            return false;
    }

    public bool isHighlighted()
    {
        return selected || neighbourHighlight;
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
                else if(isPassable())
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
        unit.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        unit.transform.rotation = new Quaternion(0, 1, 0, unitObject.GetComponent<BasicUnit>().side * 180);
    }

    public void setPlaceHolder(GameObject pHolder)
    {
        placeHolder = pHolder;
        placeHolder.transform.position = transform.position;
    }

    public static ArrayList neighbourArray //sudy radek
    {
        get
        {
            return new ArrayList
                {
                    new Point(1, 0),
                    new Point(-1, 0),
                    new Point(0, -1),
                    new Point(1, -1),
                    new Point(0, 1),
                    new Point(1, 1),
                };
        }
    }

    public void FindNeighbours(Hashtable board, Vector2 boardSize)
    {
        ArrayList neighbours = new ArrayList();

        foreach (Point point in neighbourArray) //projedeme okolni body
        {
            int neighbourY = boardPosition.y + point.y;
            int neighbourX = boardPosition.x + point.x;

            int compareY = (int) ((neighbourY > boardSize.y / 2 - 1) ? (boardSize.y - neighbourY) : neighbourY);
            int compareY2 = (int)((boardPosition.y > boardSize.y / 2 - 1) ? (boardSize.y - boardPosition.y) : boardPosition.y);

            if (compareY < compareY2) {
                neighbourX = boardPosition.x + point.x - 1;
            }

            //drzime se pouze v rozmezi herni plochy
            if (neighbourX >= 0 &&
                neighbourX < (int) boardSize.x + ((neighbourY < boardSize.y / 2 - 1) ? neighbourY : (boardSize.y - 1) - neighbourY) &&
                neighbourY >= 0 && neighbourY < (int)boardSize.y)
            {
                GameObject n = (GameObject)board[new Point(neighbourX, neighbourY)];
                neighbours.Add(n.GetComponent<HexTile>());
            }
        }

        AllNeighbours = neighbours;
    }

    public void selectNeighbours(int reach, bool highlight) //zvyraznime sousedy na ktere muze jednotka jit
    {
        if (!isPassable() && HexGridFieldManager.instance.selectedHex != this || !HexGridFieldManager.instance.selectedHex.unit.GetComponent<BasicUnit>().isMoveable())
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

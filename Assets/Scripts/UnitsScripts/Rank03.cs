using UnityEngine;
using System.Collections;

public class Rank03 : BasicUnit
{
    override public void useAbility()
    {
        if (attacking)
        {
            if (attackingTile.unit.GetComponent<BasicUnit>().rank == 0)
            {
                hiddenRank = 100;
                Debug.Log("HIDDEN RANK: 100");
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class RankShadow : BasicUnit
{
    override public void useAbility()
    {
        if(attacking)
        {
            hiddenRank = 99;
            Debug.Log("HIDDEN RANK: 99");
        }
    }
}

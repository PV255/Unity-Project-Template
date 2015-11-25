using UnityEngine;
using System.Collections;

public class RankPortal : BasicUnit
{
    override public void useAbility()
    {
        if(!attacking)
        {
            Debug.Log("END GAME");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public override void Die()
    {
        gameManager.won = true;
        Destroy(gameObject);
    }
}
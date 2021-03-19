using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GotoPlayer
{
    public static Vector2 GetDirection(Vector2 playerPostition, Vector2 objectPosition, float stopDistance)
    {
        Vector2 direction = Vector2.zero;
        if (Vector2.Distance(objectPosition, playerPostition) > stopDistance)
        {
            direction = new Vector2(playerPostition.x - objectPosition.x, playerPostition.y - objectPosition.y);
        }
        return direction;
    }
}

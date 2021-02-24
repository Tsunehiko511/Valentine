using System.Collections;
using MoonSharp.Interpreter;
using UnityEngine;

[MoonSharpUserData]
public class Player : LuaInterpreterHandlerBase
{
    Vector3 GetDirection(string direction)
    {
        switch (direction)
        {
            default:
                return Vector3.zero;
            case "left":
                return Vector3.left;
            case "right":
                return Vector3.right;
            case "up":
                return Vector3.up;
            case "down":
                return Vector3.down;
        }
    }


    public void MoveTo(string direction, int count)
    {
        StartCoroutine(MoveCorou(GetDirection(direction), count));
    }
    public void MoveTo(Vector3 direction)
    {
        // StartCoroutine(MoveCorou(direction));
    }

    IEnumerator MoveCorou(Vector3 direction, int count)
    {
        flag = false;
        for (int i=0; i< count; i++)
        {
            yield return new WaitForSeconds(0.3f);
            transform.Translate(direction);
        }
        yield return new WaitForSeconds(0.5f);
        flag = true;
    }
}

using System.Collections;
using MoonSharp.Interpreter;
using UnityEngine;

[MoonSharpUserData]
public class MoveEvent : LuaInterpreterHandlerBase
{
    [SerializeField] GameObject model = default;
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

    IEnumerator MoveCorou(Vector3 direction, int count)
    {
        flag = false;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(ParamSO.Instance.RuntimeMoveSpeed);
            transform.Translate(direction);
        }
        yield return new WaitForSeconds(ParamSO.Instance.RuntimeWaitTime);
        flag = true;
    }
    public void JumpTo(string direction, int count)
    {
        Vector3 position = GetDirection(direction) * count;
        transform.Translate(position);
    }
    public void JumpToPosition(int x, int y)
    {
        transform.position = new Vector3(x,y);
    }
    public void Show(bool isActive)
    {
        model.SetActive(isActive);
    }
}

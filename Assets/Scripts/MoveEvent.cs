using System.Collections;
using MoonSharp.Interpreter;
using UnityEngine;

[MoonSharpUserData]
public class MoveEvent : LuaInterpreterHandlerBase
{
    [SerializeField] GameObject model = default;
    [SerializeField] Sprite spriteFront = default;
    [SerializeField] Sprite spriteRight = default;
    [SerializeField] Sprite spriteLeft = default;
    [SerializeField] Sprite spriteBack = default;


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


    public void MoveTo(string direction, int count, bool look = true)
    {
        if (look)
        {
            Look(direction);
        }
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
    public void JumpToPosition(int x, int y, bool isFloat = false)
    {
        if (isFloat)
        {
            float newX = x / 10.0f;
            float newY = y / 10.0f;
            transform.position = new Vector3(newX, newY);
            return;
        }
        transform.position = new Vector3(x, y);
    }
    public void Show(bool isActive)
    {
        model.SetActive(isActive);
    }

    public void Look(string direction)
    {
        if (spriteRight == null)
        {
            return;
        }
        SpriteRenderer renderer = model.GetComponent<SpriteRenderer>();
        switch (direction)
        {
            case "right":
                renderer.sprite = spriteRight;
                break;
            case "left":
                renderer.sprite = spriteLeft;
                break;
            case "down":
                renderer.sprite = spriteFront;
                break;
            case "up":
                renderer.sprite = spriteBack;
                break;
        }
    }
}

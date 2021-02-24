using System.Collections;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

[MoonSharpUserData]
public class MessageManager : LuaInterpreterHandlerBase
{
    [SerializeField] Text messageText = default;
    [SerializeField] float speed = default;

    public void ShowMessage(string message)
    {
        StartCoroutine(ShowMessageCor(message));
    }
    IEnumerator ShowMessageCor(string message)
    {
        flag = false;
        messageText.text = "";
        foreach (char c in message)
        {
            yield return new WaitForSeconds(ParamSO.Instance.RuntimeMessageSpeed);
            messageText.text += c;
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || ParamSO.Instance.RuntimeSpeedMode);
        flag = true;
    }
    /*
         npcBoy.Show(true)
    coroutine.yield()
    npcBoy.MoveTo("down", 4)
    coroutine.yield()
    npcBoy.MoveTo("left", 2)
    coroutine.yield()

     */
}

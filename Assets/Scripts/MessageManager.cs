using System.Collections;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

[MoonSharpUserData]
public class MessageManager : LuaInterpreterHandlerBase
{
    [SerializeField] Text messageText = default;
    [SerializeField] SoundManager sound = default;

    public void ShowMessage(string message)
    {
        StartCoroutine(ShowMessageCor(message));
    }
    IEnumerator ShowMessageCor(string message)
    {
        flag = false;
        messageText.text = "";
        int count = 0;
        foreach (char c in message)
        {
            yield return new WaitForSeconds(ParamSO.Instance.RuntimeMessageSpeed);
            messageText.text += c;
            count++;
            if (count%2==1)
            {
                sound.PlaySE(SoundManager.SE.Message);
            }
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || ParamSO.Instance.RuntimeSpeedMode);
        flag = true;
    }
    public void ClearMessage()
    {
        messageText.text = "";
    }
}

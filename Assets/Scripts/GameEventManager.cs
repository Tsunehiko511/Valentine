using System.Collections;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] EventData eventData = default;
    [SerializeField] Player player = default;
    [SerializeField] TextAsset textAsset = default;

    private void Start()
    {
        StartCoroutine(ExecuteEvent(textAsset.text));
    }

    IEnumerator ExecuteEvent(string script)
    {
        var interpreter = new LuaInterpreter(script); // スクリプトを渡して初期化
        interpreter.AddHandler("player", player); // メッセージ制御のハンドラを登録
        interpreter.AddHandler("event", eventData); // メッセージ制御のハンドラを登録
        while (interpreter.HasNextScript())
        {
            interpreter.Resume();
            yield return interpreter.WaitCoroutine();
        }
    }

    IEnumerator Cor0()
    {
        yield return null;
    }
    IEnumerator Cor1()
    {
        yield return null;
    }
    IEnumerator Cor2()
    {
        yield return null;
    }

}
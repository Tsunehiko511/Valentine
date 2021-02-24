using System.Collections;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] Player player = default;
    [SerializeField] MoveEvent npcGirl = default;
    [SerializeField] MoveEvent npcBoy = default;
    [SerializeField] MessageManager messagePanel = default;
    [SerializeField] TextAsset textAsset = default;
    [SerializeField] new MoveEvent camera = default;

    // デバッグモードを作る
    // 特定の１から開始できるようにする
    // 待機じかんが0
    private void Start()
    {
        StartCoroutine(ExecuteEvent(textAsset.text));
    }

    IEnumerator ExecuteEvent(string script)
    {
        var interpreter = new LuaInterpreter(script); // スクリプトを渡して初期化
        interpreter.AddHandler("player", player); // メッセージ制御のハンドラを登録
        interpreter.AddHandler("npcGirl", npcGirl); // メッセージ制御のハンドラを登録
        interpreter.AddHandler("npcBoy", npcBoy); // メッセージ制御のハンドラを登録
        interpreter.AddHandler("message", messagePanel); // メッセージ制御のハンドラを登録
        interpreter.AddHandler("camera", camera); // メッセージ制御のハンドラを登録
        while (interpreter.HasNextScript())
        {
            interpreter.Resume();
            yield return interpreter.WaitCoroutine();
        }
    }


}
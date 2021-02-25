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
    [SerializeField] FadePanel fadePanel = default;
    [SerializeField] Player oldPlayer = default;
    [SerializeField] GameObject titlePanel = default;
    [SerializeField] SoundManager sound = default;
    [SerializeField] MoveEvent chocolate = default;
    [SerializeField] GameObject[] lines = default;
    bool pushStart;
    // デバッグモードを作る
    // 特定の１から開始できるようにする
    // 待機じかんが0
    private void Start()
    {
        pushStart = false;
        titlePanel.SetActive(true);
        sound.PlayBGM(SoundManager.BGM.Title);
        lines[0].SetActive(true);
        lines[1].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pushStart == false)
        {
            pushStart = true;
            titlePanel.GetComponent<Animator>().Play("FadeTitleAnimation");
            // titlePanel.SetActive(false);
            sound.StopBGM();
            // StartCoroutine(GameEvent());
            StartCoroutine(ExecuteEvent(textAsset.text));
        }
    }

    IEnumerator ExecuteEvent(string script)
    {
        var interpreter = new LuaInterpreter(script); // スクリプトを渡して初期化
        interpreter.AddHandler("player", player);
        interpreter.AddHandler("npcGirl", npcGirl);
        interpreter.AddHandler("npcBoy", npcBoy);
        interpreter.AddHandler("message", messagePanel);
        interpreter.AddHandler("camera", camera);
        interpreter.AddHandler("fadePanel", fadePanel);
        interpreter.AddHandler("oldPlayer", oldPlayer);
        interpreter.AddHandler("sound", sound);
        interpreter.AddHandler("chocolate", chocolate);
        yield return new WaitForSeconds(2);
        while (interpreter.HasNextScript())
        {
            interpreter.Resume();
            yield return interpreter.WaitCoroutine();
        }
    }
}
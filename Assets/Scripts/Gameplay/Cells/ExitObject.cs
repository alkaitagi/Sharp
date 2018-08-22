using System.Linq;

using UnityEngine;

using Newtonsoft.Json.Linq;

public class ExitObject : SerializableObject
{
    private void Awake() =>
        Passed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Passed && collision.GetComponent<PlayerObject>())
        {
            Pass();
            collision.GetComponent<UnitComponent>().Kill();
        }
    }

    #region gameplay

    [Header("Gameplay")]
    public Prompt message;

    [Space(10)]
    public JsonFile meta;
    public JsonFile level;

    public static bool Passed { get; private set; }

    private void Pass()
    {
        Passed = true;

        var passed = (JArray)meta["passed"];
        if (!passed.Select(t => (string)t).Contains(level.ShortName))
        {
            passed.Add(level.ShortName);
            meta.Save();
        }

        Instantiate(message, transform.position, Quaternion.identity)
            .Setup("Home", () => { EngineUtility.Main.LoadScene("Home"); });
    }

    #endregion
}
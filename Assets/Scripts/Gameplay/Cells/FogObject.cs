using UnityEngine;

using System.Linq;

using DG.Tweening;
using Newtonsoft.Json.Linq;

public class FogObject : SerializableObject
{
    private void Awake() =>
        animation = gameObject.AddComponent<TweenArrayComponent>().Init
        (
            DOTween.Sequence().Insert
            (
                maskSprite.material
                    .DOFade(0, Constants.Time)
            ) 
        );

    private void Start() =>
        collider.radius = 1.1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerObject>())
            Reveal();
    }

    #region gameplay

    [Header("Gameplay")]
    public new CircleCollider2D collider;

    [SerializeField]
    private bool spread;
    public bool Spread
    {
        get
        {
            return spread;
        }
        set
        {
            spread = value;
        }
    }

    private void Reveal()
    {
        collider.enabled = false;

        Destroy(gameObject, 0.1f);
        if (Spread)
            Invoke("Delay", .075f);

        animation[0].Play(false);
    }

    private void Delay()
    {
        while (true)
        {
            Collider2D neighbor = Physics2D.OverlapPoint(transform.position, Constants.FogMask);
            if (neighbor)
                neighbor.GetComponent<FogObject>().Reveal();
            else
                break;
        }
    }

    #endregion

    #region animation

    [Header("Animation")]
    [SerializeField]
    private SpriteRenderer maskSprite;

    private new TweenArrayComponent animation;

    #endregion

    #region serialization

    public override void Serialize(JToken token) =>
        token["spread"] = Spread;

    public override void Deserialize(JToken token) =>
        Spread = (bool)token["spread"];

    #endregion
}
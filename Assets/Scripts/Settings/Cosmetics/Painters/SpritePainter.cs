using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[AddComponentMenu("Painters/Sprite Painter")]
public class SpritePainter : BaseCosmetic<ColorVariable>
{
    public override void Refresh()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Variable.Value.Fade(spriteRenderer.color.a);
    }
}
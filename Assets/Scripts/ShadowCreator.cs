using UnityEngine;

public class ShadowCreator : MonoBehaviour
{
    [SerializeField] private Material shadowMaterial;

    void Start()
    {
        GameObject shadow = new GameObject("Shadow");
        shadow.transform.SetParent(transform, false);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer shadowSpriteRenderer = shadow.AddComponent<SpriteRenderer>();

        shadowSpriteRenderer.sprite = spriteRenderer.sprite;
        shadowSpriteRenderer.material = shadowMaterial;
        shadowSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
        shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
    }
}

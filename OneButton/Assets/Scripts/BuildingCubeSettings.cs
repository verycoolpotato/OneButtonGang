using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BuildingCubeSettings : Destructible
{
    public BuildingData Data;
    [SerializeField] private SpriteRenderer ThisSpriteRenderer;

    private GameObject platform;

    [Space]
    [Header("Sprite")]
    [SerializeField] private int ActiveSpriteNumber;
    [SerializeField] private Sprite ActiveSprite;
    private void OnValidate()
    {
        ActiveSpriteNumber = Mathf.Clamp(ActiveSpriteNumber, 0, Data.Sprites.Count -1);
        ActiveSprite = Data.Sprites[ActiveSpriteNumber];
    }
    private void Awake()
    {
        platform = transform.GetChild(0).gameObject;
        ThisSpriteRenderer.sprite = ActiveSprite;
    }

    
}

using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BuildingCubeSettings : Destructible
{
    public BuildingData Data;
    [SerializeField] private SpriteRenderer ThisSpriteRenderer;

    [Tooltip("Does this tile have a platform on it?")]
    [SerializeField] private bool HasPlatform;

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
        platform.SetActive(HasPlatform);
        ThisSpriteRenderer.sprite = ActiveSprite;
    }

    
}

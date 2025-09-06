using System.Collections.Generic;
using UnityEngine;

public class BuildingCubeSettings : MonoBehaviour
{
    public BuildingData Data;
    [SerializeField] private SpriteRenderer ThisSpriteRenderer;

    [Space]
    [Header("Sprite")]
    [SerializeField] private int ActiveSpriteNumber;
    [SerializeField] private Sprite ActiveSprite;
    private void OnValidate()
    {
        ActiveSpriteNumber = Mathf.Clamp(ActiveSpriteNumber, 0, Data.Sprites.Count -1);
        ActiveSprite = Data.Sprites[ActiveSpriteNumber];
    }

  
    private void Start()
    {
        ThisSpriteRenderer.sprite = ActiveSprite;
    }
}

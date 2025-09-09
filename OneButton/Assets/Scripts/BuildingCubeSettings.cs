using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BuildingCubeSettings : Destructible
{
   
    

    [Tooltip("Does this tile have a platform on it?")]
    [SerializeField] private bool HasPlatform;

    private GameObject platform;

    
    
   
    private void Awake()
    {
        platform = transform.GetChild(0).gameObject;
        platform.SetActive(HasPlatform);
        
    }

    
}

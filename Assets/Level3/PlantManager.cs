using UnityEngine;
using System.Collections.Generic;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance; 

    [SerializeField] private GameObject plantPrefab; 
    private List<GameObject> spawnedPlants = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public GameObject SpawnPlant(Vector3 tilePosition)
{
    Vector3 spawnPos = tilePosition + Vector3.up * 0.1f;
    GameObject newPlant = Instantiate(plantPrefab, spawnPos, Quaternion.identity, transform);
    Debug.Log("Spawned plant at " + spawnPos);
    return newPlant;
}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class FarmTileControl:MonoBehaviour
{
    public enum FarmTileCond //used for the switch states
    {
        Grass, Tilled, Watered
    }
    public FarmTileCond tileCond;

    //3 materials representing 3 different farm tile conditions
    public Material grassMat, tilledMat, wateredMat;
    private List<Material> curTileEdgeMaterial=new List<Material>();

    private AudioSource stepTileAudio;
    private AudioSource tillTileAudio;
    private AudioSource waterTileAudio;

    private int daysPassedSinceLastInteraction=0;
    [SerializeField]
    private GameObject dayNightControl;
    [SerializeField]
    private ResourceBarTracker Stamina;

  
    
    // keeps track of what’s currently spawned
    public GameObject currentPlant; 


    public void Start()
    {
        tileCond = FarmTileCond.Grass;//start with grass
        GameObject tileEdge;
        Stamina = GameObject.Find("Canvas/StaminaBar/Resource").GetComponent<ResourceBarTracker>();
        foreach (Transform edge in transform)
        {
            tileEdge = edge.gameObject;
            curTileEdgeMaterial.Add(tileEdge.GetComponent<MeshRenderer>().material);
        }
        //get access to the audio clips attached on the tile object
        AudioSource[] audios = gameObject.GetComponents<AudioSource>();
        stepTileAudio = audios[0];
        tillTileAudio = audios[1];
        waterTileAudio = audios[2];

        dayNightControl = GameObject.Find("DayNightController");
        dayNightControl.GetComponent<DayNightControl>().listenToDayPassEvent(this.gameObject);
    }

private void Update()
{
    if (tileCond == FarmTileCond.Watered && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
    {
        if (currentPlant == null && PlantManager.Instance != null)
        {
            currentPlant = PlantManager.Instance.SpawnPlant(transform.position);
            Debug.Log("Plant spawned!");
        }
    }
}


    public void UpdateTileMaterial()//called to update the tile appearance when the farm tile changes states
    {
        switch (tileCond)
        {
            case FarmTileCond.Grass:
                GetComponent<MeshRenderer>().material = grassMat;
                break;
            case FarmTileCond.Tilled:
                GetComponent<MeshRenderer>().material = tilledMat;
                break;
            case FarmTileCond.Watered:
                GetComponent<MeshRenderer>().material = wateredMat;
                break;
        }
    }

    //API prepared for the player to interact with tiles when steping on it and press the space key
    public void InteractWithFarmTile()
    {
        if (Stamina.CurrentResource() != 0)
        {
            switch (tileCond)
            {
                case FarmTileCond.Grass://interact with the grass tile tills it
                    Debug.Log("Player is tilling the tile " + transform.name);
                    tillTileAudio.Play();
                    tileCond = FarmTileCond.Tilled;
                    UpdateTileMaterial();
                    daysPassedSinceLastInteraction = 0;
                    Stamina.ChangeResourceByAmount(-5);
                    break;
                case FarmTileCond.Tilled: // player is watering the tile
                    Debug.Log("Player is watering the tile " + transform.name);
                    waterTileAudio.Play();
                    tileCond = FarmTileCond.Watered;
                    UpdateTileMaterial();
                    daysPassedSinceLastInteraction = 0;
                    Stamina.ChangeResourceByAmount(-5);

                    // Add one fund when watered
                    if (FundManager.Instance != null)
                    {
                    FundManager.Instance.AddFunds(1);
                    }
                    break;
                case FarmTileCond.Watered://repeated interaction on the watered tile does not change it to other conditions
                    Debug.Log("Tile is watered and ready for plants " + transform.name);
                    daysPassedSinceLastInteraction = 0;
                    break;
            }
        }
    }

    //API prepared for the player interaction point to select a tile
    //When the player's interaction point (a bit in front of the player) interact with a tile,
    //this tile's material gets brighter to show the current tile that the player is about to interact with
    public void EnterTile()
    {
        Debug.Log("Player entered tile " + transform.name);
        foreach (Material mat in curTileEdgeMaterial)
            mat.EnableKeyword("_EMISSION");


        //play the audio clip
        stepTileAudio.Play();
    }
    public void ExitTile()
    {
        Debug.Log("Player exited tile " + transform.name);
        foreach (Material mat in curTileEdgeMaterial)
            mat.DisableKeyword("_EMISSION");
    }


    //API provided for the day night counter to tell the tile that a day just passed
 public void ADayPassed()
    {
        daysPassedSinceLastInteraction++;

        if (tileCond == FarmTileCond.Watered && currentPlant != null)
        {
            currentPlant.GetComponent<PlantGrowth>().Grow();
        }

        if (daysPassedSinceLastInteraction >= 2)
        {
            switch (tileCond)
            {
                case FarmTileCond.Watered:
                    tileCond = FarmTileCond.Tilled;
                    UpdateTileMaterial();
                    daysPassedSinceLastInteraction = 0;
                    break;

                case FarmTileCond.Tilled:
                    tileCond = FarmTileCond.Grass;
                    UpdateTileMaterial();
                    daysPassedSinceLastInteraction = 0;

                    if (currentPlant != null)
                    {
                        Destroy(currentPlant);
                        currentPlant = null;
                    }
                    break;
            }
        }
    }

}

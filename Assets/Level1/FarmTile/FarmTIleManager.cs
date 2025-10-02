using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmTIleManager:MonoBehaviour
{
    public List<FarmTileControl> tileList;

    public GameObject FarmTile;
    public Transform FarmTileSpawnStartLocation;
    private Vector3 curFarmTileSpawnPos;
    
    public int numFarmTileX = 4;
    public int numFarmTileZ = 4;
    public float gapSizeBetweenTiles = 0.1f;
    
    private void Start()
    {
        //initialize tile list
        tileList = new List<FarmTileControl>();

        //other stuff
        curFarmTileSpawnPos=FarmTileSpawnStartLocation.position;
        curFarmTileSpawnPos.y = - FarmTile.transform.localScale.y / 2 + 0.05f;//move top of tile close to the ground surface
        int numTileGenerated = 0;
        for (int i = 0; i < numFarmTileZ; i++)
        {
            for (int j = 0; j < numFarmTileX; j++)//generate a row of tiles
            {
                GameObject curFarmTile = Instantiate(FarmTile, curFarmTileSpawnPos, Quaternion.identity);
                curFarmTileSpawnPos.x += FarmTile.transform.localScale.x + gapSizeBetweenTiles;//offset tiles
                numTileGenerated++;
                curFarmTile.name = "Farm Tile " + numTileGenerated.ToString();
                curFarmTile.transform.parent = transform;//put all the tiles under the tile manager
                //add tile to list
                tileList.Add(curFarmTile.GetComponent<FarmTileControl>());
            }
            curFarmTileSpawnPos.z += FarmTile.transform.localScale.z + gapSizeBetweenTiles;
            curFarmTileSpawnPos.x = FarmTileSpawnStartLocation.position.x;
        }
    }

    private void Update()
    {
        //check win condition: all tiles watered
        if (LevelWon())
        {
            SceneManager.LoadScene("Scene2-Store");
        }
    }

    bool LevelWon()
    {
        //check if every tile watered
        foreach (FarmTileControl tile in tileList)
        {
            //if we find one not watered, return false
            if (tile.tileCond != FarmTileControl.FarmTileCond.Watered)
            {
                return false;
            }
        }
        return true;
    }
}

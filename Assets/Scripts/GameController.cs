using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Tooltip("A reference ot the tile we want to spawn")]
    public Transform tile;


    [Tooltip("A reference to the obstacle we want to spawn")]
    public Transform obstacle;

    public Vector3 startPoint = new Vector3(0,0,-5);

    [Tooltip("How many tiles should we create in advance")]
    [Range(1,15)]
    public int initSpawnNum = 10;


    [Tooltip("How many tiles to spawn initially with no obstacles")]
    public int initNoObstacles = 4;


    //Where the next tile should be spawned at
    private Vector3 nextTileLocation;

    //How should the next title be rotated?
    private Quaternion nextTileRotation;

	// Use this for initialization
	void Start ()
	{
	    nextTileLocation = startPoint;
        nextTileRotation = Quaternion.identity;
	    for (int i = 0; i < initSpawnNum; ++i)
	    {
	        SpawnNextTile(i >= initNoObstacles && i% Random.Range(3,8) == 0);
	    }
	}

    public void SpawnNextTile(bool spawnObstacles = true)
    {
        var newTile = Instantiate(tile, nextTileLocation, nextTileRotation);

        var nextTile = newTile.Find("Next Spawn Point");
        nextTileLocation = nextTile.position;
        nextTileRotation = nextTile.rotation;

        if (!spawnObstacles)
        {
            return;
        }

        //Now we need to get all of the possible places to spawn the obstacles

        var obstacleSpawnPoints = new List<GameObject>();

        //go through each of the child game objects in our tile

        foreach (Transform child in newTile)
        {
            if (child.CompareTag("ObstacleSpawn"))
            {
                //we add it as a possibility
                obstacleSpawnPoints.Add(child.gameObject);
            }
        }

        //Make sure there is at least one
        if (obstacleSpawnPoints.Count > 0)
        {
            //get a radom object form the ones we have
            var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];
            
            //store its position for us to use
            var spawnPos = spawnPoint.transform.position;

            //create our obstacle
            var newObstacle = Instantiate(obstacle, spawnPos, Quaternion.identity);

            //have it parented to the tile
            newObstacle.SetParent(spawnPoint.transform);
        }
    }

    // Update is called once per frame
	void Update () {
		
	}
}

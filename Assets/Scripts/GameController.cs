using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Tooltip("A reference ot the tile we want to spawn")]
    public Transform tile;

    public Vector3 startPoint = new Vector3(0,0,-5);

    [Tooltip("How many tiles should we create in advance")]
    [Range(1,15)]
    public int initSpawnNum = 10;

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
	        SpawnNextTile();
	    }
	}

    public void SpawnNextTile()
    {
        var newTile = Instantiate(tile, nextTileLocation, nextTileRotation);

        var nextTile = newTile.Find("Next Spawn Point");
        nextTileLocation = nextTile.position;
        nextTileRotation = nextTile.rotation;
    }

    // Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEndBehaviour : MonoBehaviour
{
    [Tooltip("How much time to wait before detrying " + "the tile after reaching the end")]
    public float detroyTime = 1.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private static int num = 0;
    void OnTriggerEnter(Collider col)
    {
        //First check if we collided with the player
        if (col.gameObject.GetComponent<PlayerBehaviour>())
        {
            GameObject.FindObjectOfType<GameController>().SpawnNextTile(num++ % 3 == 0);

            //And destroy this entire tile after a short delay
            Destroy(transform.parent.gameObject, detroyTime);
        }
    }
}

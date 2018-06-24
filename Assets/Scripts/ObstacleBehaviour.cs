using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleBehaviour : MonoBehaviour
{
    [Tooltip("How long to wait vefore restarting the game")]
    public float waitTime = 2.0f;

    public GameObject explosion;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            //destroy the player
            Destroy(collision.gameObject);

            Invoke("ResetGame", waitTime);
        }
    }

    void PlayerTouch()
    {
        if (explosion != null)
        {
            var particles = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(particles, 1.0f);
        }
        Destroy(this.transform.parent.gameObject);
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}

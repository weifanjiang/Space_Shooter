using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public int score;
    public GameObject playerExplosion;
    Transform tf;
    private GameController gameController;

    void Start()
    {
        tf = GetComponent<Transform>();
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject != null)
        {
            gameController = GameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Boundary")
        {
            Instantiate(explosion, tf.position, tf.rotation);
            if (other.gameObject.tag == "Player")
            {
                Instantiate(playerExplosion, other.gameObject.transform.position, other.gameObject.transform.rotation);
                gameController.AddScore(-1 * score);
                gameController.GameOver();
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameController.AddScore(score);
        }
    }
}

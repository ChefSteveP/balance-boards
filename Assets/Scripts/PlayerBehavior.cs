using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    // behavior of triggers that come into contact with the player
    private void OnTriggerEnter(Collider other) {
    
        //if the player hits an obstacle
        if (other.gameObject.CompareTag("Obstacle")) {


            StartCoroutine(PlayerExplosion());

            //turn off tilt control
            Tilt.inPlay = false;

            //hide the player and turn off the collider while the explosion is happening
            //This stops the player from being able to make further collisions
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
        if (other.gameObject.CompareTag("WinningArea")) {
            //delete the player
            Destroy(gameObject);

            //load next level
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //once ball drops onto the board, the game is in play and player has tilt control
    private void OnCollisionEnter(Collision other) {
        Tilt.inPlay = true;
    }

    // Player explosoion coroutine
    public IEnumerator PlayerExplosion() {
        //particle effect
        Instantiate(explosion, transform.position, Quaternion.identity);

        //let particle effect play for 2 seconds and then reload the scenes
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

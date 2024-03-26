using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Obstacle")) {
            StartCoroutine(PlayerExplosion());
            Tilt.inPlay = false;
            //hide the player
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
        if (other.gameObject.CompareTag("WinningArea")) {
            Destroy(gameObject);
            //do win stufff
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void OnCollisionEnter(Collision other) {
        Tilt.inPlay = true;
    }
    // Player explosoion coroutine
    public IEnumerator PlayerExplosion() {
        Instantiate(explosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

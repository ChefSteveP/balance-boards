using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    Tilt tilt;
    // Start is called before the first frame update
    void Start()
    {
        tilt = GetComponent<Tilt>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the game is over and player clicks space, restart to level 1
        if (tilt.gameOver == true) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
        //during gameplayer (gameover == false), if player clicks r, reload the current scene
        if(tilt.gameOver == false){
            if (Input.GetKeyDown(KeyCode.R)) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        //if player clicks escape, quit the game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ExitGame();
        }
    }

//quit the game, this is a different line if the game is being played in the editor
public void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Log("You have quit the game");
        if (UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#else
        Application.Quit();

#endif
    }
}

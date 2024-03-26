using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 10.0f;
    public bool gameOver = false;
    public static bool inPlay = false;

    private void Awake() {
        inPlay = false;
    }
    private void Start() {
        if (gameOver) {
            inPlay = true;
        }
    }
    // Update is called once per frame
    void Update()
    {        
        // take 4- way input from the keyboard and tilt the board
        float tiltAroundZ = -Input.GetAxis("Horizontal") * rotSpeed;
        float tiltAroundX = Input.GetAxis("Vertical") * rotSpeed;

        //freeze tilt contol before the ball drops onto the board 
        //and after the ball hits an obstacle
        if (!inPlay) {
           tiltAroundZ = 0; 
           tiltAroundX = 0;
        }

        //smoothly tilt the board
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2.0f);
    }
}

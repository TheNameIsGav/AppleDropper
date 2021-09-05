using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{

    //Private Variables
    private float maxX;
    private float minX;
    private float appleFreq = 5f;
    private bool direction;
    private bool gameOver = false;
    private bool doomMode = false;
    private bool doOnce = true;
    private Rigidbody2D rb;
    

    //Public Variables
    public float speed = 5f;

    public void GameOver()
    {
        doomMode = false;
        gameOver = true;
    }

    public void DoomMode()
    {
        doomMode = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(0.0f, 1.0f) >= 0.5) //Dirty way of figuring out which way you're supposed to start moving when the game types.
        {
            direction = true; //Right
        } else
        {
            direction = false; //Left
        }

        maxX = Camera.main.orthographicSize * Screen.width / Screen.height; //Theoretically this should calculate the size of the screen 
        minX = -maxX;

        rb = GetComponent<Rigidbody2D>();

        if (direction)
        {
            rb.velocity = new Vector2(speed, 0);
        } else 
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    //Spawns apple when it receives a signal
    void SpawnApple(bool doom)
    {
        if (doom) {
            GameObject newApple = Object.Instantiate(transform.GetChild(0), new Vector3(transform.position.x, transform.position.y - 2, 0), transform.rotation).gameObject;
            newApple.GetComponent<Apple>().speed = -1f;
        }
        else {
            GameObject newApple = Object.Instantiate(transform.GetChild(0), new Vector3(transform.position.x, transform.position.y - 2, 0), transform.rotation).gameObject;
            newApple.GetComponent<Apple>().speed = Mathf.Max(-1 * (5f + (.1f * Time.time)), -60);
        }
    }

    private void FixedUpdate()
    {
        if (!gameOver && !doomMode)
        {
            if(Time.time % 10 == 0) //Increase Difficulty
            {
                appleFreq = Mathf.Max(appleFreq - .5f, .25f);
                Debug.Log("Apple Frequency @: " + appleFreq.ToString());
                speed += 1f;
            }

            if(Time.time % appleFreq == 0)
            {
                SpawnApple(false);
            }

            if (transform.position.x < minX)
            {
                rb.velocity = new Vector2(speed, 0);
            }

            if (transform.position.x > maxX)
            {
                rb.velocity = new Vector2(-speed, 0);
            }
        }

        if (doomMode)
        {
            SpawnApple(true);
            if (doOnce)
            {
                rb.velocity = new Vector2(50, 0);
                doOnce = false;
            }

            if (transform.position.x < minX)
            {
                rb.velocity = new Vector2(50, 0);
            }

            if (transform.position.x > maxX)
            {
                rb.velocity = new Vector2(-50, 0);
            }
        }
    }
}
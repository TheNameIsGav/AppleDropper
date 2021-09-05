using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private bool gameOver = false;
    private bool doomMode = false;

    public float speed = 5f;
    public int score;

    public void GameOver()
    {
        doomMode = false;
        gameOver = true;
    }
    public void DoomMode(bool inc)
    {
        doomMode = inc;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
       
        Vector2 incVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector2(incVec.x, -4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameOver)
        {
            Object.Destroy(collision.gameObject);
            if (!doomMode)
            {
                score += 10;
            }
        }
    }
}

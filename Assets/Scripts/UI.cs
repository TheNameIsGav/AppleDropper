using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private int lives = 3;

    public GameObject player;
    public GameObject appleController;
    public AudioSource audio;
    private bool shouldDoom = true;
    private bool dooming = false;

    public void LostApple()
    {
        if(lives == 3)
        {
            Object.Destroy(this.transform.GetChild(6).gameObject);
            lives--;
        } else 
        if (lives == 2)
        {
            Object.Destroy(this.transform.GetChild(5).gameObject);
            lives--;
        } else 
        if (lives == 1)
        {
            player.GetComponent<PlayerController>().GameOver();
            appleController.GetComponent<AppleController>().GameOver();
            this.transform.GetChild(3).gameObject.SetActive(false);
            this.transform.GetChild(4).gameObject.SetActive(true);
            PlayerPrefs.SetInt("HighScore", player.GetComponent<PlayerController>().score);

            this.transform.GetChild(2).gameObject.SetActive(true);
            audio.Stop();

            lives--;
        }
    }

    private void TriggerDoomMode()
    {
        player.GetComponent<PlayerController>().DoomMode(true);
        appleController.GetComponent<AppleController>().DoomMode();
        Invoke("StopDoomMode", 10);
    }

    private void StopDoomMode()
    {
        audio.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(2).gameObject.SetActive(false);
        this.transform.GetChild(3).gameObject.SetActive(false);
        this.transform.GetChild(4).gameObject.SetActive(false);
        this.transform.GetChild(0).GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    void FixedUpdate()
    {
        int internalScore = player.GetComponent<PlayerController>().score;
        if(internalScore > 666 && shouldDoom)
        {
            Debug.Log("Dooming");
            dooming = true;
            this.transform.GetChild(3).gameObject.SetActive(true);
            shouldDoom = false;
            internalScore = 666;
            audio.Play();
            Invoke("TriggerDoomMode", 9.25f);
        }

        if (dooming)
        {
            internalScore = 666;
        }
        this.transform.GetChild(1).GetComponent<Text>().text = "Current Score: " + internalScore.ToString();
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

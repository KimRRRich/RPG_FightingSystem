using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetting : MonoBehaviour
{
    public GameObject HowToPlay;
    public GameObject ESC;
    float time = 0;
    bool Switch = false;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 90;
        HowToPlay.SetActive(true);
        
        //Destroy(HowToPlay,4);
    }

    // Update is called once per frame
    void Update()
    {
        if (Switch == false)
        {
            time += Time.deltaTime;
            if (time > 5 || Input.anyKeyDown)
            {
                HowToPlay.SetActive(false);
                Switch = true;

            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ESC.activeSelf) ESC.SetActive(false);
            else
            {
                HowToPlay.SetActive(false);
                ESC.SetActive(true);
            }
            
        }
        if (ESC.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.J)|| Input.GetKeyDown(KeyCode.K)|| Input.GetKeyDown(KeyCode.L)|| Input.GetKeyDown(KeyCode.LeftAlt))
            {
                ESC.SetActive(false);
            }
        }
        
    }

    public void ButtonEvent_Quit()
    {
        ESC.SetActive(false);
        Application.Quit();
    }
    public void ButtonEvent_Restart()
    {
        ESC.SetActive(false);
        SceneManager.LoadScene("Game");
    }
    public void ButtonEvent_HowToPlay()
    {
        HowToPlay.SetActive(true);
        ESC.SetActive(false);
    }

    public void PlayerDead()
    {
        ESC.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject[] menu; // 0 = Main menu, 1 = Instructions menu, 2 = Options menu //
    public GameObject greet;
    public bool[] back; // 0 = Main menu,  1 = Instructions menu, 2 = options // 

    private void Start()
    {
        AudioManager.instance.PlayMusic("mainMusic");
    }
    private void Update()
    {
        
        if (menu[0].activeInHierarchy)
        {
            greet.SetActive(true);
        }
        else
        {
            greet.SetActive(false);
        }
    }
    public void StartButton()
    {
        SceneManager.LoadScene(1);
        AudioManager.instance.PlayMusic("mainMusic");
    }
    public void InstuctionsButton()
    {
        menu[0].SetActive(false);
        menu[1].SetActive(true);
        if (menu[1].activeInHierarchy)
        {
            back[1] = true;
            
        }
        else
        {
            back[1] = false;
            
        }
    }
    public void OptionsButton()
    {
        menu[0].SetActive(false);
        menu[2].SetActive(true);
        if (menu[2].activeInHierarchy)
        {
            back[2] = true;
            
        }
        else
        {
            back[2] = false;
        }
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void BackButton()
    {
        if (back[1])
        {
            menu[1].SetActive(false);
            menu[0].SetActive(true);
        }
        if (back[2])
        {
            menu[2].SetActive(false);
            menu[0].SetActive(true);
        }
    }
}

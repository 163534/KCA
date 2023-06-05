using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject[] menu; // 0 = Main menu, 1 = Instructions menu, 2 = Options menu //
    public bool[] back; // 0 = Main menu,  1 = Instructions menu, 2 = options // 
    bool cursorOn;
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    private void Update()
    {
        if (cursorOn)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
        }
    }
    public void InstuctionsButton()
    {
        menu[0].SetActive(false);
        menu[1].SetActive(true);
        if (menu[1].activeInHierarchy)
        {
            back[1] = true;
            cursorOn = true;
        }
        else
        {
            back[1] = false;
            cursorOn = false;
        }
    }
    public void OptionsButton()
    {
        menu[0].SetActive(false);
        menu[2].SetActive(true);
        if (menu[2].activeInHierarchy)
        {
            back[2] = true;
            cursorOn = true;
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

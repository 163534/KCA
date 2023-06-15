using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public GameObject winMenu;
    public bool win;
    // Start is called before the first frame update
    void Start()
    {
        win = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (win)
        {
            winMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            winMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            win = true;
            //SceneManager.LoadScene(0);
        }
    }
}

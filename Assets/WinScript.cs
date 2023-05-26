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
        winMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Win();
    }
    void Win()
    {
        if (win)
        {
            winMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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

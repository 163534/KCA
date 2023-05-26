using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    public GameObject winMenu;
    bool win;
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
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            win = true;
        }
    }
}

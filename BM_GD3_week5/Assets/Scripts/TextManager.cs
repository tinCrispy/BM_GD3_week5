using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
 //   public TMP_Text minesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   //     minesText.text = "hello";
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] int NextLevel;
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player")
        {
            EnteredExit();
        }
    }

    private void EnteredExit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextLevel);
    }
}

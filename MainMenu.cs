using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    private void Start()
    {
        FindObjectOfType<AudioManager>().Jouer("Theme4");
    }
    public void JouerJeu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitterJeu()
    {
        Debug.Log("c'est fini la");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_clipSelectPiece;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(m_clipSelectPiece, Camera.main.transform.position);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int m_score = 0;

    const int m_minPieces = 3;
    const int m_maxPieces = 5;

    public Text m_scoreText;
    public Text m_LetsGoText;

    public int ptsEarned = 0;

    public LevelManager m_levelManager;

    private void Start()
    {
        UpdateInterface();
    }

    public void Score(int points)
    {
        points = Mathf.Clamp(points, m_minPieces, m_maxPieces);

        ptsEarned = 0;
        switch (points)
        {
            case 3:
                ptsEarned = 40;
                break;
            case 4:
                ptsEarned = 70;
                break;
            case 5:
                ptsEarned = 100;
                break;
            default:
                break;
        }
        m_score += ptsEarned;
        m_levelManager.XpUp(ptsEarned);
        UpdateInterface();
    }

    void UpdateInterface()
    {
        m_scoreText.text = padZero(m_score, 5);
    }

    public void Reset()
    {
        m_score = 0;
        m_levelManager.Reset();
        UpdateInterface();
    }

    public string padZero(int n, int padDigits = 5)
    {
        string str = n.ToString();

        while (str.Length < padDigits)
            str = "0" + str;

        return str;
    }
}

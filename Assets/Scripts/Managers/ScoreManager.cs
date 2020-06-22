using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int m_score = 0;

    const int m_minPieces = 3;
    const int m_maxPieces = 5;

    public Text m_scoreText;
    public Text m_LetsGoText;

    private void Start()
    {
        UpdateInterface();
    }

    public void Score(int points)
    {
        points = Mathf.Clamp(points, m_minPieces, m_maxPieces);

        int ptsEarned = 0;
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
        Debug.Log("Ganhou: " + ptsEarned.ToString() + " pts");
        UpdateInterface();
    }

    void UpdateInterface()
    {
        m_scoreText.text = padZero(m_score, 5);
    }

    public void Reset()
    {
        m_score = 0;
        UpdateInterface();
    }

    string padZero(int n, int padDigits)
    {
        string str = n.ToString();

        while (str.Length < padDigits)
            str = "0" + str;

        return str;
    }
}

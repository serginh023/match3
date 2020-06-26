using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Slider m_sliderLevel;
    [SerializeField]
    private Text m_LabelLevel;
    private float levelXpNecessary = 400;
    private float nextLevelXP;
    private int currentLevel;

    public int levelJump = 200;

    void Start()
    {
        m_sliderLevel.minValue = 0;
        m_sliderLevel.maxValue = 10;
        nextLevelXP = levelXpNecessary;
        currentLevel = 1;
    }

    public void XpUp(float xp)
    {
        m_sliderLevel.value += m_sliderLevel.maxValue * xp / nextLevelXP;

        if (m_sliderLevel.value >= m_sliderLevel.maxValue)
        {
            LevelUp();
        }

        UpdateUI();
    }

    public void LevelUp()
    {
        Debug.Log("levelUp!");
        currentLevel++;
        levelXpNecessary += levelJump;
        m_sliderLevel.value = 0;

    }

    public void UpdateUI()
    {
        m_LabelLevel.text = "Level: " + currentLevel.ToString();
    }

    public void Reset()
    {
        m_sliderLevel.minValue = 0;
        m_sliderLevel.maxValue = 10;
        nextLevelXP = levelXpNecessary;
        currentLevel = 1;
        UpdateUI();
    }

}

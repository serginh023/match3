using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
    private const int BEST_SCORE = 0;
    private const string BEST_SCORE_KEY = "bestscorekey";

    [SerializeField]
    private Spawner m_Spawner;
    [SerializeField]
    private Board m_Board;

    private Piece m_activePiece1 = null;
    private Piece m_activePiece2 = null;

    [SerializeField]
    private AudioClip m_clipSelectPiece;

    [SerializeField]
    private AudioClip m_clipClearPieces;

    [SerializeField]
    private Timer m_Timer;

    [SerializeField]
    private GameObject m_CanvasGameOver;

    [SerializeField]
    private GameObject m_CanvasOverlay;

    [SerializeField]
    private GameObject m_CanvasPause;

    private bool m_IsPaused = false;

    [SerializeField]
    private ScoreManager m_scoreManager;

    [SerializeField]
    private Text m_GameOverScoreText;
    [SerializeField]
    private Text m_GameOverBestScoreText;


    void Start()
    {
        m_Board.DrawEmptyCells();

        StartGame();
    }

    private void StartGame()
    {
        Shuffle();

        m_Board.VerifyGridCombination(true);

        m_Board.VerifyAnyChance();

        while (!m_Board.m_hasValidChance)
        {
            Shuffle();
            m_Board.VerifyAnyChance();
        }

        TimerStart();
    }

    private void Shuffle()
    {
        for (int i = 0; i < m_Board.m_width; i++)
            for (int j = 0; j < m_Board.m_height - m_Board.m_header; j++)
            {
                m_Board.ErasePosition(i, j);
                Piece piece = m_Spawner.SpawnAtPosition(new Vector3(i, j, m_Board.m_posz));
                m_Board.StorePieceInGrid(piece);
            }
    }

    private void VerifyPieces(Piece piece)
    {
        if (m_activePiece1 == null)
        {
            m_activePiece1 = piece;
            AudioSource.PlayClipAtPoint(m_clipSelectPiece, Camera.main.transform.position);
            return;
        }
        else if (m_activePiece2 == null && m_activePiece1 != piece)
        {
            m_activePiece2 = piece;
            AudioSource.PlayClipAtPoint(m_clipSelectPiece, Camera.main.transform.position);
        }

        if (m_activePiece1 != null && m_activePiece2 != null)
        {
            if( m_Board.VerifyCouple(m_activePiece1.transform.position, m_activePiece2.transform.position) )
            {
                SwapPiecesPositions();

                m_Board.VerifyGridCombination(true);

                if (m_Board.m_won)
                    AudioSource.PlayClipAtPoint(m_clipClearPieces, Camera.main.transform.position);
                else
                    SwapPiecesPositions();
            }

            m_activePiece1.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            m_activePiece2.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            m_activePiece1 = null;
            m_activePiece2 = null;
        }

        while (m_Board.m_won)
            m_Board.VerifyGridCombination(true);

        //O audio de 'clear' não está aqui pois como não tem animação de clear vai ficar um áudio em cima do outro e não fica bom
        m_Board.VerifyAnyChance();

        if (!m_Board.m_hasValidChance)
        {
            GameOver();
        }

    }

    private void SwapPiecesPositions()
    {
        Vector3 aux = m_activePiece1.transform.position;
        m_activePiece1.transform.position = m_activePiece2.transform.position;
        m_activePiece2.transform.position = aux;

        m_Board.StorePieceInGrid(m_activePiece1);
        m_Board.StorePieceInGrid(m_activePiece2);
    }

    private void TimerStart()
    {
        if (m_Timer != null)
            m_Timer.StartTimer();
        else
            Debug.LogWarning("GAMECONTROLLER -> TIMERSTART m_Timer not vinculated");
    }

    private void TimerStopped()
    {
        GameOver();
    }

    private void GameOver()
    {
        m_CanvasGameOver.SetActive(true);
        m_GameOverScoreText.text = m_scoreManager.padZero(m_scoreManager.m_score);
        if (PlayerPrefs.HasKey(BEST_SCORE_KEY))
        {
            m_GameOverBestScoreText.text = m_scoreManager.padZero(PlayerPrefs.GetInt(BEST_SCORE_KEY));

            if (PlayerPrefs.GetInt(BEST_SCORE_KEY) < m_scoreManager.m_score)

                PlayerPrefs.SetInt(BEST_SCORE_KEY, m_scoreManager.m_score);
        }
        else
            PlayerPrefs.SetInt(BEST_SCORE_KEY, m_scoreManager.m_score);
        

        m_CanvasOverlay.SetActive(false);
        TogglePause();
    }

    public void TogglePause()
    {
        m_Board.SetBoardActive(!m_IsPaused);
        m_IsPaused = !m_IsPaused;
        
        Debug.Log("ispaused: " + m_IsPaused);
    }

    public void Restart()
    {
        TogglePause();

        m_CanvasGameOver.SetActive(m_IsPaused);

        m_CanvasOverlay.SetActive(!m_IsPaused);

        m_Board.SetBoardActive(!m_IsPaused);

        m_scoreManager.Reset();

        StartGame();
    }

    public void Pause()
    {
        TogglePause();

        m_CanvasPause.SetActive(m_IsPaused);

        m_CanvasOverlay.SetActive(!m_IsPaused);

        m_Board.SetBoardActive(!m_IsPaused);
    }



    private void OnEnable()
    {
        Piece.ClickEvent += VerifyPieces;
        Timer.m_EventEndTimer += TimerStopped;
    }

    private void OnDisable()
    {
        Piece.ClickEvent -= VerifyPieces;
        Timer.m_EventEndTimer -= TimerStopped;
    }

}

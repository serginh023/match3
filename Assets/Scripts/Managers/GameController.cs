﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        m_Board.DrawEmptyCells();
        Shuffle();

        m_Board.VerifyGridCombination();
        while (m_Board.m_won)
        {
            m_Board.VerifyGridCombination();
        }
    }

    private void Shuffle()
    {
        for (int i = 0; i < m_Board.m_width; i++)
            for (int j = 0; j < m_Board.m_height - m_Board.m_header; j++)
            {
                Piece piece = m_Spawner.SpawnAtPosition(new Vector3(i, j, 0));
                m_Board.StorePieceInGrid(piece);
            }
    }

    private void VerifyPieces(Piece piece)
    {
        //m_Board.VerifyRow((int)piece.transform.position.y);
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
                Vector3 aux = m_activePiece1.transform.position;
                m_activePiece1.transform.position = m_activePiece2.transform.position;
                m_activePiece2.transform.position = aux;

                m_Board.StorePieceInGrid(m_activePiece1);
                m_Board.StorePieceInGrid(m_activePiece2);
                m_Board.VerifyGridCombination();

                if (m_Board.m_won)
                {
                    AudioSource.PlayClipAtPoint(m_clipClearPieces, Camera.main.transform.position);
                }

            }

            m_activePiece1 = null;
            m_activePiece2 = null;
        }

        while (m_Board.m_won)
        {
            m_Board.VerifyGridCombination();
            //O audio de 'clear' não está aqui pois como não tem animação de clear vai ficar um áudio em cima do outro e não fica bom
        }

    }

    private void TimerStart()
    {

    }


    private void OnEnable()
    {
        Piece.ClickEvent += VerifyPieces;
    }

    private void OnDisable()
    {
        Piece.ClickEvent -= VerifyPieces;
    }

}

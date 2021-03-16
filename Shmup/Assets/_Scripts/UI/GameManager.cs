using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public enum GameState { MENU, GAME, PAUSE, ENDGAME, NEWGAME };
    public GameState gameState { get; private set; }
    public float vidas;
    public int pontos;
    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new GameManager();
        }

        return _instance;
    }
    private GameManager()
    {
        vidas = 5f;
        pontos = 0;
        gameState = GameState.MENU;
    }
    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;
    public void ChangeState(GameState nextState)
    {
        if (nextState == GameState.NEWGAME) Reset();
        gameState = nextState;
        changeStateDelegate();

        if (nextState == GameState.GAME) Continue();
        gameState = nextState;
        changeStateDelegate();
    }

    private void Reset()
    {
        vidas = 5f;
        pontos = 0;
    }

    private void Continue()
    {
    }
}

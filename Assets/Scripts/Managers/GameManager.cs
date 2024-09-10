using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Actions
    public Action<GameState> OnGameStateChanged;
    public Action OnMoveNumberDecrease;
    public Action<PlayerState> OnPlayerStateChanged;
    public Action<AimState> OnAimStateChanged;
    public Action OnEnemyDied;
    #endregion
    #region States
    public PlayerState CurrentPlayerState { get; private set; }
    public GameState CurrentGameState { get; private set; }
    public AimState CurrentAimState { get; private set; }
    #endregion
    [SerializeField] private EnemySpawnManager enemySpawnManager;
    public static GameManager Instance { get; private set; }
   
    private int score = 0;
    private int enemyCount;


    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
        }
        CurrentGameState = GameState.WaitingInput;
        CurrentPlayerState = PlayerState.Idle;
    }
    private void Start()
    {
        enemyCount = enemySpawnManager.EnemyCount;
    }
    public int GetScore() => score;

    public void ChangeGameState(GameState state)
    {
        if (CurrentGameState != state)
        {
            CurrentGameState = state;
            OnGameStateChanged?.Invoke(state);
        }
    }

    public void ChangePlayerState(PlayerState state)
    {
        if (CurrentPlayerState != state)
        {
            CurrentPlayerState = state;
            OnPlayerStateChanged?.Invoke(state);
        }
    }
    public void ChangeAimState(AimState state)
    {
        if (CurrentAimState != state)
        {
            CurrentAimState = state;
            OnAimStateChanged?.Invoke(state);
        }
    }

    public void UpdateScore()
    {
        score += 20;
    }

    private void UpdateEnemyCount()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            ChangeGameState(GameState.Win);
        }
    }
    private void OnEnable()
    {
        OnEnemyDied += UpdateScore;
        OnEnemyDied += UpdateEnemyCount;
    }
    private void OnDisable()
    {
        OnEnemyDied -= UpdateScore;
        OnEnemyDied -= UpdateEnemyCount;
    }
}

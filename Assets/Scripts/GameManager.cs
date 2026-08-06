using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<GameState> OnStateChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [Header("Timer")]
    public float CurrentTime { get; private set; }
    [SerializeField] private float maxTime;

    [Header("Lives")]
    public int CurrentLives { get; private set; }
    [SerializeField] private int maxLives;

    [Header("Diamonds")]
    public int TotalDiamonds { get; private set; }
    public int CollectedDiamonds { get; private set; }
    public GameState CurrentState { get; private set; }

    void Start()
    {
        CurrentLives = maxLives;
        CurrentTime = maxTime;
        CurrentState = GameState.Running;
    }

    void Update()
    {
        if (!IsInState(GameState.Running)) return;

        CurrentTime = Mathf.Max(0f, CurrentTime - Time.deltaTime);

        if (CurrentTime < 0)
        {
            SetState(GameState.Lost);
        }

    }

    public void SetTotalDiamonds(int amount)
    {
        TotalDiamonds = amount;
    }

    public void TakeDamage()
    {
        if (!IsInState(GameState.Running)) return;

        CurrentLives--;

        if (CurrentLives <= 0)
        {
            SetState(GameState.Lost);
        }
    }

    public void CollectDiamond()
    {
        if (!IsInState(GameState.Running)) return;

        CollectedDiamonds++;

        if (CollectedDiamonds >= TotalDiamonds)
        {
            SetState(GameState.Won);
        }
    }

    public bool IsInState(GameState state)
    {
        return CurrentState == state;
    }

    public void SetState(GameState state)
    {
        if (CurrentState == state)
        {
            return;
        }

        CurrentState = state;

        OnStateChanged?.Invoke(CurrentState);
    }
}

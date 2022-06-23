using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            textScore.text = value.ToString();

            textAnimator.SetTrigger(ScoreAnimation);
        } 
    }
    private int _score;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI textStart;
    [SerializeField] private TextMeshProUGUI textTryAgain;
    [SerializeField] private TextMeshPro textScore;
    [SerializeField] private Animator textAnimator;
    
    public bool isGameStart = false;
    
    private static readonly int ScoreAnimation = Animator.StringToHash("Score");
    private static readonly int StartAnimation = Animator.StringToHash("Start");
    private static readonly int Restart = Animator.StringToHash("Restart");

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        textAnimator.SetTrigger(StartAnimation);
    }

    public void OnClick_StartButton()
    {
        AudioManager.Instance.Play(AudioType.Click);
        
        Score = 1;

        textAnimator.ResetTrigger(StartAnimation);
        textAnimator.ResetTrigger(Restart);
     
        isGameStart = true;
        _canvas.enabled = false;
        
        BallController.Instance.OnStart();
    }

    public void OnGameOver()
    {
        isGameStart = false;
        
        _canvas.enabled = true;
        textStart.enabled = false;
        textTryAgain.enabled = true;
        
        textAnimator.SetTrigger(Restart);
    }
}

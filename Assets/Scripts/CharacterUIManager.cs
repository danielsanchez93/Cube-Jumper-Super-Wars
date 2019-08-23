using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

public static class Variables
{
    public static int startingLives=3;

}

public class CharacterUIManager : MonoBehaviour, IChatacterManager
{
    [SerializeField]
    TMPro.TextMeshProUGUI livesText;
    [SerializeField]
    TMPro.TextMeshProUGUI ScoreText;
    AudioSource audioSource;
    public AudioClip lose;
    public AudioClip damaged;
    public AudioClip coin;
    public UnityEvent OnDestroyed;

    public event Action OnChatacterDied;
    public event Action<int> OnScoreChanged;

    int score;
    int lives;
    public int Lives
    {
        get
        {
           return Variables.startingLives;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    private void Awake()
    {
        ScoreText.text = "Score: " + score.ToString();
        livesText.text = "Lifes: " + Variables.startingLives.ToString();
        OnChatacterDied = delegate
           {
               OnDestroyed.Invoke();
           };

        OnScoreChanged = delegate (int scr)
        {
            score +=scr;
            audioSource.PlayOneShot(coin);
            ScoreText.text = "Score: " + score.ToString();
        };
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(OnDestroyed == null)
            OnDestroyed = new UnityEvent();

        
    }

    public void IncreaseScore()
    {
        score++;
        ScoreText.text = "Score: " + score.ToString();
        OnScoreChanged(1);
    }

    public void DecreaseLives()
    {
        Variables.startingLives--;
        audioSource.PlayOneShot(damaged);
        livesText.text = "Lifes: "+ Variables.startingLives.ToString();
        if(Variables.startingLives <= 0)
        {
            OnChatacterDied();
            audioSource.PlayOneShot(lose);
        }
    }

}

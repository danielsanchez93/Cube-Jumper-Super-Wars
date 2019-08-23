using System;
using UnityEngine;

public interface IChatacterManager 
{
    event Action OnChatacterDied;
    event Action<int> OnScoreChanged;
    int Lives { get; }
    int Score { get; }
}

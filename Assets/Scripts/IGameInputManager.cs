using UnityEngine;
using System;

public interface IGameInputManager
{
    event Action<Vector2> OnTouchStarted;
    event Action<Vector2> OnTouchEnded;
    event Action<Vector2> OnTapped;
    
    
    
}

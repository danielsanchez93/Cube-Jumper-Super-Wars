using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class GameInputManager : MonoBehaviour, IGameInputManager
{
    public bool isTouching;
    public event Action<Vector2> OnTouchStarted;
    public event Action<Vector2> OnTouchEnded;
    public event Action<Vector2> OnTapped;

    Rigidbody2D rb;
    AudioSource audioSource;
    public AudioClip bounce;
    public float speed;
    public float jumpForce;


    private void Awake()
    {
        OnTouchStarted = delegate (Vector2 pos)
         {
             isTouching = true;
             pos = Input.mousePosition;
             Vector3 touchPosinWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.nearClipPlane));
             Jump();
         };

        OnTouchEnded = delegate (Vector2 pos)
        {
            isTouching = false;
        };
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Jump()
    {
        rb.AddForce(Vector2.up*jumpForce);
        audioSource.PlayOneShot(bounce);
    }

    private void Update()
    {
#if UNITY_ANDROID
            if(Input.touchCount>0 && !isTouching)
            {
               Vector2 pos = Input.touches[0].position;
               OnTouchStarted(pos);
            }
#else
        if(Input.GetMouseButtonDown(0) && !isTouching)
            {
                Vector2 pos = Input.mousePosition;
                OnTouchStarted(pos);
        }
#endif
        rb.velocity = new Vector2(speed,rb.velocity.y);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boundaries"))
        {
            speed *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Floor"))
        {
            OnTouchEnded(Vector2.zero);
        }
    }

}

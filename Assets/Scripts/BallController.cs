using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController Instance { get; private set; }
    
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed;
    [SerializeField] private CameraShake cameraShake;
    
    private void Awake()
    {
        Instance = this;
    }

    public void OnStart()
    {
        _rigidbody2D.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        _rigidbody2D.AddForce(Vector2.down * speed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        StartCoroutine(cameraShake.Shake(.09f, .06f));
        
        if (col.transform.CompareTag("Racket"))
        {
            AudioManager.Instance.Play(AudioType.Hit);
            
            var racket = col.transform.GetComponent<RacketController>();
            var directionVertical = racket.isUp ? -1 : 1;
            var directionHorizontal = (transform.position.x - racket.transform.position.x) / col.collider.bounds.extents.x;
            
            _rigidbody2D.AddForce(new Vector2(directionHorizontal, directionVertical) * speed);
            
            racket.PlayParticles();
        }

        if (col.transform.CompareTag("Goal"))
        {
            AudioManager.Instance.Play(AudioType.Score);
            GameManager.Instance.Score++;
            OnStart();
        }
        
        if (col.transform.CompareTag("GoalGameOver"))
        {
            AudioManager.Instance.Play(AudioType.GameOver);
            GameManager.Instance.OnGameOver();
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}

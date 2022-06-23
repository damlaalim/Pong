using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedAi;
    [SerializeField] private float limitHorizontal = 1.7f;
    [SerializeField] private ParticleSystem colParticle;
    
    public bool isUp;
    public bool isPlayer;

    private void Update()
    {
        if (!GameManager.Instance.isGameStart)
            return;
        
        var newPosition = transform.position;
        
        if (isPlayer)
        {
            var input = Input.GetAxis("Horizontal");
            newPosition = transform.position + (Vector3.right * (input * speed * Time.deltaTime));
        }
        else
        {
            var ai = Mathf.Lerp(newPosition.x, BallController.Instance.transform.position.x, speedAi * Time.deltaTime * GameManager.Instance.Score);
            newPosition.x = ai;
        }

        newPosition.x = Mathf.Clamp(newPosition.x, -limitHorizontal, limitHorizontal);
        transform.position = newPosition;
    }

    public void PlayParticles()
    {
        colParticle.Play();
    }
}

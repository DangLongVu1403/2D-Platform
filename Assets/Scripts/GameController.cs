using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector3 checkpointPos;
    Rigidbody2D playerRb;
    public ParticleController particleController;
    public Animator flashAnimator;
    // Start is called before the first frame update
    void Start()
    {
        checkpointPos = transform.position;
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            Die();
        }
    }
    void Die()
    {
        flashAnimator.SetTrigger("Flash");
        particleController.PlayDieParticle();
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.velocity = Vector2.zero;
        playerRb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }

    public void UpdateCheckPoint(Vector3 pos)
    {
        checkpointPos = pos;
    }
}

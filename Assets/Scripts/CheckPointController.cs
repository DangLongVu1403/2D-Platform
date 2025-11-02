using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public Transform respawnPoint;
    GameController controller;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Sprite checkPoint;
    [SerializeField] Sprite checkPointActive;
    Collider2D col;

    [SerializeField] private AudioSource checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        sprite.sprite = checkPoint;
        col = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkpoint.PlayOneShot(checkpoint.clip);
            controller.UpdateCheckPoint(respawnPoint.position);
            sprite.sprite = checkPointActive;
            col.enabled = false;
        }
    }
}

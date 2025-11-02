using System.Collections;
using UnityEngine;

public class TelePort : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    GameObject player;
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] private AudioSource teleSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
                StartCoroutine(TeleportCoroutine());
        }
    }

    IEnumerator TeleportCoroutine()
    {
        teleSound.PlayOneShot(teleSound.clip);
        rb.simulated = false;
        anim.SetTrigger("Portal In");
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.5f);
        player.transform.position = targetPos.position;
        //rb.velocity = Vector2.zero;
        anim.SetTrigger("Portal Out");
        yield return new WaitForSeconds(0.1f);
        rb.simulated = true;
    }

    IEnumerator MoveInPortal()
    {
        float timer = 0f;
        while (timer < 0.5f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}

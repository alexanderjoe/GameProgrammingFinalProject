using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    private bool _isCollected;

    public GameState gameState;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        _isCollected = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_isCollected)
        {
            _isCollected = true;
            // set invisible
            GetComponent<SpriteRenderer>().enabled = false;
            var playerStats = other.gameObject.GetComponent<PlayerStats>();
            playerStats.SetHealth(100);
        }
    }
}

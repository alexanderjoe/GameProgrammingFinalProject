using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private AudioSource _coinSound;
    private bool _isCollected;

    void Start()
    {
        _isCollected = false;
        _coinSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_isCollected)
        {
            _isCollected = true;
            _coinSound.Play();
            // set invisible
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
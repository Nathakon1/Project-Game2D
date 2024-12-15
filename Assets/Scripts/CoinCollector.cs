using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject coin;
    public GameObject star;
    private bool isCoinCollected;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        isCoinCollected = false;
        star.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCoinCollected)
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        audioManager.PlaySFX(audioManager.point);
        coin.SetActive(false);
        star.SetActive(true);
        isCoinCollected = true;
    }
}

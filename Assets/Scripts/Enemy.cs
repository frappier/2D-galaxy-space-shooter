using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player;
    private Animator _enemyDestoyedAnim;

    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if ( _player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _enemyDestoyedAnim = GetComponent<Animator>();

        if ( _enemyDestoyedAnim == null)
        {
            Debug.LogError("Enemy Destroyed Anim is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if(transform.position.y < -6.5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 6.4f, 0);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if(player != null)
            {
                player.Damage();
            }

            _enemyDestoyedAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }
        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(Random.Range(8, 11));
            }

            _enemyDestoyedAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }
    }
}

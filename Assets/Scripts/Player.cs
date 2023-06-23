using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private int _speedBoostMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private SpawnManager _spawnManager;

    private bool _isTripleshotActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;
    
   
    // Start is called before the first frame update
    void Start()
    {
        _shieldVisualizer.SetActive(false);
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.Log("The UI Managaer is NULL");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }

    }

    //Create a method to hold all that is movement related
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
                
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);   //putting the direction into a variable to use in the Translate
       
        transform.Translate(direction * _speed * Time.deltaTime);
       
        //Restricting on the Y axis using Math.Clamp
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.8f, 1.5f), 0);
        if(transform.position.y > 1.5f)
        {
            transform.position = new Vector3(transform.position.x, 1.5f, 0);
        }
        else
        {
             if(transform.position.y < -4.8f)
            {
                transform.position = new Vector3(transform.position.x, -4.8f, 0);
            }
        }

        //Making your player wrap on the X axis
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;
       

        if (_isTripleshotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
             
    }

    public void TripleShot()
    {
        _isTripleshotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleshotActive = false;
    }

    public void SpeedBoost()
    {
        //_isSpeedBoostactive = true;
        _speed *= _speedBoostMultiplier;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        //_isSpeedBoostactive = false;
        _speed /= _speedBoostMultiplier;
    }

    public void ShieldPowerup()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
                
        _lives -= 1;
                
        if(_lives < 1)
        {
            _spawnManager.IsPlayerDead();
            Destroy(this.gameObject);
            
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}

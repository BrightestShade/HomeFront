using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; 

public class PlayerBullet : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.5f;
    //public InputActionReference shootAction; // input 
    private float nextFireTime = 0f;
    public int maxAmmo = 6;
    public float reloadTime = 2f;
    private int currentAmmo;
    private bool isReloading = false;

    [Header("UI")]
    public Button fireButton;
    public Image ammoImage;
    public Sprite fullBulletSprite;
    public Sprite emptyBulletSprite;
    public Sprite spriteFor1Bullet;
    public Sprite spriteFor2Bullets;
    public Sprite spriteFor3Bullets;
    public Sprite spriteFor4Bullets;

    [Header("Shop")]
    private PlayerController playerController;
    private CurrencyManager cm; 
    public bool pistolUnlocked = false;
    public GameObject pistolObject;
    public int itemCost = 150;

    private Dictionary<int, Sprite> ammoSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerController = GetComponent<PlayerController>();

        currentAmmo = maxAmmo;

        ammoSprites = new Dictionary<int, Sprite>
        {
            {6, fullBulletSprite},
            {5, fullBulletSprite},
            {4, spriteFor4Bullets},
            {3, spriteFor3Bullets},
            {2, spriteFor2Bullets},
            {1, spriteFor1Bullet},
            {0, emptyBulletSprite}
        };

        UpdateAmmoUI();

        if (fireButton != null)
        {
            fireButton.onClick.AddListener(OnFireButtonPressed);
        }
             
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnEnable()
    {
        if (pistolUnlocked)
        {
            shootAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        shootAction.action.Disable();
    }*/

    public void TryBuyItem()
    {
        if (cm == null)
        {
            Debug.LogWarning("CurrencyManager is not assigned!");
            return;
        }

        if (pistolUnlocked)
        {
            Debug.Log("You already purchased the pistol.");
            return;
        }

        if (cm.currencyCount >= itemCost)
        {
            cm.currencyCount -= itemCost;
            pistolUnlocked = true;

            if (pistolObject != null)
                pistolObject.SetActive(true);

            //shootAction.action.Enable();

            Debug.Log("Pistol purchased and unlocked!");
        }
        else
        {
            Debug.Log("Not enough currency.");
        }
    }

    /*public void OnFireButtonPressed()
    {
        if (!isReloading && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                if(shootAction.action.triggered)
                {
                    Shoot();
                }

                currentAmmo--;
                UpdateAmmoUI();
                nextFireTime = Time.time + fireRate;

                if (currentAmmo <= 0)
                    StartCoroutine(Reload());
            }
        }
    }*/

    public void OnFireButtonPressed()
    {
        if (playerController != null && playerController.IsShopOpen)
        {
            Debug.Log("Cannot shoot while shop is open.");
            return;
        }

        if (!isReloading && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                /*if (shootAction.action.triggered)
                {
                    Shoot();
                }*/

                Shoot();

                currentAmmo--;
                UpdateAmmoUI();
                nextFireTime = Time.time + fireRate;

                if (currentAmmo <= 0)
                    StartCoroutine(Reload());
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        GameObject nearestEnemys = FindNearestEnemys();
        if (nearestEnemys != null)
        {
            bullet.GetComponent<Bullet>().SetTarget(nearestEnemys.transform.position);
        }
        else
        {
            // Default direction: based on player's facing rotation
            Vector3 direction = transform.right; // Player's local "forward" direction
            Vector3 defaultTarget = bulletSpawnPoint.position + direction;
            bullet.GetComponent<Bullet>().SetTarget(defaultTarget);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoImage != null && ammoSprites.ContainsKey(currentAmmo))
        {
            ammoImage.sprite = ammoSprites[currentAmmo];
        }
    }

    GameObject FindNearestEnemys()
    {
        GameObject[] lzEnemies = GameObject.FindGameObjectsWithTag("LZ");
        GameObject[] bzEnemies = GameObject.FindGameObjectsWithTag("BZ");
        
        List<GameObject> allEnemies = new List<GameObject>();
        allEnemies.AddRange(lzEnemies);
        allEnemies.AddRange(bzEnemies);
        
        GameObject nearestEnemys = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in allEnemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemys = enemy;
            }
        }

        return nearestEnemys;
    }
}
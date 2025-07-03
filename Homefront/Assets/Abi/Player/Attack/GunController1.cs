using UnityEngine;
using UnityEngine.UI;

public class GunController1 : MonoBehaviour
{
    [Header("Gun Controller 1")]
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject attackButton;

    public void OnButton1()
    {
        PlayerBullet playerBulletScript = GetComponent<PlayerBullet>();
        playerBullet.SetActive(true); 
    }

    public void OnButton2()
    {
        Bullet bulletScript = GetComponent<Bullet>();
        bullet.SetActive(true);
    }

    public void OnButton3()
    {
        attackButton.SetActive(true);
    }
}

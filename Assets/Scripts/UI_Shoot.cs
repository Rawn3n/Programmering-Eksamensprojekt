using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class UI_Shoot : MonoBehaviour
{

    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private TMP_Text textCooldown;
    
    
    [SerializeField]
    private Image imagePowerUP;
    private Sprite NonePowerUpImage;
    [SerializeField]
    private Image PwrUPimageCooldown;
    //[SerializeField]
    //private TMP_Text textCooldown;


    private bool isCooldown = false;
    private float cooldownTime = 10.0f;
    private float cooldownTimer = 0.0f;

    private bool isPowerupCooldown = false;
    private float powerupCooldownTime = 0.0f;
    private float powerupCooldownTimer = 0.0f;

    private TankShooting tankShooting;


    public void SetTank(TankShooting tank)
    {
        tankShooting = tank;
        tankShooting.OnTankShoot += StartCooldown;
        tankShooting.OnPowerupPickup += SetPowerupImage;
        tankShooting.powerupDuration += CooldownPowerup;
    }

    private void OnDisable()
    {
        tankShooting.OnTankShoot -= StartCooldown;
        tankShooting.OnPowerupPickup -= SetPowerupImage;
        tankShooting.powerupDuration -= CooldownPowerup;
    }

    void Start()
    {
        NonePowerUpImage = imagePowerUP.sprite;
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }

    void Update()
    {
        if (isCooldown)
        {
            ApplyCooldown();
        }
        if (isPowerupCooldown)
        {
            ApplyPowerupCooldown();
        }
    }

    public void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        //Debug.Log(cooldownTimer); - tester om timer tæller ned

        if (cooldownTimer <= 0.0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            textCooldown.text=Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }

    }

    private void ApplyPowerupCooldown()
    {
        powerupCooldownTimer -= Time.deltaTime;

        if (powerupCooldownTimer <= 0.0f)
        {
            isPowerupCooldown = false;
            PwrUPimageCooldown.fillAmount = 0.0f;
            RemovePowerupImage(); // Fjern powerup billedet når det udløber
        }
        else
        {
            PwrUPimageCooldown.fillAmount = powerupCooldownTimer / powerupCooldownTime;
        }
    }

    private void SetPowerupImage(Sprite sprite)
    {
        imagePowerUP.sprite = sprite;
        imagePowerUP.gameObject.SetActive(true);
    }

    private void RemovePowerupImage()
    {
        imagePowerUP.sprite = NonePowerUpImage; ;  
        Debug.Log("removePowerup image");
    }
    

    private void StartCooldown(float duration)
    {
        cooldownTime = duration;
        cooldownTimer = duration;
        isCooldown = true;

        textCooldown.gameObject.SetActive(true);
        imageCooldown.fillAmount = 1f;
    }
    public void CooldownPowerup(float duration)
    {
        powerupCooldownTime = duration;
        powerupCooldownTimer = duration;
        isPowerupCooldown = true;
        PwrUPimageCooldown.fillAmount = 1f;
    }

}

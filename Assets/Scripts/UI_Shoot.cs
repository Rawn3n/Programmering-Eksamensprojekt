using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;
using System.Collections;

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
   

    //til shoot håndtering
    private float cooldownTime = 10.0f;
    private float cooldownTimer = 0.0f;
    
    //til powerup håndtering
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

    

    private IEnumerator ApplyCooldown()
    {
        cooldownTimer = cooldownTime;
        while (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
            yield return null; // vent én frame
        }
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;

    }

    private IEnumerator ApplyPowerupCooldown()
    {
        powerupCooldownTimer = powerupCooldownTime;
        while (powerupCooldownTimer > 0.0f)
        {
            powerupCooldownTimer -= Time.deltaTime;
            PwrUPimageCooldown.fillAmount = powerupCooldownTimer / powerupCooldownTime;
            yield return null;
        }
        PwrUPimageCooldown.fillAmount = 0.0f;
        RemovePowerupImage();
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
       

        textCooldown.gameObject.SetActive(true);
        imageCooldown.fillAmount = 1f;

        StartCoroutine(ApplyCooldown());

        
    }
    public void CooldownPowerup(float duration)
    {
        powerupCooldownTime = duration;
        powerupCooldownTimer = duration;
       
        PwrUPimageCooldown.fillAmount = 1f;

        StartCoroutine(ApplyPowerupCooldown());
    }

}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shoot : MonoBehaviour
{

    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private TMP_Text textCooldown;

    private bool isCooldown = false;
    private float cooldownTime = 10.0f;
    private float cooldownTimer = 0.0f;

    [SerializeField] private TankShooting tankShooting;


    public void SetTank(TankShooting tank)
    {
        tankShooting = tank;
        tankShooting.OnTankShoot += StartCooldown;
    }

    private void OnDisable()
    {
        tankShooting.OnTankShoot -= StartCooldown;

    }

    void Start()
    {
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }

    void Update()
    {
        if (isCooldown)
        {
            ApplyCooldown();
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

    private void StartCooldown(float duration)
    {
        cooldownTime = duration;
        cooldownTimer = duration;
        isCooldown = true;

        textCooldown.gameObject.SetActive(true);
        imageCooldown.fillAmount = 1f;
    }

 
}

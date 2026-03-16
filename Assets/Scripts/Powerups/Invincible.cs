using UnityEngine;

public class Invincible : Powerups
{
    private GameObject shieldVisual;

    public override void StartPowerup(TankShooting tank)
    {
        HealthSystem h = tank.GetComponent<HealthSystem>();
        h.canTakeDamage = false; //kan ikke tage skade mere
        shieldVisual = CreateShield(tank.transform); // lav et visuelt skjold omkring tanken
    }

    public override void EndPowerup(TankShooting tank)
    {
        HealthSystem h = tank.GetComponent<HealthSystem>();
        h.canTakeDamage = true;
        if (shieldVisual != null)
        {
            Object.Destroy(shieldVisual);
        }
    }

    private GameObject CreateShield(Transform parent)
    {
        GameObject shield = new GameObject("ShieldVisual");
        shield.transform.SetParent(parent);
        shield.transform.localPosition = Vector3.zero;

        LineRenderer lr = shield.AddComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.loop = true;
        lr.widthMultiplier = 0.1f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.sortingLayerName = "Default";
        lr.sortingOrder = 5;
        lr.startColor = new Color(1f, 1f, 0f, 0.8f); // farvevalg = gul der er gennemsigtig
        lr.endColor = new Color(1f, 1f, 0f, 0.8f);


        // Teng cirklen / skoldet
        int segments = 40;
        float radius = 1.5f;
        lr.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI * 2f;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lr.SetPosition(i, new Vector3(x, y, 0f));
        }

        return shield;
    }
}



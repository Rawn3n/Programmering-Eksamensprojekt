using UnityEngine;
using System.Collections;


public abstract class Powerups : MonoBehaviour
{

    public IEnumerator PowerupDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        EndPowerup();
    }

    public abstract void EndPowerup();
}

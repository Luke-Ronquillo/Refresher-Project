using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Stat current;
    [SerializeField] Stat max;
    [SerializeField] Image bar;

    bool isBeingDamaged = false;
    private void Update()
    {
        if (current.amount < 0)
            current.amount = 0;
        if (current.amount > max.amount)
            current.amount = max.amount;
        bar.fillAmount = current.amount / max.amount;
    }
    IEnumerator DecreaseHealthOverTime (float amount)
    {
        current.amount -= amount;
        yield return new WaitForSeconds(2f);
        if (isBeingDamaged)
            StartCoroutine(DecreaseHealthOverTime(amount));
    }
    public void EnterDamageZone (float amount)
    {
        isBeingDamaged = true;
        StartCoroutine(DecreaseHealthOverTime(amount));
    }
    public void ExitDamageZone ()
    {
        isBeingDamaged = false;
    }
    public void IncreaseHealth (float amount)
    {
        current.amount += amount;
    }
}

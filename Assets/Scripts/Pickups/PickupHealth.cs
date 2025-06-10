using UnityEngine;

public class PickupHealth : Pickup
{
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddHealth();
        Destroy(gameObject);
    }
}
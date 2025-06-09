using UnityEngine;

public class PickupLife : Pickup
{
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddLife();
        Destroy(gameObject);
    }
}
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    int coin = 0;

    public int Coin
    {
        set => coin = Mathf.Clamp(value, 0, 9999);
        get => coin;
    }
}

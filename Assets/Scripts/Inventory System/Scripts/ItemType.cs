using UnityEngine;

namespace Inventory.sys
{
    public enum ItemType
    {
        [Tooltip("Things you can fight with")]
        Weapon,

        [Tooltip("To be Consumed")]
        Consummable,

        [Tooltip("Currency items")]
        Coin,

        [Tooltip("Temporary boosts or buffs")]
        PowerUp      
    }
}

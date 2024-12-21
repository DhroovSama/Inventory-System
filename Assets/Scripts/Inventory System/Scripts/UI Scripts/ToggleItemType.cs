using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.sys
{
    #region XML Documentation
    /// <summary>
    /// Associates a specific item type with a toggle for identification.
    /// </summary>
    #endregion
    public class ToggleItemType : MonoBehaviour
    {
        [Tooltip("The type of item represented by this toggle.")]
        public ItemType itemType;
    }
}

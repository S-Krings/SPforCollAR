// add this to NetworkIdentities for custom range if needed.
// only works with DistanceInterestManagement.
using UnityEngine;

namespace Mirror
{
    [DisallowMultipleComponent]
    public class PermissionInterestManagementOverride : NetworkBehaviour
    {
        [Tooltip("The maximum range that objects will be visible at.")]
        public int visRange = 20;
    }
}

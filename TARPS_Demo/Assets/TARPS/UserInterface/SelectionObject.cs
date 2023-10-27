using UnityEngine;

public class SelectionObject : MonoBehaviour
{
    //[Tooltip("Who or what do you want to protect?")]
    [Header("Protection Target")]
    [SerializeField] bool Users;
    [SerializeField] bool UsedData;
    [SerializeField] bool Bystanders;
    [Space]
    //[Tooltip("Who do you want to protect them from?")]
    [Header("Adversary")]
    [SerializeField] bool OtherUsers;
    [SerializeField] bool AppCreators;
    [SerializeField] bool OutsideAttackers;
    [Space]
    [Header("Protection Layer")]
    [Tooltip("What happens between the application instances")] [SerializeField] bool InterApplication;
    [Tooltip("What happens in one application instance")] [SerializeField] bool InApplication;
    [Tooltip("What happens in the real world")] [SerializeField] bool Physical;
}

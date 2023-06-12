using UnityEngine;
using UnityEditor;

public class SelectionWindow : EditorWindow
{
    [SerializeField] bool Users;
    [SerializeField] bool UsedData;
    [SerializeField] bool Bystanders;
    
    [SerializeField] bool OtherUsers;
    [SerializeField] bool AppCreators;
    [SerializeField] bool OutsideAttackers;

    [SerializeField] bool StaticInformationDisclosure;
    [SerializeField] bool SituationalInformationDisclosure;
    [SerializeField] bool UnwantedInput;

    [SerializeField] bool Occlusion;
    [SerializeField] bool Distraction;
    [SerializeField] bool Illusion;
    [SerializeField] bool UnwantedContentPlacement;

    [Tooltip("What happens between the application instances")] [SerializeField] bool InterApplication;
    [Tooltip("What happens in one application instance")] [SerializeField] bool InApplication;
    [Tooltip("What happens in the real world")] [SerializeField] bool Physical;

    private bool PrivacyShield;
    private bool PermissionMenu;
    private bool Dissolve;

    [MenuItem("PrivacySecurityToolbox/Configuration Window")]
    public static void ShowWindow()
    {
        GetWindow<SelectionWindow>("Configuration");
    }

    private void OnGUI()
    {
        GUILayout.Label("Protection Target", EditorStyles.boldLabel);
        Users = GUILayout.Toggle(Users, "Users");
        UsedData = GUILayout.Toggle(UsedData, "Used Data");
        Bystanders = GUILayout.Toggle(Bystanders, "Bystanders");

        GUILayout.Space(10);

        GUILayout.Label("Adversary", EditorStyles.boldLabel);
        OtherUsers = GUILayout.Toggle(OtherUsers, "Other Users");
        AppCreators = GUILayout.Toggle(AppCreators, "App Creators");
        OutsideAttackers = GUILayout.Toggle(OutsideAttackers, "Outside Attackers");

        GUILayout.Space(10);

        GUILayout.Label("Protection Layer", EditorStyles.boldLabel);
        StaticInformationDisclosure = GUILayout.Toggle(StaticInformationDisclosure, "Static Information Disclosure");
        SituationalInformationDisclosure = GUILayout.Toggle(SituationalInformationDisclosure, "Situational Information Disclosure");
        UnwantedInput = GUILayout.Toggle(UnwantedInput, "Unwanted Input");

        GUILayout.Space(10);

        GUILayout.Label("Input Protection Risks", EditorStyles.boldLabel);
        InterApplication = GUILayout.Toggle(InterApplication, "Inter-Application");
        InApplication = GUILayout.Toggle(InApplication, "In-Application");
        Physical = GUILayout.Toggle(Physical, "Physical");

        GUILayout.Space(5);

        GUILayout.Label("Output Protection Risks", EditorStyles.boldLabel);
        Occlusion = GUILayout.Toggle(Occlusion, "Occlusion");
        Distraction = GUILayout.Toggle(Distraction, "Distraction");
        Illusion = GUILayout.Toggle(Illusion, "Illusion");
        UnwantedContentPlacement = GUILayout.Toggle(UnwantedContentPlacement, "Unwanted Content Placement");

        if (GUILayout.Button("Find Protection Features"))
        {
            Debug.Log("Selection: \n"+
            "Users "+ Users+"\n"+
            "UsedData "+ UsedData+ "\n" +
            "Bystanders " + Bystanders+ "\n" +

            "OtherUsers " + OtherUsers+ "\n" +
            "AppCreators " + AppCreators+ "\n" +
            "OutsideAttackers " + OutsideAttackers+ "\n" +

            "StaticInformationDisclosure " + StaticInformationDisclosure+ "\n" +
            "SituationalInformationDisclosure " + SituationalInformationDisclosure+ "\n" +
            "UnwantedInput " + UnwantedInput+ "\n" +

            "Occlusion " + Occlusion+ "\n" +
            "Distraction " + Distraction+ "\n" +
            "Illusion " + Illusion+ "\n" +
            "UnwantedContentPlacement " + UnwantedContentPlacement);
            AddFeatures();
        }
        if (PermissionMenu)
        {
            EditorGUILayout.HelpBox("Permission Manager Button Added - What to do now?\n" +
            "  - Add the PermissionInterestManagement component to the GameObject your NetworkManager is on\n" +
            "  - Add the PermissionInterestManagementOverride component to objects that should always appear\n" +
            "  - Use the PermissionManager.singleton in your code:\n" +
            "     - Use AddStandardPermissions when spawining new objects\n" +
            "     - Use checkPermission when allowing actions that only authorized users should be able to do",
            MessageType.None);
        }
        if (PrivacyShield)
        {
            EditorGUILayout.HelpBox("Protection Shield Button Added - What to do now?\n" +
            "Nothing, you're done!",
            MessageType.None);
        }
        if (Dissolve)
        {
            EditorGUILayout.HelpBox("Dissolve Button Added - What to do now?\n" +
            "  - Add \"Dissolve\" tag to objects that should dissolve\n" +
            "  - Do not forget the prefabs that can be spawned",
            MessageType.None);
        }
    }

    private void AddFeatures()
    {
        GameObject menu = GameObject.Find("HandProtectionMenu");
        if (menu == null)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/PrivacySecurityToolkit/SharedAssets/Resources/HandProtectionMenu.prefab", typeof(GameObject));
            if (prefab == null)
            {
                Debug.Log("HandProtectionMenu missing");
                return;
            }
            menu = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Undo.RegisterCreatedObjectUndo(menu, "Create " + menu.name);
        }
        GameObject permissionManagerButton = null, shieldButton = null, dissolveButton = null;
        foreach (Transform child in menu.transform.GetChild(0).GetChild(0))
        {
            Debug.Log(child.name);
            if(child.name == "PermissionButton")
            {
                permissionManagerButton = child.gameObject;
            }
            if(child.name == "ShieldButton")
            {
                shieldButton = child.gameObject;
            }
            if(child.name == "ClearViewButton")
            {
                dissolveButton = child.gameObject;
            }
        }
        Debug.Log("Permission " + permissionManagerButton + " dheld: " + shieldButton + " dissolve: " + dissolveButton);

        if (!((Users||UsedData)&&OtherUsers && (StaticInformationDisclosure||SituationalInformationDisclosure)))
        {
            permissionManagerButton.SetActive(false);
            PermissionMenu = false;
        }
        else
        {
            PermissionMenu = true;
        }
        if(!(Users && OtherUsers && SituationalInformationDisclosure))
        {
            shieldButton.SetActive(false);
            PrivacyShield = false;
        }
        else
        {
            PrivacyShield = true;
        }
        if (!(Users && (OtherUsers||OutsideAttackers) && Occlusion))
        {
            dissolveButton.SetActive(false);
            Dissolve = false;
        }
        else
        {
            Dissolve = true;
        }
    }
}

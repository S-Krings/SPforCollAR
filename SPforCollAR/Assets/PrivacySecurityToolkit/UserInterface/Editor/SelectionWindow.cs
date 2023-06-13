using UnityEngine;
using UnityEditor;

public class SelectionWindow : EditorWindow
{
    [SerializeField] bool Users = true;
    [SerializeField] bool UsedData = true;
    [SerializeField] bool Bystanders = true;

    [SerializeField] bool OtherUsers = true;
    [SerializeField] bool AppCreators = true;
    [SerializeField] bool OutsideAttackers = true;

    [SerializeField] bool StaticInformationDisclosure = true;
    [SerializeField] bool SituationalInformationDisclosure = true;
    [SerializeField] bool UnwantedInput = true;

    [SerializeField] bool Occlusion = true;
    [SerializeField] bool Distraction = true;
    [SerializeField] bool Illusion = true;
    [SerializeField] bool UnwantedContentPlacement = true;

    [Tooltip("What happens between the application instances")] [SerializeField] bool InterApplication;
    [Tooltip("What happens in one application instance")] [SerializeField] bool InApplication;
    [Tooltip("What happens in the real world")] [SerializeField] bool Physical;

    private bool PrivacyShield;
    private bool PermissionMenu;
    private bool Dissolve;
    private bool Outline;

    private Vector2 scrollPos = Vector2.zero;
    private Vector2 scrollPosHelps = Vector2.zero;

    [MenuItem("PrivacySecurityToolbox/Configuration Window")]
    public static void ShowWindow()
    {
        GetWindow<SelectionWindow>("Configuration");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select your Protection Goals:", EditorStyles.boldLabel);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));
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
        EditorGUILayout.EndScrollView();

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
        
        scrollPosHelps = EditorGUILayout.BeginScrollView(scrollPosHelps, GUILayout.ExpandHeight(true));

            if (PermissionMenu)
            {
                EditorGUILayout.HelpBox("Permission Manager Button Added - What to do now?\n" +
                "  - Add the PermissionInterestManagement component to the GameObject your NetworkManager is on\n" +
                "  - Add the PermissionInterestManagementOverride component to objects that should always appear\n" +
                "  - Use the PermissionManager.singleton in your code:\n" +
                "     - Use AddStandardPermissions when spawining new objects\n" +
                "     - Use checkPermission when allowing actions that only authorized users should be able to do\n" +
                "Now, the PermissionManager protects Users or Used Data from Other Users by preventing Static Information Disclosure and/or Situational Information Disclosure.",
                MessageType.None);
            }
            if (PrivacyShield)
            {
                EditorGUILayout.HelpBox("Protection Shield Button Added - What to do now?\n" +
                "Nothing, you're done!\n" +
                "Now, the PrivacyShield protects Users from Other Users by preventing Situational Information Disclosure.",
                MessageType.None);
            }
            if (Dissolve)
            {
                EditorGUILayout.HelpBox("Dissolve Button Added - What to do now?\n" +
                "  - Add \"Dissolve\" tag to objects that should dissolve\n" +
                "  - Do not forget the prefabs that can be spawned\n" +
                "Now, the Dissolve feature protects Users from Other Users and/or Outside Attackers by preventing Occlusion.",
                MessageType.None);
            }
            if (Outline)
            {
                EditorGUILayout.HelpBox("Outline Button Added - What to do now?\n" +
                "  - Add \"Outline\" tag to objects that should become outlined\n" +
                "  - Do not forget the prefabs that can be spawned\n" +
                "Now, the Outline protects Users from OtherUsers and/or Outside Attackers by preventing Illusions.",
                MessageType.None);
            }
        EditorGUILayout.EndScrollView();
    }

    private void AddFeatures()
    {
        GameObject menu = GameObject.Find("HandProtectionMenu");
        if (menu == null)
        {
            Debug.Log("No ProtectionMenu in scene, adding new one");
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/PrivacySecurityToolkit/SharedAssets/Resources/HandProtectionMenu.prefab", typeof(GameObject));
            if (prefab == null)
            {
                Debug.Log("HandProtectionMenu missing");
                return;
            }
            menu = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Undo.RegisterCreatedObjectUndo(menu, "Create " + menu.name);
        }
        GameObject permissionManagerButton = null, shieldButton = null, dissolveButton = null, outlineButton = null;
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
            if(child.name == "OutlineButton")
            {
                outlineButton = child.gameObject;
            }
        }
        Debug.Log("Permission " + permissionManagerButton + " shield: " + shieldButton + " dissolve: " + dissolveButton + " outline: "+outlineButton);

        if (!((Users||UsedData)&&OtherUsers && (StaticInformationDisclosure||SituationalInformationDisclosure)))
        {
            permissionManagerButton.SetActive(false);
            PermissionMenu = false;
        }
        else
        {
            permissionManagerButton.SetActive(true);
            PermissionMenu = true;
        }

        if(!(Users && OtherUsers && SituationalInformationDisclosure))
        {
            shieldButton.SetActive(false);
            PrivacyShield = false;
        }
        else
        {
            shieldButton.SetActive(true);
            PrivacyShield = true;
        }

        if (!(Users && (OtherUsers||OutsideAttackers) && Occlusion))
        {
            dissolveButton.SetActive(false);
            Dissolve = false;
        }
        else
        {
            dissolveButton.SetActive(true);
            Dissolve = true;
        }

        if (!(Users && (OtherUsers||OutsideAttackers) && Illusion))
        {
            outlineButton.SetActive(false);
            Outline = false;
        }
        else
        {
            outlineButton.SetActive(true);
            Outline = true;
        }
    }
}

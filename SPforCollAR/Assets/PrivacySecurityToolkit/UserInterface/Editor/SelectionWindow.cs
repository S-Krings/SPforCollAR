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

    [SerializeField] bool InterApplication = true;
    [SerializeField] bool InApplication = true;
    [SerializeField] bool Physical = true;

    private bool PrivacyShield;
    private bool PermissionMenu;
    private bool Dissolve;
    private bool Outline;

    private Vector2 scrollPos = Vector2.zero;
    private Vector2 scrollPosHelps = Vector2.zero;

    [MenuItem("PrivacySecurityToolbox/Open Protection Configurator Window")]
    public static void ShowWindow()
    {
        GetWindow<SelectionWindow>("Protection Configurator");
    }

    private void OnGUI()
    {
        GUIStyle titlestyle = new GUIStyle();
        titlestyle.richText = true;
        titlestyle.wordWrap = true;
        titlestyle.normal.textColor = new Color32(0xc0, 0xc0, 0xc0, 0xff);//(192, 192, 192, 255);
        titlestyle.fontStyle = FontStyle.Bold;


        GUIStyle textstyle = GUI.skin.toggle;
        textstyle.richText = true;
        textstyle.wordWrap = true;
        textstyle.normal.textColor = new Color32(0xc0, 0xc0, 0xc0, 0xff);//(192, 192, 192, 255);

        GUILayout.Label("<size=16>Select your Protection Goals:</size>", titlestyle);
        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));

        GUILayout.Label("Who or what do you want to protect?", titlestyle);
        Users = GUILayout.Toggle(Users, "Users", textstyle);
        UsedData = GUILayout.Toggle(UsedData, "Used Data", textstyle);
        Bystanders = GUILayout.Toggle(Bystanders, "Bystanders", textstyle);

        GUILayout.Space(10);

        GUILayout.Label("From whom do you want to protect?", titlestyle);
        OtherUsers = GUILayout.Toggle(OtherUsers, "Other Users", textstyle);
        AppCreators = GUILayout.Toggle(AppCreators, "App Creators", textstyle);
        OutsideAttackers = GUILayout.Toggle(OutsideAttackers, "Outside Attackers", textstyle);

        /*GUILayout.Space(10);

        GUILayout.Label("Protection Layer", titlestyle);
        InterApplication = GUILayout.Toggle(InterApplication, "Inter-Application", textstyle);
        InApplication = GUILayout.Toggle(InApplication, "In-Application", textstyle);
        Physical = GUILayout.Toggle(Physical, "Physical", textstyle);*/

        GUILayout.Space(10);
        GUILayout.Label("What risks do you want to prevent?", titlestyle);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);
        EditorGUILayout.BeginVertical();

        GUILayout.Space(5);
        GUILayout.Label("What information going into the application do you want to protect?", titlestyle);
        StaticInformationDisclosure = GUILayout.Toggle(StaticInformationDisclosure, "Static Information (3D Objects, Documents, ...)", textstyle);
        SituationalInformationDisclosure = GUILayout.Toggle(SituationalInformationDisclosure, "Situational Information (User Movements, Camera View, ...)", textstyle);
        UnwantedInput = GUILayout.Toggle(UnwantedInput, "Prevent Unwanted Inputs", textstyle);

        GUILayout.Space(5);

        GUILayout.Label("From what things display/output issues do you want to protect?", titlestyle);
        Occlusion = GUILayout.Toggle(Occlusion, "Occlusion", textstyle);
        Distraction = GUILayout.Toggle(Distraction, "Distraction", textstyle);
        Illusion = GUILayout.Toggle(Illusion, "Illusion", textstyle);
        UnwantedContentPlacement = GUILayout.Toggle(UnwantedContentPlacement, "Unwanted Content Placement", textstyle);

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Apply Protection Features"))
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
                "       - Use AddStandardPermissions when spawining new objects\n" +
                "       - Use checkPermission when allowing actions that only authorized users should be able to do\n" +
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
                "  - Add Tag \"Dissolve\" to objects that should dissolve (or \"DissolveAndOutline\" if you also use the outline)\n" +
                "  - Do not forget to tag your prefabs\n" +
                "Now, the Dissolve feature protects Users from Other Users and/or Outside Attackers by preventing Occlusion.",
                MessageType.None);
            }
            if (Outline)
            {
                EditorGUILayout.HelpBox("Outline Button Added - What to do now?\n" +
                "  - Add Tag \"Outline\" to objects that should become outlined (or \"DissolveAndOutline\" if you also use dissolve)\n" +
                "  - Do not forget to tag your prefabs\n" +
                "Now, the Outline protects Users from Other Users and/or Outside Attackers by preventing Illusions.",
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

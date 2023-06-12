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
        }
    }

    private void FindFeatures()
    {
        if (Users && OtherUsers && SituationalInformationDisclosure)
        {

        }
    }

}

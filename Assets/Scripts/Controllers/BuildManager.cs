using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public static BuildManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BuildManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("BuildManager");
                    instance = singletonObject.AddComponent<BuildManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    void Start()
    {
        if (nodeUI == null)
        {
            // Change to use the static GameFacade reference
            GameManager.gameFacade.FindReferences();
        }
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        if (nodeUI != null)
        {
            nodeUI.SetTarget(node);
        }
    }

    public void DeselectNode()
    {
        if (selectedNode != null)
        {
            selectedNode = null;
        }

        if (nodeUI != null)
        {
            nodeUI.Hide();
        }
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}

using UnityEngine;
using UnityEngine.UIElements;
public class MyUIManager : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button myButton;
    public static MyUIManager instance;
    private VisualElement turretVisualElement;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }
    void Update()
    {

    }
    void FixedUpdate()
    {

    }
    public void EnableTurrets()
    {
        turretVisualElement.SetEnabled(true);

    }
    public void SetTurretVisualElement(Vector3 position)
    {
        turretVisualElement.transform.position = position;
    }

    void OnEnable()
    {
        // Get the UIDocument component attached to this GameObject
        uiDocument = GetComponent<UIDocument>();
        turretVisualElement = uiDocument.rootVisualElement.Q<VisualElement>("Turrets");
        turretVisualElement.BlockRaycasts(); //This optional extension method lets you register visual elements as if it were built in.
        UIToolkitRaycastChecker.RegisterBlockingElement(turretVisualElement); // Same effect as the above code, but this is more explicit that you are registering your element to some system you'll need to unregister it from.

        if (uiDocument != null)
        {
            // Access the root visual element
            VisualElement root = uiDocument.rootVisualElement;

            // Query for specific elements by their names (assigned in UI Builder)
            myButton = root.Q<Button>("Rocket");


            // Add event listeners
            if (myButton != null)
            {
                myButton.clicked += OnButtonClicked;
            }
            else
            {
                Debug.LogError("Button with name 'myButton' not found in the UI Document.");
            }

            root.Q<Button>("Gatling").clicked += () =>
                        {
                            UIInterface.instance.CreateGatling();
                        };
        }
        else
        {
            Debug.LogError("UIDocument component not found on this GameObject.");
        }
    }

    void OnButtonClicked()
    {
        UIInterface.instance.CreateTurret();

    }
    void OnDisable()
    {
        // Unregister the event listeners when the UI is disabled
        turretVisualElement.AllowRaycasts();
        UIToolkitRaycastChecker.UnregisterBlockingElement(turretVisualElement);
    }
}
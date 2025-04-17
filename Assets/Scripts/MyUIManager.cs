using UnityEngine;
using UnityEngine.UIElements;
public class MyUIManager : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button myButton;
    void Start()
    {

    }
    void Update()
    {

    }
    void FixedUpdate()
    {

    }
    void OnEnable()
    {
        // Get the UIDocument component attached to this GameObject
        uiDocument = GetComponent<UIDocument>();

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
}
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using Random = UnityEngine.Random;

public class EditorUtils : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        EditorApplication.playModeStateChanged += OnEnterPlayMode;
    }

    private static void OnEnterPlayMode(PlayModeStateChange obj)
    {
        if (obj == PlayModeStateChange.ExitingEditMode) AssetDatabase.Refresh(ImportAssetOptions.Default);
    }
    [MenuItem("Tools/Refresh &r")]
    private static void Refresh()
    {
        AssetDatabase.Refresh(ImportAssetOptions.Default);
    }
    // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MenuItem.html
    [MenuItem("Tools/Reset Position &g")]  // %g means Ctrl+G (Cmd+G on macOS)
    private static void ResetPosition()
    {
        if (Selection.activeGameObject != null)
        {
            // Change the position of the selected GameObject
            Selection.activeGameObject.transform.position = new Vector3(0, 0, 0);
            Debug.Log("Position changed to (0, 0, 0)");
        }
        else
        {
            Debug.LogWarning("No GameObject selected.");
        }
    }
    [MenuItem("Tools/Get Bound Size &s")]  // %g means Ctrl+G (Cmd+G on macOS)
    private static void GetBoundSize()
    {
        if (Selection.activeGameObject != null)
        {
            Debug.Log("Bounds size: " + Selection.activeGameObject.GetComponent<MeshRenderer>().bounds.size);
        }
        else
        {
            Debug.LogWarning("No GameObject selected.");
        }
    }
    [MenuItem("Tools/Reset Rotation")]  // %g means Ctrl+G (Cmd+G on macOS)
    private static void ResetRotation()
    {
        if (Selection.activeGameObject != null)
        {

            Selection.activeGameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Position changed to (0, 0, 0)");
        }
        else
        {
            Debug.LogWarning("No GameObject selected.");
        }
    }
    [MenuItem("Tools/Rigidbody")]
    private static void CreateRigidbody()
    {
        if (Selection.gameObjects != null)
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                Rigidbody rb = go.GetComponent<Rigidbody>();
                if (rb == null)
                    rb = go.AddComponent(typeof(Rigidbody)) as Rigidbody;
                rb.isKinematic = true;
            }
        }
        else
        {
            Debug.LogWarning("No GameObject selected.");
        }
    }
    [MenuItem("Tools/EmptyObject &e")]
    private static void CreateEmptyObject()
    {

        // if (string.IsNullOrEmpty(name))
        // {
        //     name = "GameManager";
        // }
        /*if (!File.Exists("Assets/Scripts/" + name + ".cs"))
        {
            File.WriteAllText("Assets/Scripts/" + name + ".cs", @"using UnityEngine;

public class " + name + @": MonoBehaviour
{
    void Awake()
    {
        var boundsSize = GetComponent<Renderer>().bounds.size;
        
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
}");
            
        }*/
        var name = EditorGUIUtility.systemCopyBuffer.Trim();
        if (string.IsNullOrEmpty(name))
        {
            name = "GameManager";
        }
        // if (Selection.activeGameObject != null)
        // {

        //     var emptyObject = new GameObject(name);
        //     emptyObject.transform.SetParent(Selection.activeGameObject.transform);
        //     emptyObject.AddComponent(Type.GetType(name + ",Assembly-Csharp"));
        // }

        var emptyObject = new GameObject(name);
        emptyObject.AddComponent(Type.GetType(name + ",Assembly-Csharp"));


    }
    [MenuItem("Tools/Plane")]
    private static void CreatePlane()
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
        obj.transform.position = new Vector3(0, 0, 0);


    }
    [MenuItem("Tools/Cube")]
    private static void CreateCube()
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = new Vector3(0, 0, 0);
        obj.AddComponent(typeof(Rigidbody));



    }
    [MenuItem("Tools/Capsule")]
    private static void Capsule()
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        obj.transform.position = new Vector3(0, 0, 0);
        obj.AddComponent(typeof(Rigidbody));

    }
    [MenuItem("Tools/Sphere")]
    private static void CreateSphere()
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.position = new Vector3(0, 0, 0);
        obj.AddComponent(typeof(Rigidbody));

    }
    [MenuItem("Tools/ParticleSystem")]
    private static void ParticleSystem()
    {
        var obj = new GameObject("ParticleSystem");
        ParticleSystem ps = obj.AddComponent<ParticleSystem>();
        ParticleSystemRenderer psr = obj.AddComponent<ParticleSystemRenderer>();
    }

    public static void AddTag(string tag)
    {
        UnityEngine.Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
        if ((asset != null) && (asset.Length > 0))
        {
            SerializedObject so = new SerializedObject(asset[0]);
            SerializedProperty tags = so.FindProperty("tags");

            for (int i = 0; i < tags.arraySize; ++i)
            {
                if (tags.GetArrayElementAtIndex(i).stringValue == tag)
                {
                    return;     // Tag already present, nothing to do.
                }
            }

            tags.InsertArrayElementAtIndex(0);
            tags.GetArrayElementAtIndex(0).stringValue = tag;
            so.ApplyModifiedProperties();
            so.Update();
        }
    }
    [MenuItem("Tools/Tag")]
    private static void Tag()
    {
        AddTag(EditorGUIUtility.systemCopyBuffer);
    }
    [MenuItem("Tools/RandomCubes")]
    private static void RandomCubes()
    {
        AddTag("Platform");
        float minX = -10;
        float maxX = 10;
        float minScale = 0.5f;
        float maxScale = 1.5f;
        for (int i = 0; i < 10; i++)
        {
            var x = Random.Range(minX, maxX);
            var z = Random.Range(minX, maxX);
            var scale = Random.Range(minScale, maxScale);
            var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.name = "Platform" + i;
            obj.tag = "Platform";
            obj.transform.position = new Vector3(x, scale / 2.0f, z);
            obj.transform.localScale = new Vector3(scale, scale, scale);
            obj.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Mat 3.mat", typeof(Material));
        }
    }
    [MenuItem("Tools/CreateMaterial")]  // &s means Alt+S
    private static void CreateMaterial()
    {
        var material = new Material(Shader.Find("Standard"));
        var dir = "/Materials/";
        Debug.Log(Application.dataPath + dir);
        if (!Directory.Exists(Application.dataPath + dir))
        {
            Directory.CreateDirectory(Application.dataPath + dir);
        }
        string path = Path.Combine("Assets" + dir, "Mat " + (Directory.GetFiles(Application.dataPath + dir, "*.mat").Length + 1) + ".mat"); // EditorUtility.SaveFilePanelInProject("Save Material", "NewMaterial.mat", "mat", "Please enter a file name to save the material to:");
        if (path.Length != 0)
        {
            AssetDatabase.CreateAsset(material, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = material;
        }
    }

    static void TraverseChildren(GameObject gameObject)
    {

        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.GetComponent<Renderer>() != null)
            {
                bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
            }
            if (child.transform.childCount > 0)
                TraverseChildren(child);
        }

    }
    static Bounds bounds;

    [MenuItem("Tools/Collider Fit to Children")]
    static void FitToChildren()
    {
        bounds = new Bounds(Vector3.zero, Vector3.zero);
        foreach (GameObject rootGameObject in Selection.gameObjects)
        {
            // if (!(rootGameObject.GetComponent<Collider>() is BoxCollider))
            //     continue;
            // bool hasBounds = false;

            // for (int i = 0; i < rootGameObject.transform.childCount; ++i)
            // {
            //     Renderer childRenderer = rootGameObject.transform.GetChild(i).GetComponent<Renderer>();
            //     if (childRenderer != null)
            //     {
            //         if (hasBounds)
            //         {
            //             bounds.Encapsulate(childRenderer.bounds);
            //         }
            //         else
            //         {
            //             bounds = childRenderer.bounds;
            //             hasBounds = true;
            //         }
            //     }
            // }
            TravserChildren(rootGameObject);
            float scale = rootGameObject.transform.localScale.x;
            bounds = new Bounds(bounds.center, bounds.size / scale);
            BoxCollider collider = (BoxCollider)rootGameObject.AddComponent(typeof(BoxCollider));
            collider.center = (bounds.center - rootGameObject.transform.position) / scale;
            collider.size = bounds.size;
        }
    }
}
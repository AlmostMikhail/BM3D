using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;
using UnityEngine.UI;


public class VertexEditingMain : MonoBehaviour
{
    public Rect rect;
    public List<ProBuilderMesh> proBuilderMeshList;
    public ProBuilderMesh proBuilderMeshMain;
    public Vector2 startPositionRec;
    public Vector2 endPositionRec;
    Dictionary<ProBuilderMesh, HashSet<int>> dicMeshSharedVerts;
    [SerializeField] CameraMovement6 cameraMovement6;
    [SerializeField] RotateCamera rotateCamera;
    [SerializeField] public Vector3 worldMousePosition;
    public List<SharedVertex> sharedVertexList;
    public Dictionary<int, int> dicLocalAndCommon;
    public Vector3 localVertices;
    public bool hasSelected;
    bool hasSetLocalVertsPos;
    float distanceToCamera;
    List<Vector3> localVertsList;
    bool vPressed;
    bool ePressed;
    bool fPressed;

    [SerializeField] Button vButton;
    [SerializeField] Button eButton;

    [SerializeField] EdgeEditingMain edgeEditingMain;
    [SerializeField] ExtrudeMain extrudeMain;
    [SerializeField] Renderer renderer1; 

    void Start()
    {
        /*
                // Access the wireShader
                string wireShader = BuiltinMaterials.lineShader;

                // Create a new Material using the wireShader
                Material wireMaterial = new Material(Shader.Find(wireShader));

                renderer1.sharedMaterial = wireMaterial;*/

        sharedVertexList = new List<SharedVertex>();
        dicLocalAndCommon = new Dictionary<int, int>();
        localVertsList = new List<Vector3>();
        IList<int> listVertsSelected = new List<int>();
    }
    void Update()
    {
        setDragPosition();
        if (!hasSelected)
        {
            rotateCamera.RotateCameraFunc();
            cameraMovement6.CameraMove();
        }

        if (vPressed)
        {
            moveVertexFunc();

        }
        else if (ePressed)
        {
            edgeEditingMain.SelectAndEditEdge();

        }
        else if (fPressed)
        {
            extrudeMain.FaceExtrudeFunc();

        }

        proBuilderMeshMain.ToMesh(MeshTopology.Quads);
        proBuilderMeshMain.Refresh();
    }

    void setDragPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPositionRec = Input.mousePosition;

            //the min x is the same as mouse x position
            //the screen height might be 1080 and the start might be 900
            //1080 - 900  = 180 distance from top left
            rect.min = new Vector2(startPositionRec.x, (Screen.height - startPositionRec.y));

        }
        endPositionRec = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {

            //The x position is on the x axis the mouse ends at
            //The y position is screen heigh minus endMouse Position
            rect.max = new Vector2(endPositionRec.x, Screen.height - endPositionRec.y);
        }
    }
    void moveVertexFunc()
    {
        if (Input.GetMouseButtonUp(0))
        {

            //select vertices in a rect and store in dictionary
            dicMeshSharedVerts = SelectionPicker.PickVerticesInRect(Camera.main, rect, proBuilderMeshList, PickerOptions.Default, 1);

            hasSelected = !hasSelected;
            if (!hasSelected)
            {
                dicMeshSharedVerts = null;
            }
            hasSetLocalVertsPos = false;
        }
        if (dicMeshSharedVerts != null)
        {
            //  Debug.Log("entered loop shared Verts ");
            //loop through dictionary
            foreach (KeyValuePair<ProBuilderMesh, HashSet<int>> meshAndVerts in dicMeshSharedVerts)
            {
                // Debug.Log("entered loop shared Verts 2");
                //loop through HashSet of verts selected
                foreach (int selectedVerts in meshAndVerts.Value)
                {
                    //  Debug.Log("entered loop shared Verts 3");
                    IList<SharedVertex> sharedVertexAll = proBuilderMeshMain.sharedVertices;

                    sharedVertexList.Add(sharedVertexAll[selectedVerts]);

                    SharedVertex.GetSharedVertexLookup(sharedVertexList, dicLocalAndCommon);

                    foreach (KeyValuePair<int, int> localAndCommon in dicLocalAndCommon)
                    {

                        //Debug.Log("entered loop shared Verts 4");

                        //local vertex position

                        Debug.Log("key position " + localAndCommon.Key);

                        if (!hasSetLocalVertsPos)
                        {
                            Vector3 cameraPosition = Camera.main.transform.position;

                            localVertices = proBuilderMeshMain.positions[localAndCommon.Key];

                            distanceToCamera = Vector3.Distance(localVertices, cameraPosition);

                            hasSetLocalVertsPos = true;
                        }


                        Debug.Log("local verts position" + localVertices);

                        //get mouse world position from mouse input
                        worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(endPositionRec.x, endPositionRec.y, distanceToCamera));

                        //convert worldMousePosition to local space of the mesh
                        //Vector3 localMousePosition = proBuilderMeshMain.transform.InverseTransformPoint(worldMousePosition);

                        VertexPositioning.SetSharedVertexPosition(proBuilderMeshMain, selectedVerts, worldMousePosition);

                        sharedVertexList.Clear();

                    }
                }
            }
        }
    }
    public void VPressedFunc()
    {
        vPressed = true;
        ePressed = false;
        fPressed = false;
        Debug.Log("Vertex pressed ");
    }
    public void EPressedFunc()
    {
        ePressed = true;
        vPressed = false;
        fPressed = false;
        Debug.Log("Edge pressed ");

    }
    public void FPressedFunc()
    {
        fPressed = true;
        vPressed = false;
        ePressed = false;
        Debug.Log("Face pressed ");

    }

}
// distanceFromCamToVerts = Camera.main.WorldToScreenPoint(new Vector3(0, 0, localVertices.z));
// distanceCameraVerts = Vector3.Distance(localVertices, Camera.main.transform.position);

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class EdgeEditingMain : MonoBehaviour
{
    HashSet<EdgeLookup> edgeLookups;
    Dictionary<ProBuilderMesh, HashSet<Edge>> probuilderEdgePair;
    Dictionary<int, int> sharedVertexEdgeSelect;
    [SerializeField] VertexEditingMain vertexEditingMain;
    [SerializeField] float distanceToCameraEdge;
    [SerializeField] int commonVertEdgeA;
    [SerializeField] int commonVertEdgeB;
    [SerializeField] Vector3 commonVertEdgeAPos;
    [SerializeField] Vector3 commonVertEdgeBPos;
    int localVertEdgeA;
    int localVertEdgeB;
    int commonEdgeA;
    int commonEdgeB;
    Dictionary<int, int> sharedVertexEdge;
    HashSet<int> listVertsEdge;
    Dictionary<int, int> sharedVertexDic;
    bool hasSelectedEdge;
    void Start()
    {
        sharedVertexEdge = new Dictionary<int, int>();
        sharedVertexEdgeSelect = new Dictionary<int, int>();
        edgeLookups = new HashSet<EdgeLookup>();
        listVertsEdge = new HashSet<int>();
        sharedVertexDic = new Dictionary<int, int>();
    }
    void Update()
    {

    }
    public void SelectAndEditEdge()
    {
        if (Input.GetMouseButtonUp(0))
        {
            probuilderEdgePair = SelectionPicker.PickEdgesInRect(Camera.main, vertexEditingMain.rect, vertexEditingMain.proBuilderMeshList, PickerOptions.Default, 1);

            vertexEditingMain.hasSelected = !vertexEditingMain.hasSelected;
            hasSelectedEdge = false;

        }
        if (probuilderEdgePair != null)
        {
            foreach (KeyValuePair<ProBuilderMesh, HashSet<Edge>> meshAndEdge in probuilderEdgePair)
            {

                foreach (Edge edges in meshAndEdge.Value)
                {
                    commonEdgeA = edges.a;
                    commonEdgeB = edges.b;
                    listVertsEdge.Add(commonEdgeA);
                    listVertsEdge.Add(commonEdgeB);
                }

                IList<SharedVertex> sharedVertices = vertexEditingMain.proBuilderMeshList[0].sharedVertices;

                SharedVertex.GetSharedVertexLookup(sharedVertices, sharedVertexDic);

                edgeLookups = EdgeLookup.GetEdgeLookupHashSet(meshAndEdge.Value, sharedVertexDic);


                foreach (EdgeLookup edgeLookupList in edgeLookups)
                {
                    commonVertEdgeA = edgeLookupList.common.a;
                    commonVertEdgeB = edgeLookupList.common.b;

                    localVertEdgeA = edgeLookupList.local.a;
                    localVertEdgeB = edgeLookupList.local.b;
                }

                commonVertEdgeAPos = vertexEditingMain.proBuilderMeshList[0].positions[localVertEdgeA];
                commonVertEdgeBPos = vertexEditingMain.proBuilderMeshList[0].positions[localVertEdgeB];

                Vector3 cameraPosition = Camera.main.transform.position;

                Vector3 midPointEdge = (commonVertEdgeAPos + commonVertEdgeBPos) / 2;

                if (!hasSelectedEdge)
                {
                    distanceToCameraEdge = Vector3.Distance(midPointEdge, cameraPosition);
                    hasSelectedEdge = true;
                }

                vertexEditingMain.worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(vertexEditingMain.endPositionRec.x, vertexEditingMain.endPositionRec.y, distanceToCameraEdge));
                
                Vector3 positionDifference = vertexEditingMain.worldMousePosition - midPointEdge;

                VertexPositioning.TranslateVertices(vertexEditingMain.proBuilderMeshList[0],meshAndEdge.Value,positionDifference);

                sharedVertexEdgeSelect.Clear();
                edgeLookups.Clear();
                vertexEditingMain.sharedVertexList.Clear();
            }
        }
    }
}

//  IList<SharedVertex> sharedVertexAll = vertexEditingMain.proBuilderMeshList[0].sharedVertices;

//vertexEditingMain.sharedVertexList.Add(sharedVertexAll[commonVertEdgeA]);
//vertexEditingMain.sharedVertexList.Add(sharedVertexAll[commonVertEdgeB]);

// SharedVertex.GetSharedVertexLookup(vertexEditingMain.sharedVertexList, vertexEditingMain.dicLocalAndCommon);

            //  VertexPositioning.SetSharedVertexPosition(vertexEditingMain.proBuilderMeshList[0], commonVertEdgeA,
            //      vertexEditingMain.worldMousePosition + new Vector3(-0.5f,0,0));
            //     VertexPositioning.SetSharedVertexPosition(vertexEditingMain.proBuilderMeshList[0], commonVertEdgeB,
            //      vertexEditingMain.worldMousePosition + new Vector3(0.5f,0,0));

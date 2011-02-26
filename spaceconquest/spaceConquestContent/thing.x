xof 0303txt 0032


template VertexDuplicationIndices { 
  <b8d65549-d7c9-4995-89cf-53a9a8b031e3>
  DWORD nIndices;
  DWORD nOriginalVertices;
  array DWORD indices[nIndices];
 }
 template XSkinMeshHeader {
  <3cf169ce-ff7c-44ab-93c0-f78f62d172e2>
  WORD nMaxSkinWeightsPerVertex;
  WORD nMaxSkinWeightsPerFace;
  WORD nBones;
 }
 template SkinWeights {
  <6f0d123b-bad2-4167-a0d0-80224f25fabb>
  STRING transformNodeName;
  DWORD nWeights;
  array DWORD vertexIndices[nWeights];
  array float weights[nWeights];
  Matrix4x4 matrixOffset;
 }

Frame RootFrame {

  FrameTransformMatrix {
    1.000000,0.000000,0.000000,0.000000,
    0.000000,1.000000,0.000000,0.000000,
    0.000000,0.000000,1.000000,0.000000,
    0.000000,0.000000,0.000000,1.000000;;
  }
Frame Sphere {

  FrameTransformMatrix {
    1.000000,0.000000,0.000000,0.000000,
    0.000000,1.000000,0.000000,0.000000,
    0.000000,0.000000,1.000000,0.000000,
    0.000000,0.000000,0.000000,1.000000;;
  }
Mesh {
30;
0.000000; 0.000000; -1.000000;,
-0.433000; 0.750000; -0.500000;,
0.866000; 0.000000; -0.500000;,
-0.433000; 0.750000; 0.500000;,
0.866000; 0.000000; 0.500000;,
0.866000; 0.000000; -0.500000;,
-0.433000; 0.750000; -0.500000;,
0.866000; 0.000000; 0.500000;,
-0.433000; 0.750000; 0.500000;,
0.000000; 0.000000; 1.000000;,
-0.433000; 0.750000; 0.500000;,
-0.433000; -0.750000; 0.500000;,
0.000000; 0.000000; 1.000000;,
-0.433000; 0.750000; -0.500000;,
-0.433000; -0.750000; -0.500000;,
-0.433000; -0.750000; 0.500000;,
-0.433000; 0.750000; 0.500000;,
0.000000; 0.000000; -1.000000;,
-0.433000; -0.750000; -0.500000;,
-0.433000; 0.750000; -0.500000;,
0.000000; 0.000000; -1.000000;,
0.866000; 0.000000; -0.500000;,
-0.433000; -0.750000; -0.500000;,
0.866000; 0.000000; 0.500000;,
-0.433000; -0.750000; 0.500000;,
-0.433000; -0.750000; -0.500000;,
0.866000; 0.000000; -0.500000;,
-0.433000; -0.750000; 0.500000;,
0.866000; 0.000000; 0.500000;,
0.000000; 0.000000; 1.000000;;
9;
3; 0, 1, 2;,
4; 3, 4, 5, 6;,
3; 7, 8, 9;,
3; 10, 11, 12;,
4; 13, 14, 15, 16;,
3; 17, 18, 19;,
3; 20, 21, 22;,
4; 23, 24, 25, 26;,
3; 27, 28, 29;;
  MeshMaterialList {
    0;
    9;
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0;;
    }  //End of MeshMaterialList
  MeshNormals {
30;
    0.000000; 0.000000; -1.000000;,
    -0.400830; 0.694266; -0.597736;,
    0.801660; 0.000000; -0.597736;,
    -0.400830; 0.694266; 0.597736;,
    0.801660; 0.000000; 0.597736;,
    0.801660; 0.000000; -0.597736;,
    -0.400830; 0.694266; -0.597736;,
    0.801660; 0.000000; 0.597736;,
    -0.400830; 0.694266; 0.597736;,
    0.000000; 0.000000; 1.000000;,
    -0.400830; 0.694266; 0.597736;,
    -0.400830; -0.694266; 0.597736;,
    0.000000; 0.000000; 1.000000;,
    -0.400830; 0.694266; -0.597736;,
    -0.400830; -0.694266; -0.597736;,
    -0.400830; -0.694266; 0.597736;,
    -0.400830; 0.694266; 0.597736;,
    0.000000; 0.000000; -1.000000;,
    -0.400830; -0.694266; -0.597736;,
    -0.400830; 0.694266; -0.597736;,
    0.000000; 0.000000; -1.000000;,
    0.801660; 0.000000; -0.597736;,
    -0.400830; -0.694266; -0.597736;,
    0.801660; 0.000000; 0.597736;,
    -0.400830; -0.694266; 0.597736;,
    -0.400830; -0.694266; -0.597736;,
    0.801660; 0.000000; -0.597736;,
    -0.400830; -0.694266; 0.597736;,
    0.801660; 0.000000; 0.597736;,
    0.000000; 0.000000; 1.000000;;
9;
3; 0, 1, 2;,
4; 3, 4, 5, 6;,
3; 7, 8, 9;,
3; 10, 11, 12;,
4; 13, 14, 15, 16;,
3; 17, 18, 19;,
3; 20, 21, 22;,
4; 23, 24, 25, 26;,
3; 27, 28, 29;;
}  //End of MeshNormals
 }
}
}
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif

[CustomEditor(typeof(BoxColliderScript))]
public class BoxCollider2DEditor : Editor  {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BoxColliderScript backGroundScript = (BoxColliderScript)target;
        if (GUILayout.Button("Create BoxCollider2D"))
        {
            backGroundScript.CreateBoxCollider();
        }
    }
}

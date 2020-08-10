using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    Spawner m_spawner;
    Object m_scoreManager;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameController gameController = (GameController)target;

        GUILayout.BeginVertical();
        GUILayout.Label(" ");
        GUILayout.Label("Test Solutions GUI", EditorStyles.boldLabel);
        GUILayout.Label(" ");
        GUILayout.EndVertical();

        GUILayout.Label("New Float Test", EditorStyles.boldLabel);
        gameController.TestSolutionsFloat = EditorGUILayout.Slider("Test New Float Value", .5f, 0, 1f);

        GUILayout.Label("New Custom Type Test", EditorStyles.boldLabel);
        gameController.GameObjectTestSolutions = EditorGUILayout.ObjectField(m_scoreManager, typeof(Object), true);



    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyFieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyFieldOfView fow = (EnemyFieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        //Handles.DrawWireArc(fow.transform.position, Vector3.right, Vector3.up, 360, fow.viewRadius);
        //Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.up, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngleHorizontalGeneral / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngleHorizontalGeneral / 2, false);

        Vector3 viewAngleC = fow.DirFromAngle(-fow.viewAngleHorizontalShootAngle / 2, false);
        Vector3 viewAngleD = fow.DirFromAngle(fow.viewAngleHorizontalShootAngle / 2, false);

        Vector3 viewAngleE = fow.DirFromAngleVertical(-fow.viewAngleVertical / 2, false);
        Vector3 viewAngleF = fow.DirFromAngleVertical(fow.viewAngleVertical / 2, false);

        Vector3 viewAngleG = fow.DirFromAngleVertical(-fow.viewAnglePeripheralVertical / 2, false);
        Vector3 viewAngleH = fow.DirFromAngleVertical(fow.viewAnglePeripheralVertical / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleC * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleD * fow.viewRadius);

        /*
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleE * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleF * fow.viewRadius);
        
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleG * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleH * fow.viewRadius);
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScale : MonoBehaviour
{
    private BodyPart currentBodyPart = BodyPart.None;
    private GameObject model = GameObject.Find("HumanMale_Character_FREE");


    public ModelScale()
    {

    }

    enum BodyPart
    {
        RightArm,
        LeftArm,
        RightLeg,
        LeftLeg,
        Body,
        None
    }

    public void ScaleModel(List<Vector3> vectorList, UnityEditor.BodyPart bodyPart)
    {
        switch (bodyPart)
        {
            case UnityEditor.BodyPart.RightArm:

                break;
            case UnityEditor.BodyPart.LeftArm:

                break;
            case UnityEditor.BodyPart.RightLeg:

                break;
            case UnityEditor.BodyPart.LeftLeg:

                break;
            case UnityEditor.BodyPart.Body:
                Debug.Log("Here");

                Transform ankle_r = this.model.transform.Find("Armature/Root_M/Hip_R/Knee_R/Ankle_R");
                Transform ankle_l = this.model.transform.Find("Armature/Root_M/Hip_L/Knee_L/Ankle_L");
                Transform knee_r = this.model.transform.Find("Armature/Root_M/Hip_R/Knee_R");
                Transform knee_l = this.model.transform.Find("Armature/Root_M/Hip_L/Knee_L");
                Transform hip_r = this.model.transform.Find("Armature/Root_M/Hip_R");
                Transform hip_l = this.model.transform.Find("Armature/Root_M/Hip_L");
                Transform chest = this.model.transform.Find("Armature/Root_M/Spine1_M/Spine2_M/Chest_M");


                float ankle_knee = vectorList[1].y - vectorList[0].y;
                float ankle_hip = vectorList[2].y - vectorList[0].y;
                float ankle_chest = vectorList[3].y - vectorList[0].y;
                Vector3 worldPositionAnkle_r = ankle_r.TransformPoint(Vector3.zero);
                Vector3 worldPositionAnkle_l = ankle_l.TransformPoint(Vector3.zero);

                Vector3 newWorldPositionKnee_r = worldPositionAnkle_r + new Vector3(0, ankle_knee, 0);
                Vector3 newWorldPositionKnee_l = worldPositionAnkle_l + new Vector3(0, ankle_knee, 0);
                knee_l.position = newWorldPositionKnee_l;
                knee_r.position = newWorldPositionKnee_r;

                Vector3 newWorldPositionHip_r = worldPositionAnkle_r + new Vector3(0, ankle_hip, 0);
                Vector3 newWorldPositionHip_l = worldPositionAnkle_l + new Vector3(0, ankle_hip, 0);
                hip_l.position = newWorldPositionHip_l;
                hip_r.position = newWorldPositionHip_r;

                Vector3 newWorldPositionChest = worldPositionAnkle_r + new Vector3(0, ankle_chest,0);
                chest.position = newWorldPositionChest;

                //Vector3 worldPositionAnkle = ankle_r.TransformPoint(Vector3.zero);

                //Vector3 newWorldPosition = worldPositionAnkle + new Vector3(0, ankle_knee, 0);
                //knee_r.position = newWorldPosition;

                //newWorldPosition = worldPositionAnkle + new Vector3(0, ankle_hip, 0);
                //hip_r.position = newWorldPosition;

                //newWorldPosition = worldPositionAnkle + new Vector3(0, ankle_chest, 0);
                //chest.position = newWorldPosition;
                break;
            case UnityEditor.BodyPart.None:

                break;
            default:

                break;
        }
    }


    void start()
    {

    }

    void update()
    {

    }
}

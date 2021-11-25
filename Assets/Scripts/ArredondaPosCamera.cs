using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

///<summary> Classe que faz o arredondamento da câmera para diminuir o "tremido"
public class ArredondaPosCamera : CinemachineExtension
{
    public float PixelsPerUnit = 3;

    /*
    
    */
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state,
        float deltaTime)
    {
        if(stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.FinalPosition;
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y),pos.z); //Arredonda posição
            state.PositionCorrection += pos2 - pos; //Correção
        }
    }

    /*
    Metodo auxiliar que faz o arredondamento de cada componente (x e y)
    */
    float Round(float x)
    {
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}

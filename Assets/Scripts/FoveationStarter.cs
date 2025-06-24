using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class  FoveationStarter: MonoBehaviour
{
    List<XRDisplaySubsystem> xrDisplay = new List<XRDisplaySubsystem>();
    public InputAction foveator;
    void Start()
    {
        foveator.Enable();
        SubsystemManager.GetSubsystems(xrDisplay);
        if (xrDisplay.Count == 1)
        { 
          xrDisplay[0].foveatedRenderingFlags = XRDisplaySubsystem.FoveatedRenderingFlags.None;
          xrDisplay[0].foveatedRenderingLevel = 1.0f; // Full strength
          Debug.Log($"upd ----------------------  {xrDisplay[0].foveatedRenderingFlags} flags, strength: {xrDisplay[0].foveatedRenderingLevel}.");
        }
    }

    void Update()
    {
        var action = foveator.WasPressedThisFrame();
        if (action) toggleFoveation();
        // toggleFoveation();
    }

    void toggleFoveation()
    {
        xrDisplay[0].foveatedRenderingLevel = (xrDisplay[0].foveatedRenderingLevel > 0.0f) ? 0.0f : 1.0f;
        Debug.Log($"upd ----------------------  Foveation strength: {xrDisplay[0].foveatedRenderingLevel}.");
    }

}

// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR;

// public class FoveationStarter : MonoBehaviour
// {

//   void Start()
//   {
//     SubsystemManager.GetSubsystems(xrDisplays);
//     if (xrDisplays.Count == 1)
//     { 
//       xrDisplays[0].foveatedRenderingLevel = 1.0f; // Full strength
//     }
//   }
//   void Update() {
//     Debug.Log(xrDisplays[0].foveatedRenderingLevel);
//   }
// }
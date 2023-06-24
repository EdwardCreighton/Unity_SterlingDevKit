using System.Collections.Generic;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.FourWheel
{
    public class CarController : GameLoopEntity
    {
        [SerializeField] private List<WheelComponent> wheelComponents;
    }
}

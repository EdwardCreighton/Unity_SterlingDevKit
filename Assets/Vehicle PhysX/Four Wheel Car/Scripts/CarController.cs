using System.Collections.Generic;
using UnityEngine;

namespace SterlingAssets.VehiclePhysX.FourWheel
{
    [SelectionBase]
    public class CarController : GameLoopEntity
    {
        [SerializeField] private Rigidbody vehicleRigidbody;
        [SerializeField] private List<Collider> vehicleColliders;

        [SerializeField] private Transform tyreFL;
        [SerializeField] private Transform hubcapFL;
        [SerializeField] private Transform caliperFL;
        [SerializeField] private Transform brakeDiskFL;

        [SerializeField] private Transform tyreFR;
        [SerializeField] private Transform hubcapFR;
        [SerializeField] private Transform caliperFR;
        [SerializeField] private Transform brakeDiskFR;

        [SerializeField] private Transform tyreRL;
        [SerializeField] private Transform hubcapRL;
        [SerializeField] private Transform caliperRL;
        [SerializeField] private Transform brakeDiskRL;

        [SerializeField] private Transform tyreRR;
        [SerializeField] private Transform hubcapRR;
        [SerializeField] private Transform caliperRR;
        [SerializeField] private Transform brakeDiskRR;

        [SerializeField] private SuspensionProfile suspensionProfile;
        [SerializeField] private FrictionProfile frictionProfile;

        [SerializeField] private WheelComponent wheelComponentFL;
        [SerializeField] private WheelComponent wheelComponentFR;
        [SerializeField] private WheelComponent wheelComponentRL;
        [SerializeField] private WheelComponent wheelComponentRR;
    }
}

using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
    public class WheelComponent_v2 : GameLoopEntity
    {
        [SerializeField] private Transform tyreTransform;
        [SerializeField] private Transform hubcapTransform;
        [SerializeField] private Transform caliperTransform;
        [SerializeField] private Transform brakeDiskTransform;

        public Transform TyreTransform
        {
            get => tyreTransform;
            set => tyreTransform = value;
        }
        public Transform HubcapTransform
        {
            get => hubcapTransform;
            set => hubcapTransform = value;
        }
        public Transform CaliperTransform
        {
            get => caliperTransform;
            set => caliperTransform = value;
        }
        public Transform BrakeDiskTransform
        {
            get => brakeDiskTransform;
            set => brakeDiskTransform = value;
        }
        
    }
}
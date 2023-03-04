using UnityEngine;

namespace SterlingAssets.VehiclePhysX
{
	public partial class WheelComponent
	{
		public void UpdatePhysics()
		{
			if (!vehicleRigidbody) return;
			
			CheckGround();
			
			wheelData.suspensionProfile.Sequence(wheelData);

			if (!wheelData.hitInfo.transform) return;

			wheelData.frictionProfile.Sequence(wheelData);

			ResolveCollision();

			ApplyForce();
			
			RotateWheelPhysics();
		}

		private void ResolveCollision()
		{
			// ApplyForce
			// Move vehicle transform
			
			/*Vector3 velocity = vehicleRigidbody.GetPointVelocity(wheelData.hitInfo.point);
			float nSpeed = Vector3.Dot(velocity, wheelData.hitInfo.normal);
			
			if (nSpeed >= 0f) return;

			float invMass = 1f / vehicleRigidbody.mass;

			float j = -2f * nSpeed / (invMass + 1f);
			Vector3 impulse = j * wheelData.hitInfo.normal;
            
			vehicleRigidbody.AddForceAtPosition(impulse, wheelData.hitInfo.point, ForceMode.Impulse);

			Vector3 correction = wheelData.hitInfo.normal * (0.8f *
				Mathf.Max(wheelData.penetrationDistance - 0.01f, 0f) / invMass);

			Vector3 delta = invMass * correction;
			vehicleRigidbody.transform.position += delta;*/
		}

		private void CheckGround()
		{
			wheelRigidbody.SweepTest(-transform.up, out wheelData.hitInfo, wheelData.suspensionData.MaxLength);

			if (wheelData.hitInfo.transform)
			{
				wheelData.linearVelocity = vehicleRigidbody.GetPointVelocity(wheelData.hitInfo.point);
				wheelData.linearVelocity = transform.InverseTransformDirection(wheelData.linearVelocity);
			}
			else
			{
				wheelData.linearVelocity = Vector3.zero;
			}
		}

		private void ApplyForce()
		{
			Vector3 force = transform.up * wheelData.suspensionData.upForce;

			Vector3 forwardProject = Vector3.ProjectOnPlane(transform.forward, wheelData.hitInfo.normal);
			forwardProject.Normalize();
			Vector3 rightProject = Vector3.ProjectOnPlane(transform.right, wheelData.hitInfo.normal);
			rightProject.Normalize();

			force += forwardProject * wheelData.frictionData.tireForce.y + rightProject * wheelData.frictionData.tireForce.x;
			
			vehicleRigidbody.AddForceAtPosition(force, transform.position);
		}

		private void RotateWheelPhysics()
		{
			/*
			 * These weird block of code is the reason why you can make ANY friction model and be sure, that wheels will behave good.
			 */
			
			/*
			 * In real life time passes continuously and forces are applied simultaneously.
			 * In virtual reality it does not.
			 * All calculations in PC are discrete, all instructions follow each other.
			 * So you need to figure out, in what order you should apply forces, so the wheel behaves like a real one.
			 */
			
			/*
			 * This logic is not the best one, it definitely misses some instructions.
			 * But it's a good start. And it works, this is the best thing about it.
			 */
			
			/*
			 * What's the logic, you may ask. Well, let's see...
			 */
			
			/*
			 * In real life there is always friction forces that are acting upon the wheel. Any additional torque will be
			 *		managed according to these forces. That's why we need to calculate friction first.
			 * After that we can apply any additional torque.
			 * Every calculation must be separated from others, any angularAcceleration should be clamped.
			 */

			/*
			 * First, we need to accelerate wheel rotation according to friction force applied.
			 * That acceleration must be clamped so the wheel won't rotate more than it should.
			 * Clamp limit comes from logic: without additional torque the wheel can't rotate more than it has passed.
			 */
			float frictionAngVel = Mathf.Abs(wheelData.frictionData.tireForce.y) * Time.fixedDeltaTime / wheelData.inertia * Mathf.Sign(-wheelData.Slip);
			float limit = Mathf.Abs(wheelData.Slip) / wheelData.radius;
			frictionAngVel = Mathf.Clamp(frictionAngVel, -limit, limit);
			wheelData.angularVelocity += frictionAngVel;
			
			/*
			 * Secondly wheel can't rotate for ever. There is always a rolling resistance (rolling friction, rolling drag)
			 * Accelerate wheel in the opposite direction of it's current rotation
			 * That acceleration should be clamped: we don't want to apply more force than it's needed to stop rolling
			 */
			float rollFrictionForce = 0.015f * Mathf.Max(wheelData.suspensionData.upForce, 0f); // TODO: get 0.005f from manager
			float rollFrictionAngAccel = rollFrictionForce * wheelData.radius / wheelData.inertia;
			rollFrictionAngAccel = Mathf.Min(rollFrictionAngAccel, Mathf.Abs(wheelData.angularVelocity));
			wheelData.angularVelocity += rollFrictionAngAccel * Mathf.Sign(-wheelData.angularVelocity) * Time.fixedDeltaTime;

			/*
			 * Now we can talk about additional torque.
			 * Drive torque is simple and formula comes from the laws of physics.
			 */
			float angularAcceleration = wheelData.driveTorque / wheelData.inertia;
			wheelData.angularVelocity += angularAcceleration * Time.fixedDeltaTime;

			/*
			 * Brake torque is almost like drive torque, but it should be clamped for the same reason as roll friction:
			 *		we don't want to apply more brake force than it's needed to stop rotation.
			 *		When you press brakes on you car, wheels don't rotate backwards, right?
			 */
			angularAcceleration = wheelData.brakeTorque / wheelData.inertia;
			float tempAngVel = angularAcceleration * Time.fixedDeltaTime;
			tempAngVel = Mathf.Min(tempAngVel, Mathf.Abs(wheelData.angularVelocity));
			wheelData.angularVelocity += tempAngVel * Mathf.Sign(-wheelData.angularVelocity);
		}
	}
}

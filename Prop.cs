using System;
using OpenTK;
using BulletSharp;

namespace Blocky
{
	public abstract class Prop
	{
		public CollisionShape Shape { get; private set; }
		public float Mass { get; private set; }
		public bool IsDynamic { get; private set; }
		
		public RigidBody Body { get; private set; }
		public MotionState State { get { return Body.MotionState; } }
		
		public Prop(CollisionShape shape, float mass, Matrix4 start)
		{
			Physics physics = Game.Instance.Physics;
			
			Shape = shape;
			IsDynamic = (mass != 0.0f);
			physics.Props.Add(this);
			Vector3 inertia = Vector3.Zero;
            if (IsDynamic) shape.CalculateLocalInertia(mass, out inertia);
            DefaultMotionState motionState = new DefaultMotionState(start);
            RigidBody.RigidBodyConstructionInfo info = new RigidBody.RigidBodyConstructionInfo(mass, motionState, shape, inertia);
            Body = new RigidBody(info);
            physics.World.AddRigidBody(Body);
		}
		
		public virtual void Update(double time) {  }
		public virtual void Render() {  }
	}
}

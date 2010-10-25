using System;
using System.Collections.Generic;
using OpenTK;
using BulletSharp;

namespace Blocky
{
	public class Physics
	{
		public DiscreteDynamicsWorld World { get; private set; }
		public CollisionDispatcher Dispatcher { get; private set; }
		
		public List<Prop> Props { get; private set; }
		
		public Physics()
		{
			CollisionConfiguration config = new DefaultCollisionConfiguration();
			Dispatcher = new CollisionDispatcher(config);
			World = new DiscreteDynamicsWorld(Dispatcher, new DbvtBroadphase(), null, config);
			World.Gravity = new Vector3(0.0f, -9.81f, 0.0f);
			
			Props = new List<Prop>();
		}
		
		public void Update(double time)
		{
			World.StepSimulation((float)time);
		}
		
		public void Add(Prop entity)
		{
			World.AddRigidBody(entity.Body);
		}
		public void Remove(Prop entity)
		{
			World.RemoveRigidBody(entity.Body);
		}
	}
}

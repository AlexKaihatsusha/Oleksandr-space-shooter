# Oleksandr-space-shooter
 
I used the code that we went through in the lectures and used it to write the following logic for my project, since we actually went through most of the main points required for the assignment. 

What did I add and change?
- I created the Asteroid class, which is almost identical in implementation to the Projectile class. I just changed the direction of movement and added the LifeTime component that was made in the lectures, and that's it, since the logic is handled in the LifeTimeManagementSystem, I didn't add anything extra.

- Added the logic of object colizations. At first I played with PhysicsShape but without success, only I started to be confused . Instead of that I just used the Mathematics library to do it. Since is generally more performance than regular Unity maths and because it already has some logic for AABB processing.
- Also added a ShapeSize component which holds both X and Y sizes of the shape( just 2 float variables).
- Created a new AABBComponent which is struct. (i.e. it is stored in a stack) And I added there an AABB object which consists of Сenter float3 and Extents float3 by AABB definition.
  And added this component to the objects that will interact with Projectile and Asteroid class.

- Created an AABBMovementSystem that updates all Entities that include the AABBComponent mainly to update the AABB data (made a Generic approach so to speak).
set it to update [UpdateAfter(typeof(TransformSystemGroup))]. to run the logic after we made the move.
In the loop I update Center float3 and assigned it the current position of the object and set the component data using SetComponentData() function.

Now the most important thing is that I created a CollisionDetectionSystem that handles collision.

I set it to update [UpdateInGroup(typeof(SimulationSystemGroup))] in the simulation group and [UpdateAfter(typeof(TransformSystemGroup))]  (As I did with AABB movement system, which is mentioned above, so we update collisions and AABB data with minimal differnces during update).

And for optimisation, I added in the OnCreate function
state.RequireForUpdate<ProjectileTag>();
which means we will start the loop as soon as Projectile appears in the world, and this way we don't do any checking. Maybe to add additional AsteroidTag will also improve, but I didn't add

In the loop I have declared a buffer that will be responsible for the commands to delete objects.
below I created a basic bool function AABBOverlapCheck inside the basic logic of checking AABB (Axis-Aligned Bounding Box) of input paraments of two AABBs data.
Then there are foreach loops that I use to get objects by tags (I added custom tags to Projectile and Asteroid as components in baker classes).
The first loop searches for Entities by ProjectileTag and AABBComponent so that we check Projectile with all asteroids (the nested(second) loop).

I wouldn't say that this approach is ideal for solving the problem of collision handling, as I haven't found a solution how we can use Jobs to handle and process  two arrays of Entities. Because the documentation doesn't show much in the way of examples or use cases


And the waves, I added WaveData as component to Spawner and set some default data in Authoring class, I didn't want to change SpawnerCode
In SpawnerSystem I added new function HandleWave(and added to Query component WaveData to process). So and there is just a simple time updating and switching on/off waves, and spawn asteroids when wave is going on

Finally, I created executable build, I faced with some problems adding packages (because they were hidden).

Thank you for you time and attention!!!!
Have a magnificant day!


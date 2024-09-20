using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

//add decorator
[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class PlayerInputSystem : SystemBase
{
    private GameInput InputActions;
    private Entity Player;

    
    //OnCreating
    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
        InputActions = new GameInput();
    }

    //it when the system first start to RUN
    protected override void OnStartRunning()
    {
        InputActions.Enable();
        InputActions.Gameplay.Shoot.performed += OnShoot;
        Player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    private void OnShoot(InputAction.CallbackContext obj)
    {
        //make sure that player exist
        if(!SystemAPI.Exists(Player)) return;
        
        SystemAPI.SetComponentEnabled<FireProjectileTag>(Player, true);
    }

    protected override void OnUpdate()
    {
        Vector2 moveInput = InputActions.Gameplay.Move.ReadValue<Vector2>();
        SystemAPI.SetSingleton(new PlayerMoveInput { Value = moveInput});
    }

    protected override void OnStopRunning()
    {
        InputActions.Disable();
        Player = Entity.Null;
    }
}

class_name KinematicEntityInterface
extends ObjectInterface


@export var target: KinematicEntity:
	set(value):
		target = value
		target.tree_exiting.connect(_target_updated)
		_target_updated()

var _is_target_valid: bool = false


## Apply force to the desired direction.
func move(direction: Vector2, force_override: float = -1.0) -> void:
	# If our target isn't valid, don't even try.
	if not _is_target_valid:
		printerr("Target %s is not valid!" % target)
		return

	# Move in the desired direction.
	var movement_force: Vector2 = direction * (
		# If force_override was set, use it instead.
		force_override if force_override != -1 else target.movement_force
		)
	target.apply_central_force(movement_force)

	## TODO: Find a better way of doing this.
	# Rotate towards the direction we're moving.
	var angle_diff: float = angle_difference(target.rotation, movement_force.angle())
	target.apply_torque(target.movement_force * angle_diff * 10)


func _target_updated() -> void:
	_is_target_valid = is_instance_valid(target) and target is KinematicEntity

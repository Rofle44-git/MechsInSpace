class_name PlayerInput
extends Component


signal left_axis(strength: Vector2)


func _physics_process(_delta: float) -> void:
	var _left_axis_strength: Vector2 = Input.get_vector(
		"left", "right",
		"up", "down"
	)

	if _left_axis_strength.is_zero_approx(): return
	left_axis.emit(_left_axis_strength)

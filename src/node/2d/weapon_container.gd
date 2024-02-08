class_name WeaponContainer
extends Node2D
## A node for managing [Weapon]s.
##
## A [WeaponContainer] is responsible of switching out, and providing access
## to, the active_weapon weapon.


var active_weapon: Weapon = null ## The currently active [Weapon].
var active_idx: int = 0 ## The index of [member WeaponContainer.active_weapon] among the container's child-nodes.


func _init() -> void:
	child_entered_tree.connect(_on_weapon_added)


func cycle(step: int) -> void: ## Cycles trough the weapons with a specified step.
	# In-/decrement active_index by the amount of "step", and loop it within
	# the possible range of weapons.
	active_idx = posmod(active_idx+step, get_child_count()-1)
	_switch_weapon()


func next() -> void:
	cycle(1)


func previous() -> void:
	cycle(-1)


func set_active_weapon(new_idx: int) -> void: ## Attempts to set [member WeaponContainer.active_weapon] to the Nth child.
	# Is the new index inbounds?
	if new_idx >= 0 or new_idx < get_child_count():
		# Yes, use it and switch the weapon.
		active_idx = new_idx
		_switch_weapon()
	else:
		# No, push an error.
		push_error("new_idx is out o	f bounds.")


func _switch_weapon() -> void:
	# Is there already an active weapon?
	if active_weapon:
		# Yes, wait for it to disappear first
		await active_weapon.disappear()

	active_weapon = get_child(active_idx)
	active_weapon.appear()


func _on_weapon_added(_node: Node) -> void:
	# Set it to 0 just in case.
	active_idx = 0
	active_weapon = get_child(active_idx)
	# Signal is no longer needed, disconnect it and abort.
	child_entered_tree.disconnect(_on_weapon_added)
	return



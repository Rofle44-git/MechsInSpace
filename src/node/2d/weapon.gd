class_name Weapon
extends Node2D
## Abstract class for weapons.
##
## This is a base class for all weapons. It provides universal and simple
## functions to be called by other nodes.


signal just_shot() ## Emitted when a bullet has been shot.
signal started_shooting() ## Emitted when the weapon has started shooting.
signal stopped_shooting() ## Emitted when the weapon has stopped shooting.

@export_range(0.0, 32767.0, 1.0, "suffix:bullets") var bullet_capacity: float = 3.0 ## Maximum bullet capacity.
@export var reload_time: float = 1.0 ## Duration of each reload cycle.
@export var bullets_per_reload: float = 1.0 ## Amount of bullets re-stocked each reload cycle.
@export var bullets_per_second: int = 1 ## Amount of bullets shot per second.
@export_range(0.0, 100.0, 0.1, "suffix:% π") var inaccuracy: float = 0.0 ## Inaccuracy in percentage of pi. 100% would equal 180°, and 0% would equal 0°.
@export var bullet_scene: PackedScene ## The [Bullet] to be instantiated when shooting.
@export var weapon_tip: Marker2D ## The tip of the weapon to spawn the [Bullet] at.
@export_subgroup("Node dependencies")
@export var animations: AnimationPlayer = null ## The [AnimationPlayer] for the weapon's effects.

@onready var bullets_left: int = bullet_capacity ## Amount of bullets left.


func appear() -> void: ## Makes the weapon appear. This is called by [WeaponContainer] while switching weapons.
	show()


func disappear() -> void: ## Makes the weapon disappear. This is called by [WeaponContainer] while switching weapons.
	hide()


@warning_ignore("unused_parameter")
func reload(held: bool = true) -> void: {} ## Reloads the weapon. The "held" parameter is an optional flag for weapons with special behavior.


@warning_ignore("unused_parameter")
func shoot(held: bool = true) -> void: {} ## Attempts to shoot a [Bullet]. The "held" parameter is an optional flag for weapons with special behavior.


func _make_bullet() -> Bullet: ## Internal function responsible of actually instantiating a [Bullet].
	var bullet: Bullet = bullet_scene.instantiate() as Bullet
	bullet.global_transform = weapon_tip.global_transform
	return bullet

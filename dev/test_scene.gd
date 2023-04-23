extends Node


@export var packed : PackedScene


func _ready() -> void:
	var _packed = packed.instantiate()
	print("Setting rotation #1")
	add_child(_packed)
	var e = _packed.name
	print("Setting rotation #2")
	

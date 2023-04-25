extends Control


@export var override_debug_menu : bool = false;


func _ready() -> void:
	if OS.has_feature("debug") && !override_debug_menu:
		get_tree().change_scene_to_file("res://dev/dev_menu.tscn");

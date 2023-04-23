extends Node2D


func _ready() -> void:
	if OS.has_feature("debug"):
		get_tree().change_scene_to_file("res://dev/dev_menu.tscn");

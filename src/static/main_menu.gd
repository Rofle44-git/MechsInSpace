extends Control


@export var override_debug_menu : bool = false;


func _ready() -> void:
	if OS.has_feature("debug") && !override_debug_menu:
		get_tree().change_scene_to_file("res://dev/dev_menu.tscn");

func _on_start_pressed() -> void:
	get_tree().change_scene_to_file("res://src/static/game.tscn");

func _on_button_pressed() -> void:
	pass # Replace with function body.

func _on_about_pressed() -> void:
	pass # Replace with function body.

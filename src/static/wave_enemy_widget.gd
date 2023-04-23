extends PanelContainer


func set_enemy_name(text : String) -> void:
	@warning_ignore("unsafe_property_access")
	$HBoxContainer/Label.text = text;

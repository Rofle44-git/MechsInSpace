extends Node


func _ready() -> void:
	print("Ready")
	await get_tree().process_frame
	print("Using rotation")

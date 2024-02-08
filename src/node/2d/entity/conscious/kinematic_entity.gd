class_name KinematicEntity
extends ConsciousEntity
## Class for moving entities.
##
## A kinematic entity is a conscious entity capable of moving on its own.
##
## Examples: moving platform, drone


@export_range(1.0, 1073741824.0, 1.0) var movement_force: float = 1e4 ## The force applied when moving.

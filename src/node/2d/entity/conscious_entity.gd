class_name ConsciousEntity
extends Entity
## Class for sentient objects.
##
## A conscious Entity is an object capable of interacting with the game-world
## and/or its objects. The key difference between a [ConsciousEntity] and a
## [Prop], is that a [ConsciousEntity] can freely start interactions with
## the game-world or its entities, without requiring external triggers.
## In other words: Even though the [Prop] and [ConsciousEntity] can react to
## interactions or follow a scripted behavior, ONLY the [ConsciousEntity] can
## actually "get the ball rolling".
##
## Examples: moving platform, turret

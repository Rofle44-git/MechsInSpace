class_name Entity
extends RigidBody2D
## Abstract class for entities.
##
## This serves as a base class for all objects capable of performing at least
## one of the following actions:
## - interacting with the game-world and/or its objects
## - reacting to incoming interactions from the game-world and/or its objects
## If an object DOES have scripted behavior, but it can be imitated with a
## compound of nodes, it is NOT an entity.
## As this is a base class, inheriting from it is not intended.

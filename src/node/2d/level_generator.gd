class_name LevelGenerator
extends Node2D
## A base class for level generators.
##
## This serves as a base class for all level generators, that provides some
## override-able methods. These methods are implemented in this base class,
## to ensure all custom level generators that inherit from [LevelGenerator]
## are fully compatible with the default procedure of building and preparing
## procedurally generated levels. The intentions of these methods is, to make
## implementing an interface or controller for all level generators simpler.
## TL;DR the methods serve as a guideline, so you're forced to override them in
## in a way that is compatible with the script using them.

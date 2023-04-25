extends Label


var random_string : PackedStringArray = [
	"you know you want it",
	"as if your life depends on it",
	"shoot em up good",
	"it's what they would have wanted",
	"because that's what heros do",
	"or die",
	"all 4294967296 of them",
	"\"shoot em up good\",\n];\n\n\nfunc _ready() -> void:\n\ttext = \"...\"+random_string[randi_range(0, random_string.size()-1)];",
	"yes that's the title. that's all I came up with",
	"but don't beat em up",
];


func _ready() -> void:
	text = "..."+random_string[randi_range(0, random_string.size()-1)];

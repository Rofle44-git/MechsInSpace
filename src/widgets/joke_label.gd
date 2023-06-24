extends Label


var random_string : PackedStringArray = [
	"you know you want it",
	"as if your life depends on it",
	"shoot em up good",
	"it's what they would have wanted",
	"because that's what heros do",
	"or die",
	"all 2147483648 of them",
	"yes that's the title. that's all I came up with",
	"but don't beat em up",
	"pretty please?"
];


func _ready() -> void:
	text = "..."+random_string[randi_range(0, random_string.size()-1)];

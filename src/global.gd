extends Node

var is_recording: bool = false
var frame: int = 0
var ffmpeg_path: String = ""


func _process(_delta) -> void:
	if is_recording:
		var image = get_viewport().get_texture().get_image()
		FFmpeg.Encode(image)
		frame += 1

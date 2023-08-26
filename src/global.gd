extends Node

var is_recording: bool = false
var frame: int = 0
var ffmpeg_path: String = ""


func _process(_delta) -> void:
	if is_recording:
		frame += 1
		FFmpeg.Add(get_viewport().get_texture().get_image().save_png_to_buffer())

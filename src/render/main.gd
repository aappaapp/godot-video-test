extends Control


func _process(_delta) -> void:
	if Global.is_recording:
		var image = get_viewport().get_texture().get_image()
		FFmpeg.Encode(image)
		Global.frame += 1
